using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_Avalonia
{
    [ChessPieceAttr(ChessPieceType.Soldier)]
    public class Soldier : ChessPiece
    {
        public Soldier(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            if (this.Side == Side.Red)
            {
                availableCells.AddRange(FindMovesSouth(chessBoard));
            }
            else
            {
                availableCells.AddRange(FindMovesNorth(chessBoard));
            }
            availableCells.AddRange(CheckCrossRiverMoves(chessBoard));

            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<Cell> FindMovesEast(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y, out var cell))
            {
                yield return cell;
            }
        }

        private IEnumerable<Cell> FindMovesWest(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y, out var cell))
            {
                yield return cell;
            }
        }

        private IEnumerable<Cell> FindMovesNorth(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X, this.Y + 1, out var cell))
            {
                yield return cell;
            }
        }

        private IEnumerable<Cell> FindMovesSouth(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X, this.Y - 1, out var cell))
            {
                yield return cell;
            }
        }

        private List<Cell> CheckCrossRiverMoves(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            if (chessBoard.FindSpecificCell(this.X, this.Y, out var currentCell))
            {
                if (currentCell.Side != this.Side)
                {
                    availableCells.AddRange(FindMovesEast(chessBoard));
                    availableCells.AddRange(FindMovesWest(chessBoard));
                }
            }
            return availableCells;
        }

    }
}
