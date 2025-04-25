using Avalonia.Threading;
using ChineseChess_AvaloniaMVVM.Models;
using GameCommons;
using MsBox.Avalonia;
using ReactiveUI;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public abstract class GameWindowViewModelBase : WindowViewModelBase
    {
        public string GameDescription { get => BoardUserControl.ChessBoardVm.GameDescription; }
        public abstract string Message { get; }
        ChessBoardUserControlViewModel _boardUserControl;
        public ICommand ToStartWindowCommand { get; }
        public MainWindowViewModel Parent { get; }
        public Side CurrentPlayerTurn
        {
            get { return BoardUserControl.ChessBoardVm.CurrentPlayerTurn; }
            set { BoardUserControl.ChessBoardVm.CurrentPlayerTurn = value; }
        }
        public string TurnLabelText { get; set; }
        public ChessBoardUserControlViewModel BoardUserControl
        {
            get { return _boardUserControl; }
            private set { this.RaiseAndSetIfChanged(ref _boardUserControl, value); }
        }

        protected GameWindowViewModelBase(MainWindowViewModel parent) : base(parent)
        {
            Parent = parent;
            _boardUserControl = new ChessBoardUserControlViewModel(PostChessPieceMove);
            SetTurnLabel();
            ToStartWindowCommand = ReactiveCommand.Create(ToStartWindow);

        }
        public abstract void Reset();
        protected virtual void ToStartWindow()
        {
            var question = MessageBoxManager.GetMessageBoxStandard("To Start Window", "This action to end current game. Continue?", MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question);
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var result = await question.ShowAsync();
                if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
                {
                    Reset();
                    Parent.ToStartWindow();
                }
            });
        }
        protected void PostChessPieceMove(object? sender, PropertyChangedEventArgs e)
        {
            // Notify the UI to update the chessboard
            if (sender is CellBase cellVm && e.PropertyName == "ChessPiece")
            {
                var board = cellVm.ChessBoard;
                // only update the UI if the board is not loading and the cell has a chess piece
                if (!board.Loading && cellVm.ChessPiece == null)
                {
                    OnPropertyChanged(e.PropertyName);
                    var lastTurn = board.CurrentPlayerTurn;
                    if (lastTurn == Side.Red)
                    {
                        board.CurrentPlayerTurn = Side.Black;
                        // Update the chessboard UI
                        //Debug.WriteLine($"Property changed: {e.PropertyName}");
                        //Debug.WriteLine("");
                    }
                    else
                    {
                        board.CurrentPlayerTurn = Side.Red;
                    }
                    SetTurnLabel();
                    UpdateUIPostMove(board);
                    // Update the chessboard UI
                    Debug.WriteLine($"Property changed: {e.PropertyName}");
                    Debug.WriteLine("");
                }

            }

        }
        public abstract void UpdateUIPostMove(ChessBoardBase chessBoard);
        public bool CheckWinner(out Side side)
        {
            var thereIsWinner = BoardUserControl.ChessBoardVm.CheckWinner(out side);
            BoardUserControl.ChessBoardVm.ActiveGame = !thereIsWinner;
            return thereIsWinner;
        }
        public void SetTurnLabel()
        {
            if (BoardUserControl.ChessBoardVm.ActiveGame)
            {
                TurnLabelText = CurrentPlayerTurn.GetDescription() + " turn";
            }
            else
            {
                CheckWinner(out Side winner);
                TurnLabelText = winner.GetDescription() + " wins!";
            }
            this.RaisePropertyChanged(nameof(TurnLabelText));
        }
    }
}
