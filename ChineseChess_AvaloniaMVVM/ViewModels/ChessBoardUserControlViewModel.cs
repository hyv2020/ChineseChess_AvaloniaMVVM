using ChessModelLib;
using GameCommons;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class ChessBoardUserControlViewModel : ViewModelBase
    {
        public string Message { get; } = "Chessboard";
        private ChessBoardViewModel _chessBoardVm;
        public Side CurrentPlayerTurn
        {
            get { return _chessBoardVm.CurrentPlayerTurn; }
            set { _chessBoardVm.CurrentPlayerTurn = value; }
        }
        public ChessBoardViewModel ChessBoardVm
        {
            get { return _chessBoardVm; }
            set { this.RaiseAndSetIfChanged(ref _chessBoardVm, value); }
        }
        public int ColumnCount
        {
            get { return _chessBoardVm.ColumnCount; }
        }
        public List<CellViewModel> GridVm
        {
            get { return _chessBoardVm.GridVm; }
        }
        public bool Loading { get => _chessBoardVm.Loading; set { _chessBoardVm.Loading = value; } }
        public ChessBoardUserControlViewModel(PropertyChangedEventHandler postChessPieceMove, ICreateBoardCommand gameToCreate)
        {
            var board = gameToCreate.Execute(postChessPieceMove);
            _chessBoardVm = new ChessBoardViewModel(board);
        }

        public void ClearAllValidMoves()
        {
            _chessBoardVm.ClearAllValidMoves();
        }

        public void ClearBoard()
        {

            _chessBoardVm.ClearBoard();
        }
    }
}
