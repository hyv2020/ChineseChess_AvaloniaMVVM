using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_Avalonia
{
    [ChessPieceAttr(ChessPieceType.Advisor)]
    public class Advisor : ChessPiece
    {
        public Advisor(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
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
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y - 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
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
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y + 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }
        private IEnumerable<Cell> FindMoveUpRight(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y + 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }

    }
}
