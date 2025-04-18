using GameCommons;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess
{
    public class ChineseChessCell : CellBase
    {
        public bool AdvisorArea { get; }
        public Side Side { get; }
        public ChineseChessCell(int x, int y, string value, ChineseChessBoard chineseChessBoard,
            PropertyChangedEventHandler postChessPieceMove, bool advisorZone = false)
            : base(x, y, value, chineseChessBoard, postChessPieceMove)
        {
            AdvisorArea = advisorZone;
            Side = (y > 4) ? Side.Red : Side.Black;

        }

        public override void ResolveMove()
        {
            var previousCell = ChessBoard.SelectedCell;
            ChessBoard.SelectedCell = this;
        }

    }
}
