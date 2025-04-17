using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChineseChess_AvaloniaMVVM.Models;
using ReactiveUI;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class ChessBoardUserControlViewModel : ViewModelBase
    {
        public string Message { get; } = "Chessboard";
        private ChessBoardViewModel _ChineseClassBoard;
        public ChessBoardViewModel ChineseClassBoard
        {
            get { return _ChineseClassBoard; }
            set { this.RaiseAndSetIfChanged(ref _ChineseClassBoard, value); }
        }
        public int RowCount
        {
            get { return _ChineseClassBoard.RowCount; }
        }
        public List<CellViewModel> GridVm
        {
            get { return _ChineseClassBoard.GridVm; }
        }
        public ChessBoardUserControlViewModel(PropertyChangedEventHandler postChessPieceMove)
        {
            var board =new ChineseChessBoard(postChessPieceMove);
            _ChineseClassBoard = new ChessBoardViewModel(board);
        }


    }
}
