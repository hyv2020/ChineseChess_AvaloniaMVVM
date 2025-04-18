using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Type
{
    [ChessPieceAttr(ChessPieceType.Soldier)]
    public class Soldier : ChineseChessPieceBase
    {
        public Soldier(Side side, ChineseChessCell cell) : base(side, cell)
        {

        }
        public Soldier(Soldier soldier, ChineseChessCell cell) : base(soldier, cell)
        {
        }
        public Soldier()
        {
            // Empty constructor for serialization
        }
        public override IEnumerable<ChineseChessCell> FindValidMove()
        {
            List<ChineseChessCell> availableCells = new();
            var chessBoard = Location.ChessBoard as ChineseChessBoard;
            if (Side == Side.Red)
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

        private IEnumerable<ChineseChessCell> FindMovesEast(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X + 1, Y, out var cell))
            {
                yield return cell as ChineseChessCell;
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesWest(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X - 1, Y, out var cell))
            {
                yield return cell as ChineseChessCell;
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesNorth(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X, Y + 1, out var cell))
            {
                yield return cell as ChineseChessCell;
            }
        }

        private IEnumerable<ChineseChessCell> FindMovesSouth(ChineseChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(X, Y - 1, out var cell))
            {
                yield return cell as ChineseChessCell;
            }
        }

        private List<ChineseChessCell> CheckCrossRiverMoves(ChineseChessBoard chessBoard)
        {
            List<ChineseChessCell> availableCells = new();
            if (chessBoard.FindSpecificCell(X, Y, out var currentCell))
            {
                var ccc = currentCell as ChineseChessCell;
                if (ccc.Side != Side)
                {
                    availableCells.AddRange(FindMovesEast(chessBoard));
                    availableCells.AddRange(FindMovesWest(chessBoard));
                }
            }
            return availableCells;
        }

    }
}
