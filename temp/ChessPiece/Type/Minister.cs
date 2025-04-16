using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_Avalonia
{
    [ChessPieceAttr(ChessPieceType.Minister)]
    public class Minister : ChessPiece
    {
        public Minister(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            availableCells.AddRange(FindMoveDownLeft(chessBoard));
            availableCells.AddRange(FindMoveUpLeft(chessBoard));
            availableCells.AddRange(FindMoveDownRight(chessBoard));
            availableCells.AddRange(FindMoveUpRight(chessBoard));
            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<Cell> FindMoveDownLeft(ChessBoard chessBoard)
        {
            if (CheckOccupiedCell(-1, -1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X - 2, this.Y - 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        yield return cell;
                    }
                }
            }
        }
        private IEnumerable<Cell> FindMoveUpLeft(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y - 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }
        private IEnumerable<Cell> FindMoveDownRight(ChessBoard chessBoard)
        {
            if (CheckOccupiedCell(-1, 1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X - 2, this.Y + 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        yield return cell;
                    }
                }
            }
        }
        private IEnumerable<Cell> FindMoveUpRight(ChessBoard chessBoard)
        {
            if (CheckOccupiedCell(1, -1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X + 2, this.Y - 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        yield return cell;
                    }
                }
            }
        }

        private bool CheckOccupiedCell(int xOffset, int yOffset, ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + xOffset, this.Y + yOffset, out var occupiedCell))
            {
                return occupiedCell.ChessPiece == null;
            }
            return false;
        }
        private bool CheckCrossRiver(Cell destCell)
        {
            return destCell.Side == this.Side;
        }
    }
}
