using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using System;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory
{
    internal class ChessPieceAttr : Attribute
    {
        public ChessPieceType Type { get; set; }
        public ChessPieceAttr(ChessPieceType type)
        {
            Type = type;
        }
    }
}
