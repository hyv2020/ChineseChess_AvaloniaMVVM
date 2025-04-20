using ChineseChess_AvaloniaMVVM.Models;
using GameCommons;
using ReactiveUI;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public abstract class GameWindowViewModelBase : WindowViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! UserControl";
        ChessBoardUserControlViewModel _boardUserControl;
        public ICommand ToStartWindowCommand { get; }
        public MainWindowViewModel Parent { get; }

        public ChessBoardUserControlViewModel BoardUserControl
        {
            get { return _boardUserControl; }
            private set { this.RaiseAndSetIfChanged(ref _boardUserControl, value); }
        }

        protected GameWindowViewModelBase(MainWindowViewModel parent) : base(parent)
        {
            Parent = parent;
            _boardUserControl = new ChessBoardUserControlViewModel(PostChessPieceMove);
            ToStartWindowCommand = ReactiveCommand.Create(ToStartWindow);

        }
        public abstract void Reset();
        protected virtual void ToStartWindow()
        {
            Parent.ToStartWindow();
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
                    UpdateUIPostMove(board);
                    // Update the chessboard UI
                    Debug.WriteLine($"Property changed: {e.PropertyName}");
                    Debug.WriteLine("");
                }

            }

        }
        public abstract void UpdateUIPostMove(ChessBoardBase chessBoard);
    }
}
