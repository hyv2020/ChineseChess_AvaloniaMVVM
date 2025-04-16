using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_Avalonia
{
    [ChessPieceAttr(ChessPieceType.Cannon)]
    public class Cannon : ChessPiece
    {
        public Cannon(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            //x axis moves
            //scan the whole axis
            availableCells.AddRange(FindMovesEast(chessBoard));
            availableCells.AddRange(FindMovesWest(chessBoard));
            availableCells.AddRange(FindMovesNorth(chessBoard));
            availableCells.AddRange(FindMovesSouth(chessBoard));
            return FliterCellsToValidPoints(availableCells);
        }
        private IEnumerable<Cell> FindMovesEast(ChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = this.X + 1; i < ChessBoard.BoardSizeX; i++)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell;
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerable<Cell> FindMovesWest(ChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = this.X - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell;
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerable<Cell> FindMovesNorth(ChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = this.Y + 1; i < ChessBoard.BoardSizeY; i++)
            {
                if (chessBoard.FindSpecificCell(this.X, i, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell;
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerable<Cell> FindMovesSouth(ChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = this.Y - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(this.X, i, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell;
                            break;
                        }
                    }
                }
            }
        }
    }
}
