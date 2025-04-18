using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Type;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using GameCommons;
using System;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory
{
    public static class ChessPieceFactory
    {
        public static ChineseChessPieceBase CreateChessPiece(int x, int y, Side side, ChessPieceType chessPieceType, ChineseChessBoard chessBoard)
        {
            chessBoard.FindSpecificCell(x, y, out var cell);
            var cc = cell as ChineseChessCell;
            switch (chessPieceType)
            {
                case ChessPieceType.Soldier:
                    return CreateSoldier(side, cc);
                case ChessPieceType.Horse:
                    return CreateHorse(side, cc);
                case ChessPieceType.Cannon:
                    return CreateCannon(side, cc);
                case ChessPieceType.Chariot:
                    return CreateChariot(side, cc);
                case ChessPieceType.Minister:
                    return CreateMinister(side, cc);
                case ChessPieceType.Advisor:
                    return CreateAdvisor(side, cc);
                case ChessPieceType.General:
                    return CreateGeneral(side, cc);
                default:
                    throw new ArgumentOutOfRangeException(nameof(chessPieceType));
            }
        }
        public static ChineseChessPieceBase CreateChessPiece(Side side, ChessPieceType chessPieceType, ChineseChessCell cell)
        {
            switch (chessPieceType)
            {
                case ChessPieceType.Soldier:
                    return CreateSoldier(side, cell);
                case ChessPieceType.Horse:
                    return CreateHorse(side, cell);
                case ChessPieceType.Cannon:
                    return CreateCannon(side, cell);
                case ChessPieceType.Chariot:
                    return CreateChariot(side, cell);
                case ChessPieceType.Minister:
                    return CreateMinister(side, cell);
                case ChessPieceType.Advisor:
                    return CreateAdvisor(side, cell);
                case ChessPieceType.General:
                    return CreateGeneral(side, cell);
                default:
                    throw new ArgumentOutOfRangeException(nameof(chessPieceType));
            }
        }
        public static ChineseChessPieceBase CloneChessPieceToNewLocation(int x, int y, ChineseChessPieceBase oldPiece, ChineseChessBoard chessBoard)
        {
            ChineseChessPieceBase newPiece = CreateChessPiece(x, y, oldPiece.Side, oldPiece.GetChessPieceType(), chessBoard);
            newPiece.Name = oldPiece.Name;
            return newPiece;
        }
        public static ChineseChessPieceBase CloneChessPieceToNewLocation(ChineseChessPieceBase oldPiece, ChineseChessCell newLocation)
        {
            var chessPieceType = oldPiece.GetChessPieceType();
            switch (chessPieceType)
            {
                case ChessPieceType.Soldier:
                    return new Soldier(oldPiece as Soldier, newLocation);
                case ChessPieceType.Horse:
                    return new Horse(oldPiece as Horse, newLocation);
                case ChessPieceType.Cannon:
                    return new Cannon(oldPiece as Cannon, newLocation);
                case ChessPieceType.Chariot:
                    return new Chariot(oldPiece as Chariot, newLocation);
                case ChessPieceType.Minister:
                    return new Minister(oldPiece as Minister, newLocation);
                case ChessPieceType.Advisor:
                    return new Advisor(oldPiece as Advisor, newLocation);
                case ChessPieceType.General:
                    return new General(oldPiece as General, newLocation);
                default:
                    throw new ArgumentOutOfRangeException(nameof(chessPieceType));
            }
        }
        private static ChineseChessPieceBase CreateSoldier(Side side, ChineseChessCell cell)
        {
            return new Soldier(side, cell);
        }
        private static ChineseChessPieceBase CreateHorse(Side side, ChineseChessCell cell)
        {
            return new Horse(side, cell);
        }
        private static ChineseChessPieceBase CreateCannon(Side side, ChineseChessCell cell)
        {
            return new Cannon(side, cell);
        }
        private static ChineseChessPieceBase CreateChariot(Side side, ChineseChessCell cell)
        {
            return new Chariot(side, cell);
        }
        private static ChineseChessPieceBase CreateMinister(Side side, ChineseChessCell cell)
        {
            return new Minister(side, cell);
        }
        private static ChineseChessPieceBase CreateAdvisor(Side side, ChineseChessCell cell)
        {
            return new Advisor(side, cell);
        }
        private static ChineseChessPieceBase CreateGeneral(Side side, ChineseChessCell cell)
        {
            return new General(side, cell);
        }
    }
}
