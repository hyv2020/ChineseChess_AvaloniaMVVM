using Avalonia.Media.Imaging;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;

namespace ChineseChess_AvaloniaMVVM.ViewModels.ChineseChess
{
    internal static class DrawChineseChessPiece
    {
        const string _ChineseChessPieceImageRootFilePath = "Assets/ChessPiecesImages/ChineseChess/";

        internal static Bitmap DrawImage(ChineseChessPieceBase chineseChessPieceBase)
        {
            var chessPieceType = chineseChessPieceBase.GetChessPieceType();
            var side = chineseChessPieceBase.Side;
            var imagePath = $"{_ChineseChessPieceImageRootFilePath}{side}{chessPieceType}.gif";
            var image = new Bitmap(imagePath);
            return image;
        }
    }
}
