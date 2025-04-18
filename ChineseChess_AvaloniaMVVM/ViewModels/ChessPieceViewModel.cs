using Avalonia.Media.Imaging;
using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using ChineseChess_AvaloniaMVVM.ViewModels.ChineseChess;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessPieceViewModel : ViewModelBase
{
    public ChessPieceBase ChessPiece { get; }
    public double Height { get; }
    public double Width { get; }
    public Bitmap ChessPieceImage { get; }
    public ChessPieceViewModel(ChessPieceBase chessPiece)
    {
        ChessPiece = chessPiece;
        ChessPieceImage = GetChessPieceImage();
        Height = ChessPieceImage.PixelSize.Height;
        Width = ChessPieceImage.PixelSize.Width;
    }
    protected Bitmap GetChessPieceImage()
    {
        if (ChessPiece is ChineseChessPieceBase chineseChessPiece)
        {
            Bitmap chessPieceImage = DrawChineseChessPiece.DrawImage(chineseChessPiece);
            return chessPieceImage;
        }
        return null;
    }
}