using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using System;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece
{


    public static class ChessPieceMethods
    {
        public static ChessPieceType GetChessPieceType(this ChineseChessPieceBase chessPiece)
        {
            if (chessPiece != null)
            {
                var chessType = chessPiece.GetType();
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
            else
            {
                return ChessPieceType.Null;
            }
        }
    }

}
