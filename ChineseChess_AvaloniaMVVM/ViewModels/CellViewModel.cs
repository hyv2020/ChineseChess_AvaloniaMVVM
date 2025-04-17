using Avalonia.Controls;
using ChineseChess_AvaloniaMVVM.Models;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class CellViewModel
{
    public CellBase Cell { get; }
    public Image BackgroundImage { get; }
    public int X => Cell.X;
    public int Y => Cell.Y;
    private ChessPieceViewModel? _ChessPieceVm;
    public ChessPieceViewModel? ChessPieceVm { get => _ChessPieceVm; }
    public bool IsEmpty => ChessPieceVm == null;

    public CellViewModel(CellBase cell)
    {
        Cell = cell;
        BackgroundImage = GetBackgroundImage();
        _ChessPieceVm = null;
        cell.PropertyChanged += ChessPiece_PropertyChanged;
    }

    private Image GetBackgroundImage()
    {
        Image backgroundImage = null;
        return backgroundImage;
    }

    private void ChessPiece_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var chessPiece = sender as ChessPieceBase;
        if (chessPiece != null)
        {
            if (chessPiece != null)
            {
                _ChessPieceVm = new ChessPieceViewModel(chessPiece);
            }
            else
            {
                _ChessPieceVm = null;
            }
        }

    }
    public void ResolveMove()
    {
        Cell.ResolveMove();
    }
    public void OnPropertyChanged(string propertyName)
    {
        Cell.OnPropertyChanged(propertyName);
    }
}