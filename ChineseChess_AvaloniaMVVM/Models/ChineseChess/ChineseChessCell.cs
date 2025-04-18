using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
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
        public ChineseChessCell()
        {
            // Empty constructor for serialization
        }
        public override void ResolveMove()
        {
            var previousCell = ChessBoard.SelectedCell;
            ChessBoard.SelectedCell = this;
        }

        public override string ToSaveCode()
        {
            if (this.ChessPiece is null)
            {
                return "0";
            }
            else
            {
                return $"{(int)this.ChessPiece.Side}{(int)GetChessPiece().GetChessPieceType()}";
            }
        }

        public override ChineseChessPieceBase? GetChessPiece()
        {
            if (this.ChessPiece is null)
            {
                return null;
            }
            else
            {
                return (ChineseChessPieceBase)this.ChessPiece;
            }
        }
    }
}
