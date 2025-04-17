using Avalonia.Controls;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public class ChineseChessCell : CellBase
    {
        public bool AdvisorZone { get; }
        public ChineseChessCell(int x, int y, string value, ChineseChessBoard chineseChessBoard,
            PropertyChangedEventHandler postChessPieceMove, bool advisorZone = false)
            : base(x, y, value, chineseChessBoard, postChessPieceMove)
        {
            AdvisorZone = advisorZone;
        }

        public override void ResolveMove()
        {
            var previousCell = ChessBoard.SelectedCell;
            ChessBoard.SelectedCell = this;
        }

    }
}
