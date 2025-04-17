using Avalonia.Controls;
using ChineseChess_AvaloniaMVVM.Models;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessPieceViewModel
{
    public ChessPieceBase ChessPiece { get; }

    public Image ChessPieceImage { get; }
    public ChessPieceViewModel(ChessPieceBase chessPiece)
    {
        ChessPiece = chessPiece;
        ChessPieceImage = null;
    }
}