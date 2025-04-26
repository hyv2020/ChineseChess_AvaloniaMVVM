using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ChineseChessModelLib.ChessPiece.Factory;

namespace ChineseChessModelLib
{
    internal static class DrawChineseChessPiece
    {
        const string _ChineseChessPieceImageRootFilePath = "avares://ChineseChess_AvaloniaMVVM/Assets/ChessPiecesImages/ChineseChess/";

        internal static Bitmap DrawImage(ChineseChessPieceBase chineseChessPieceBase)
        {
            var chessPieceType = chineseChessPieceBase.GetChessPieceType();
            var side = chineseChessPieceBase.Side;
            var imagePath = $"{_ChineseChessPieceImageRootFilePath}{side}{chessPieceType}.gif";
            var image = new Bitmap(AssetLoader.Open(new Uri(imagePath)));
            return image;
        }
    }
}
