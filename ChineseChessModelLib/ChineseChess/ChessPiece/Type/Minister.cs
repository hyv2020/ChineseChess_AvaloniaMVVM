using ChineseChessModelLib.ChessPiece.Factory;
using ChineseChessModelLib.Enum;
using GameCommons;

namespace ChineseChessModelLib.ChessPiece.Type
{
    [ChessPieceAttr(ChessPieceType.Minister)]
    public class Minister : ChineseChessPieceBase
    {
        public Minister(Side side, ChineseChessCell cell) : base(side, cell)
        {

        }
        public Minister(Minister minister, ChineseChessCell cell) : base(minister, cell)
        {
        }
        public Minister()
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
            if (CheckOccupiedCell(-1, -1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(X - 2, Y - 2, out var cell))
                {
                    var cc = cell as ChineseChessCell;
                    if (CheckCrossRiver(cc))
                    {
                        yield return cc;
                    }
                }
            }
        }
        private IEnumerable<ChineseChessCell> FindMoveUpLeft(ChineseChessBoard chessBoard)
        {
            if (CheckOccupiedCell(1, -1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(X + 2, Y - 2, out var cell))
                {
                    var cc = cell as ChineseChessCell;
                    if (CheckCrossRiver(cc))
                    {
                        yield return cc;
                    }
                }
            }
        }
        private IEnumerable<ChineseChessCell> FindMoveDownRight(ChineseChessBoard chessBoard)
        {
            if (CheckOccupiedCell(-1, 1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(X - 2, Y + 2, out var cell))
                {
                    var cc = cell as ChineseChessCell;
                    if (CheckCrossRiver(cc))
                    {
                        yield return cc;
                    }
                }
            }
        }
        private IEnumerable<ChineseChessCell> FindMoveUpRight(ChineseChessBoard chessBoard)
        {
            if (CheckOccupiedCell(1, 1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(X + 2, Y + 2, out var cell))
                {
                    var cc = cell as ChineseChessCell;

                    if (CheckCrossRiver(cc))
                    {
                        yield return cc;
                    }
                }
            }
        }

        private bool CheckOccupiedCell(int xOffset, int yOffset, ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X + xOffset, Y + yOffset, out var occupiedCell))
            {
                return occupiedCell.ChessPiece == null;
            }
            return false;
        }
        private bool CheckCrossRiver(ChineseChessCell destCell)
        {
            return destCell.Side == Side;
        }
    }
}
