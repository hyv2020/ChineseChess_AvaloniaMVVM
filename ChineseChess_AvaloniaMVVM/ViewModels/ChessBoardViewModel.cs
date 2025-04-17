using ChineseChess_AvaloniaMVVM.Models;
using ReactiveUI;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class ChessBoardViewModel : ViewModelBase
    {
        public string Message { get; } = "Chessboard";
        private ChessBoardBase _ChineseClassBoard;
        public ChessBoardBase ChineseClassBoard
        {
            get { return _ChineseClassBoard; }
            set { this.RaiseAndSetIfChanged(ref _ChineseClassBoard, value); }
        }
        public int RowCount
        {
            get { return _ChineseClassBoard.RowCount; }
        }
        public CellBase[] Grid
        {
            get { return _ChineseClassBoard.GridArr; }
        }
        public ChessBoardViewModel(PropertyChangedEventHandler postChessPieceMove)
        {
            _ChineseClassBoard = new ChineseChessBoard(postChessPieceMove);
        }


    }
}
