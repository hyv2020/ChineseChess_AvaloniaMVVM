using ChineseChessModelLib.ChessPiece.Factory;
using ChineseChessModelLib.Enum;
using GameCommons;

namespace ChineseChessModelLib.ChessPiece.Type
{
    [ChessPieceAttr(ChessPieceType.Horse)]
    public class Horse : ChineseChessPieceBase
    {

        public Horse(Side side, ChineseChessCell cell) : base(side, cell)
        {

        }
        public Horse(Horse horse, ChineseChessCell cell) : base(horse, cell)
        {
        }
        public Horse()
        {
            // Empty constructor for serialization
        }
        public override IEnumerable<ChineseChessCell> FindValidMove()
        {
            List<ChineseChessCell> availableCells = new();
            var chessBoard = Location.ChessBoard as ChineseChessBoard;
            availableCells.AddRange(FindMovesEast(chessBoard));
            availableCells.AddRange(FindMovesWest(chessBoard));
            availableCells.AddRange(FindMovesNorth(chessBoard));
            availableCells.AddRange(FindMovesSouth(chessBoard));
            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<ChineseChessCell> FindMovesEast(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X + 1, Y, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(X + 2, Y - 1, out var cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                    if (chessBoard.FindSpecificCell(X + 2, Y + 1, out cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesWest(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X - 1, Y, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(X - 2, Y - 1, out var cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                    if (chessBoard.FindSpecificCell(X - 2, Y + 1, out cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesNorth(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X, Y + 1, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(X + 1, Y + 2, out var cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                    if (chessBoard.FindSpecificCell(X - 1, Y + 2, out cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesSouth(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X, Y - 1, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(X + 1, Y - 2, out var cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                    if (chessBoard.FindSpecificCell(X - 1, Y - 2, out cell))
                    {
                        yield return cell as ChineseChessCell;
                    }
                }
            }
        }
    }
}
