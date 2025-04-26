using Avalonia.Media.Imaging;
using ChessModelLib;

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
        ChessPieceImage = chessPiece.GetChessPieceImage();
        Height = ChessPieceImage.PixelSize.Height;
        Width = ChessPieceImage.PixelSize.Width;
    }

}