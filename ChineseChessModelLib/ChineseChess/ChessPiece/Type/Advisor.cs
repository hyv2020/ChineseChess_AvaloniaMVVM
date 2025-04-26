using ChineseChessModelLib.ChessPiece.Factory;
using ChineseChessModelLib.Enum;
using GameCommons;

namespace ChineseChessModelLib.ChessPiece.Type
{
    [ChessPieceAttr(ChessPieceType.Advisor)]
    public class Advisor : ChineseChessPieceBase
    {
        public Advisor(Side side, ChineseChessCell cell) : base(side, cell)
        {

        }
        public Advisor(Advisor advisor, ChineseChessCell cell) : base(advisor, cell)
        {
        }
        public Advisor()
        {
            // Empty constructor for serialization
        }
        public override IEnumerable<ChineseChessCell> FindValidMove()
        {
            List<ChineseChessCell> availableCells = new();
            var chessBoard = Location.ChessBoard as ChineseChessBoard;
            availableCells.AddRange(FindMoveDownLeft(chessBoard));
            availableCells.AddRange(FindMoveUpLeft(chessBoard));
            availableCells.AddRange(FindMoveDownRight(chessBoard));
            availableCells.AddRange(FindMoveUpRight(chessBoard));

            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<ChineseChessCell> FindMoveDownLeft(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X - 1, Y - 1, out var cell))
            {
                var cc = cell as ChineseChessCell;
                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }
        private IEnumerable<ChineseChessCell> FindMoveUpLeft(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X + 1, Y - 1, out var cell))
            {
                var cc = cell as ChineseChessCell;
                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }
        private IEnumerable<ChineseChessCell> FindMoveDownRight(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X - 1, Y + 1, out var cell))
            {
                var cc = cell as ChineseChessCell;
                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }
        private IEnumerable<ChineseChessCell> FindMoveUpRight(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X + 1, Y + 1, out var cell))
            {
                var cc = cell as ChineseChessCell;
                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }

    }
}
