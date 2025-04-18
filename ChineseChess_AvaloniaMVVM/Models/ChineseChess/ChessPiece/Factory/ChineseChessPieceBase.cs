using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using GameCommons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory
{
    public abstract class ChineseChessPieceBase : ChessPieceBase
    {
        protected ChineseChessPieceBase(Side side, ChineseChessCell cell) : base(side, cell)
        {
            ChessPieceType chessPieceType = this.GetChessPieceType();

            var count = cell.ChessBoard.GridArr
    .Where(cell => cell.ChessPiece is not null &&
    ((ChineseChessPieceBase)cell.ChessPiece).GetChessPieceType() == chessPieceType).Count() + 1;
            this.Name = $"{side}{chessPieceType}{count}";
        }

        /// <summary>
        /// Constructor for moving piece while keeping the same name
        /// </summary>
        /// <param name="chessPiece">Piece to move</param>
        protected ChineseChessPieceBase(ChineseChessPieceBase chineseChessPieceBase, ChineseChessCell cell) : base(chineseChessPieceBase, cell)
        {

        }
        public IEnumerable<ChineseChessCell> FliterCellsToValidPoints(IEnumerable<ChineseChessCell> cells)
        {
            var validateCells = cells.Where(c => c.ChessPiece == null || this.Side != c.ChessPiece.Side);
            return validateCells;
        }
        public ChessPieceType GetChessPieceType()
        {
            var chessType = GetType();
            object[] attrs = Attribute.GetCustomAttributes(chessType);
            foreach (object attr in attrs)
            {
                ChessPieceAttr chessPieceAttr = attr as ChessPieceAttr;
                if (chessPieceAttr != null)
                {
                    return chessPieceAttr.Type;
                }
            }
            throw new Exception();
        }
    }
}
