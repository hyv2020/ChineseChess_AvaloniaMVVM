using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Type
{
    [ChessPieceAttr(ChessPieceType.Cannon)]
    public class Cannon : ChineseChessPieceBase
    {
        public Cannon(Side side, ChineseChessCell cell) : base(side, cell)
        {

        }
        public Cannon(Cannon cannon, ChineseChessCell cell) : base(cannon, cell)
        {
        }
        public Cannon()
        {
            // Empty constructor for serialization
        }
        public override IEnumerable<ChineseChessCell> FindValidMove()
        {
            List<ChineseChessCell> availableCells = new();
            var chessBoard = Location.ChessBoard as ChineseChessBoard;
            //x axis moves
            //scan the whole axis
            availableCells.AddRange(FindMovesEast(chessBoard));
            availableCells.AddRange(FindMovesWest(chessBoard));
            availableCells.AddRange(FindMovesNorth(chessBoard));
            availableCells.AddRange(FindMovesSouth(chessBoard));
            return FliterCellsToValidPoints(availableCells);
        }
        private IEnumerable<ChineseChessCell> FindMovesEast(ChineseChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = X + 1; i < chessBoard.BoardSizeX; i++)
            {
                if (chessBoard.FindSpecificCell(i, Y, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell as ChineseChessCell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell as ChineseChessCell;
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesWest(ChineseChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = X - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(i, Y, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell as ChineseChessCell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell as ChineseChessCell;
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesNorth(ChineseChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = Y + 1; i < chessBoard.BoardSizeY; i++)
            {
                if (chessBoard.FindSpecificCell(X, i, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell as ChineseChessCell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell as ChineseChessCell;
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesSouth(ChineseChessBoard chessBoard)
        {
            bool firstOccupied = false;
            for (int i = Y - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(X, i, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        yield return cell as ChineseChessCell;
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            yield return cell as ChineseChessCell;
                            break;
                        }
                    }
                }
            }
        }
    }
}
