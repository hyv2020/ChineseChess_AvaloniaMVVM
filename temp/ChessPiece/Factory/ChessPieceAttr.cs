using System;

namespace ChineseChess_Avalonia
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
