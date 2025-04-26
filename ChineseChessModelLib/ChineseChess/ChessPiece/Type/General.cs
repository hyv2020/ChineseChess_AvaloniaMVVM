using ChineseChessModelLib.ChessPiece.Factory;
using ChineseChessModelLib.Enum;
using GameCommons;

namespace ChineseChessModelLib.ChessPiece.Type
{
    [ChessPieceAttr(ChessPieceType.General)]
    public class General : ChineseChessPieceBase
    {
        public General(Side side, ChineseChessCell cell) : base(side, cell)
        {

        }
        public General(General general, ChineseChessCell cell) : base(general, cell)
        {
        }
        public General()
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
            if (CheckFlyingGeneral(out var cell, chessBoard))
            {
                availableCells.Add(cell);
            }
            return FliterCellsToValidPoints(availableCells);
        }

        private IEnumerable<ChineseChessCell> FindMovesEast(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X + 1, Y, out var cell))
            {
                var cc = cell as ChineseChessCell;
                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesWest(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X - 1, Y, out var cell))
            {
                var cc = cell as ChineseChessCell;

                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesNorth(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X, Y + 1, out var cell))
            {
                var cc = cell as ChineseChessCell;

                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesSouth(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X, Y - 1, out var cell))
            {
                var cc = cell as ChineseChessCell;

                if (cc.AdvisorArea)
                {
                    yield return cc;
                }
            }
        }

        private bool CheckFlyingGeneral(out ChineseChessCell cell, ChineseChessBoard chessBoard)
        {
            if (Side == Side.Red)
            {
                return CheckFlyingGeneralRed(out cell, chessBoard);
            }
            else
            {
                return CheckFlyingGeneralBlack(out cell, chessBoard);
            }

        }

        private bool CheckFlyingGeneralRed(out ChineseChessCell cell, ChineseChessBoard chessBoard)
        {
            for (int i = Y; i >= 0; i--)
            {
                if (i != Y && chessBoard.FindSpecificCell(X, i, out var cb))
                {
                    cell = cb as ChineseChessCell;
                    if (cell.ChessPiece != null)
                    {
                        if (((ChineseChessPieceBase)cell.ChessPiece).GetChessPieceType() == ChessPieceType.General)
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
        private bool CheckFlyingGeneralBlack(out ChineseChessCell cell, ChineseChessBoard chessBoard)
        {
            for (int i = Y; i < chessBoard.BoardSizeY; i++)
            {
                if (i != Y && chessBoard.FindSpecificCell(X, i, out var cb))
                {
                    cell = cb as ChineseChessCell;
                    if (cell.ChessPiece != null)
                    {
                        if (((ChineseChessPieceBase)cell.ChessPiece).GetChessPieceType() == ChessPieceType.General)
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
