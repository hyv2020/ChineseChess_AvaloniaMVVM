using Avalonia.Threading;
using ChessModelLib;
using ChineseChess_AvaloniaMVVM.Models;
using GameClient;
using GameCommons;
using GameServer;
using MsBox.Avalonia;
using NetworkCommons;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class NetworkGameWindowViewModel : GameWindowViewModelBase, INetworkObserver, IDisposable
    {
        public string PlayerLabelText { get; set; }
        public string ConnectionStatusText { get; set; }
        public string PublicIPLabelText { get; set; }
        public override string Message { get; } = "Network Game";
        string[] sides = Enum.GetNames(typeof(Side));
        public bool ClientConnected { get; set; } = true;
        AsynchronousTCPListener listener;
        AsynchronousClient client;
        bool gameStarted = false;
        bool host = false;
        Side playerSide;
        string serverIP;
        int currentTurn = 1;
        List<Turn> turnRecord = new List<Turn>();
        public NetworkGameWindowViewModel(MainWindowViewModel parent) : base(parent)
        {
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            HostInitialise();
        }
        public NetworkGameWindowViewModel(MainWindowViewModel parent, string connectionIP) : base(parent)
        {
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            this.host = false;
            ClientStart(connectionIP);
            client.SendMessageAsync(BoardUserControl.ChessBoardVm.GameMode);
            client.SendMessageAsync("1");
            client.RegisterObserver(this);
            UpdatePlayerLabel();
            this.gameStarted = false;
        }
        public NetworkGameWindowViewModel() : this(new MainWindowViewModel())
        {
            // This constructor is used for design-time data
        }
        public override void Reset()
        {
            /*            var vm = BoardUserControl.ChessBoardVm;
                        vm.ClearBoard();
                        vm.LoadGame();
                        vm.ActiveGame = true;
                        this.turnRecord.Clear();
                        this.currentTurn = 1;
                        this.host = false;
                        this.gameStarted = false;
                        this.ClientConnected = true;
                        this.sameGame = false;
                        Disconnect();*/
        }

        public override void UpdateUIPostMove(ChessBoardBase chessBoard)
        {
            this.EndTurn();
            BoardUserControl.ChessBoardVm.ActiveGame = false;
            this.SaveState();
            UtilOps.DeleteTempFilesAfterThisTurn(this.currentTurn);
        }
        private void HostInitialise()
        {
            string localIP = NetworkCommons.IP.GetCurrentMachineIP();
            this.host = true;
            var moveSide = UtilOps.RandomStart();
            var playerSide = UtilOps.RandomStart();
            this.playerSide = playerSide;
            UpdatePlayerLabel(playerSide);
            BoardUserControl.ChessBoardVm.LoadGame();
            Turn startTurn = new Turn(BoardUserControl.ChessBoardVm.GameMode, this.currentTurn, moveSide, BoardUserControl.ChessBoardVm.SaveGame().ToList());
            BoardUserControl.CurrentPlayerTurn = moveSide;
            ConnectionStatusText = "Hosting at " + localIP;
            PublicIPLabelText = "Public IP: " + NetworkCommons.IP.GetPublicIpAddress();
            LoadTurn(startTurn);
            SetTurnLabel();
            BoardUserControl.ChessBoardVm.ActiveGame = false;
            listener = new AsynchronousTCPListener(BoardUserControl.ChessBoardVm.GameMode);
            this.listener.hostStartSide = playerSide;
            this.listener.hostStartTurn = startTurn;
            this.gameStarted = true;

            listener.RegisterObserver(this);
            ServerStartAsync();
        }
        #region Network Management

        /// <summary>
        /// Start server
        /// </summary>
        private async void ServerStartAsync()
        {
            var listen = listener.StartListeningAsync();
            await listen;
        }

        int attempts = 1;
        private bool disposedValue;

        /// <summary>
        /// Connect client to server
        /// </summary>
        /// <param name="serverIP"></param>
        private void ClientStart(string serverIP)
        {
            TryConnectAsync();
            async void TryConnectAsync()
            {
                this.serverIP = serverIP;
                try
                {
                    // ip address of current dervice
                    client = new AsynchronousClient(serverIP, BoardUserControl.ChessBoardVm.GameMode);
                    if (!this.host)
                    {
                        ConnectionStatusText = "Connecting to " + serverIP;
                        this.RaisePropertyChanged(nameof(ConnectionStatusText));
                        //ConnectionStatusLabel.Text = "Joined " + serverIP;
                        //PublicIPLabel.Text = "Public IP: " + NetworkCommons.IP.GetPublicIpAddress();
                    }
                    var connectServer = client.ConnectAsync();
                    client.RegisterObserver(this);
                    await connectServer;
                    // code after this line dont run
                }
                catch (Exception ex)
                {
                    attempts++;
                    Debug.WriteLine($"{serverIP} client connection failed");
                    Debug.WriteLine($"{ex.GetType()} : {ex.Message}");
                    var failedConnectionMessage = MessageBoxManager.GetMessageBoxStandard("Failed to connect to server", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Error);
                    Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        var result = await failedConnectionMessage.ShowAsync();
                        if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
                        {
                            Debug.WriteLine($"Attempt {attempts} to connect to {serverIP} again...");
                            client.Disconnect();
                            ConnectionStatusText = $"Retry attempt {attempts} to join" + serverIP;
                            ClientStart(serverIP);
                        }
                        else
                        {
                            Parent.ToStartWindow();
                            client.Disconnect();
                            this.ClientConnected = false;
                            this.Dispose();
                        }
                    });
                }

            }
        }

        public void OnTcpDataReceived(object data)
        {
            if (data as Turn != null)
            {
                LoadTurn(data as Turn);
                SetTurnLabel();
                if (!CheckWinner(out Side winner))
                {
                    BoardUserControl.ChessBoardVm.ActiveGame = BoardUserControl.CurrentPlayerTurn == playerSide;
                }
            }
            else if (int.TryParse(data.ToString(), out int clientCount))
            {
                if (clientCount == 1 && !this.gameStarted)
                {
                    this.gameStarted = true;
                    var startMessage = MessageBoxManager.GetMessageBoxStandard("Game Start", "Have comes a new challenger!", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info);
                    BoardUserControl.ChessBoardVm.ActiveGame = playerSide == BoardUserControl.CurrentPlayerTurn;
                    SetTurnLabel();
                    Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        var result = await startMessage.ShowAsync();
                        if (result == MsBox.Avalonia.Enums.ButtonResult.Ok)
                        {
                            // Handle the OK button click
                        }
                    });
                    SendDataToServerAsync(clientCount);
                }
                else if (clientCount == 1 && this.gameStarted)
                {
                    this.gameStarted = true;
                    var startMessage = MessageBoxManager.GetMessageBoxStandard("Game Start", "Game is starting...", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info);
                    Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        var result = await startMessage.ShowAsync();
                        if (result == MsBox.Avalonia.Enums.ButtonResult.Ok)
                        {
                            // Handle the OK button click
                        }
                    });
                    BoardUserControl.ChessBoardVm.ActiveGame = playerSide == BoardUserControl.CurrentPlayerTurn;
                }
            }
            else if (data.ToString() == "0")
            {
                // client disconnected
                this.ClientConnected = false;
                ConnectionStatusText = "Client Disconnected";
                this.RaisePropertyChanged(nameof(ConnectionStatusText));
                PublicIPLabelText = "Public IP: " + NetworkCommons.IP.GetPublicIpAddress();
            }
            else if (sides.Contains(data.ToString()))
            {
                var recievedSide = (Side)Enum.Parse(typeof(Side), data.ToString());
                if (!this.host)
                {
                    if (recievedSide == Side.Red)
                    {
                        this.playerSide = Side.Black;
                    }
                    else
                    {
                        this.playerSide = Side.Red;
                    }
                }
                UpdatePlayerLabel(playerSide);
            }
            else if (data.ToString() == BoardUserControl.ChessBoardVm.GameMode && !this.gameStarted)
            {
            }
            else if (data.ToString() != BoardUserControl.ChessBoardVm.GameMode && !this.gameStarted)
            {
                if (!this.host)
                {
                    Parent.ToStartWindow();
                    Disconnect();
                    this.ClientConnected = false;
                    Dispose();
                }
            }
            Debug.WriteLine($"Observer Received data: {data}");
        }
        private async Task SendDataToServerAsync(object data)
        {
            if (client != null)
            {
                var sendData = client.SendMessageAsync(data);
                await sendData;
            }
            else
            {
                var sendData = listener.RedirectToClientAsync(data);
                await sendData;
            }
        }

        #endregion
        private void LoadTurn(Turn turn)
        {
            this.turnRecord.Add(turn);
            var selectedTurn = this.turnRecord.Last();
            BoardUserControl.ChessBoardVm.ClearBoard();
            BoardUserControl.ChessBoardVm.LoadGame(selectedTurn.BoardState);
            this.currentTurn = selectedTurn.TurnNumber;
            BoardUserControl.CurrentPlayerTurn = selectedTurn.WhosTurn;
            // player on only use stuff on his/her turn
            SetTurnLabel();
            selectedTurn.SaveToFile();
        }
        private async void SaveState()
        {
            Turn currentTurnState = new Turn(BoardUserControl.ChessBoardVm.GameMode, this.currentTurn, BoardUserControl.CurrentPlayerTurn, BoardUserControl.ChessBoardVm.SaveGame().ToList());
            currentTurnState.SaveToFile();
            await SendDataToServerAsync(currentTurnState);
        }
        private void EndTurn()
        {
            var currentTurn = this.turnRecord.FindIndex(x => x.TurnNumber == this.currentTurn);
            var updatedTurnRecord = this.turnRecord.Take(currentTurn + 1);
            this.turnRecord = updatedTurnRecord.ToList();
            this.currentTurn++;
        }
        public override void SetTurnLabel()
        {
            if (!this.gameStarted && BoardUserControl.ChessBoardVm.ActiveGame)
            {
                TurnLabelText = CurrentPlayerTurn.GetDescription() + " turn";
            }
            else
            {
                try
                {
                    if (CheckWinner(out Side winner))
                    {
                        TurnLabelText = winner.GetDescription() + " wins!";
                    }
                    else
                    {
                        if (BoardUserControl.CurrentPlayerTurn == playerSide)
                        {
                            BoardUserControl.ChessBoardVm.ActiveGame = true;
                        }
                    }
                }
                catch
                {

                }
            }
            this.RaisePropertyChanged(nameof(TurnLabelText));
            if (BoardUserControl.CurrentPlayerTurn == playerSide)
            {
                BoardUserControl.ChessBoardVm.ActiveGame = true;
            }
            else
            {
                BoardUserControl.ChessBoardVm.ActiveGame = false;
            }
        }
        private void UpdatePlayerLabel(Side? playerSide = null)
        {
            if (playerSide == null)
            {
                PlayerLabelText = $"Game haven't started";
                return;
            }
            PlayerLabelText = $"You are {playerSide.GetDescription()}";
            this.RaisePropertyChanged(nameof(PlayerLabelText));
        }
        private void Disconnect()
        {
            if (!host)
            {
                client.Disconnect();
            }
            else
            {
                listener.StopListening();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~NetworkGameWindowViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
