using Avalonia.Media.Imaging;
using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessPieceViewModel : ViewModelBase
{
    public ChessPieceBase ChessPiece { get; }

    public Bitmap ChessPieceImage { get; }
    public ChessPieceViewModel(ChessPieceBase chessPiece)
    {
        ChessPiece = chessPiece;
        ChessPieceImage = GetChessPieceImage();
    }
    protected Bitmap GetChessPieceImage()
    {
        if (ChessPiece is ChineseChessPieceBase chineseChessPiece)
        {
            Bitmap chessPieceImage = null;
            return chessPieceImage;
        }
        return null;
    }
}