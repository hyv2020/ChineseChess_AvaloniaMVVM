using ChineseChessModelLib.Enum;

namespace ChineseChessModelLib.ChessPiece.Factory
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
