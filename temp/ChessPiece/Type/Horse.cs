using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_Avalonia
{
    [ChessPieceAttr(ChessPieceType.Horse)]
    public class Horse : ChessPiece
    {

        public Horse(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            availableCells.AddRange(FindMovesEast(chessBoard));
            availableCells.AddRange(FindMovesWest(chessBoard));
            availableCells.AddRange(FindMovesNorth(chessBoard));
            availableCells.AddRange(FindMovesSouth(chessBoard));
            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<Cell> FindMovesEast(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X + 2, this.Y - 1, out var cell))
                    {
                        yield return cell;
                    }
                    if (chessBoard.FindSpecificCell(this.X + 2, this.Y + 1, out cell))
                    {
                        yield return cell;
                    }
                }
            }
        }

        private IEnumerable<Cell> FindMovesWest(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X - 2, this.Y - 1, out var cell))
                    {
                        yield return cell;
                    }
                    if (chessBoard.FindSpecificCell(this.X - 2, this.Y + 1, out cell))
                    {
                        yield return cell;
                    }
                }
            }
        }

        private IEnumerable<Cell> FindMovesNorth(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X, this.Y + 1, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X + 1, this.Y + 2, out var cell))
                    {
                        yield return cell;
                    }
                    if (chessBoard.FindSpecificCell(this.X - 1, this.Y + 2, out cell))
                    {
                        yield return cell;
                    }
                }
            }
        }

        private IEnumerable<Cell> FindMovesSouth(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X, this.Y - 1, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X + 1, this.Y - 2, out var cell))
                    {
                        yield return cell;
                    }
                    if (chessBoard.FindSpecificCell(this.X - 1, this.Y - 2, out cell))
                    {
                        yield return cell;
                    }
                }
            }
        }
    }
}
