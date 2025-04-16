using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_Avalonia
{
    [ChessPieceAttr(ChessPieceType.General)]
    public class General : ChessPiece
    {
        public General(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            availableCells.AddRange(FindMovesEast(chessBoard));
            availableCells.AddRange(FindMovesWest(chessBoard));
            availableCells.AddRange(FindMovesNorth(chessBoard));
            availableCells.AddRange(FindMovesSouth(chessBoard));
            if (CheckFlyingGeneral(out var cell, chessBoard))
            {
                availableCells.Add(cell);
            }
            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<Cell> FindMovesEast(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }

        private IEnumerable<Cell> FindMovesWest(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }

        private IEnumerable<Cell> FindMovesNorth(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X, this.Y + 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }

        private IEnumerable<Cell> FindMovesSouth(ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X, this.Y - 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    yield return cell;
                }
            }
        }

        private bool CheckFlyingGeneral(out Cell cell, ChessBoard chessBoard)
        {
            if (this.Side == Side.Red)
            {
                return CheckFlyingGeneralRed(out cell, chessBoard);
            }
            else
            {
                return CheckFlyingGeneralBlack(out cell, chessBoard);
            }

        }

        private bool CheckFlyingGeneralRed(out Cell cell, ChessBoard chessBoard)
        {
            for (int i = this.Y; i >= 0; i--)
            {
                if (i != this.Y && chessBoard.FindSpecificCell(this.X, i, out cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        if (cell.ChessPiece.GetChessPieceType() == ChessPieceType.General)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    continue;
                }
            }
            cell = null;
            return false;
        }
        private bool CheckFlyingGeneralBlack(out Cell cell, ChessBoard chessBoard)
        {
            for (int i = this.Y; i < ChessBoard.BoardSizeY; i++)
            {
                if (i != this.Y && chessBoard.FindSpecificCell(this.X, i, out cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        if (cell.ChessPiece.GetChessPieceType() == ChessPieceType.General)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    continue;
                }
            }
            cell = null;
            return false;
        }

    }
}
