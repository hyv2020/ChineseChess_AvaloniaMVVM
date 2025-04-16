using GameCommons;
using System;

namespace ChineseChess_Avalonia
{
    public static class ChessPieceFactory
    {
        public static ChessPiece CreateChessPiece(int x, int y, Side side, ChessPieceType chessPieceType, ChessBoard chessBoard)
        {
            switch (chessPieceType)
            {
                case ChessPieceType.Soldier:
                    return CreateSoldier(side, x, y, chessBoard);
                case ChessPieceType.Horse:
                    return CreateHorse(side, x, y, chessBoard);
                case ChessPieceType.Cannon:
                    return CreateCannon(side, x, y, chessBoard);
                case ChessPieceType.Chariot:
                    return CreateChariot(side, x, y, chessBoard);
                case ChessPieceType.Minister:
                    return CreateMinister(side, x, y, chessBoard);
                case ChessPieceType.Advisor:
                    return CreateAdvisor(side, x, y, chessBoard);
                case ChessPieceType.General:
                    return CreateGeneral(side, x, y, chessBoard);
                default:
                    throw new ArgumentOutOfRangeException(nameof(chessPieceType));
            }
        }
        public static ChessPiece CloneChessPieceToNewLocation(int x, int y, ChessPiece oldPiece, ChessBoard chessBoard)
        {
            ChessPiece newPiece = CreateChessPiece(x, y, oldPiece.Side, oldPiece.GetChessPieceType(), chessBoard);
            newPiece.Name = oldPiece.Name;
            newPiece.ChessPicture.Name = oldPiece.ChessPicture.Name;
            return newPiece;
        }
        private static ChessPiece CreateSoldier(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Soldier(x, y, side, chessBoard);
        }
        private static ChessPiece CreateHorse(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Horse(x, y, side, chessBoard);
        }
        private static ChessPiece CreateCannon(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Cannon(x, y, side, chessBoard);
        }
        private static ChessPiece CreateChariot(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Chariot(x, y, side, chessBoard);
        }
        private static ChessPiece CreateMinister(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Minister(x, y, side, chessBoard);
        }
        private static ChessPiece CreateAdvisor(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Advisor(x, y, side, chessBoard);
        }
        private static ChessPiece CreateGeneral(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new General(x, y, side, chessBoard);
        }
    }
}
