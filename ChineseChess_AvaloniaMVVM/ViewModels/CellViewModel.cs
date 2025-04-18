using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess;
using ChineseChess_AvaloniaMVVM.ViewModels.ChineseChess;
using System.ComponentModel;
namespace ChineseChess_AvaloniaMVVM.ViewModels;

public partial class CellViewModel : ViewModelBase
{
    public ChessBoardViewModel ChessBoard { get; }
    public CellViewModel Self { get => this; }
    public CellBase CellBase { get; }
    public Bitmap BackgroundImage { get; }
    public Bitmap? ChessPieceImage { get => _ChessPieceVm is null ? null : _ChessPieceVm.ChessPieceImage; }
    public Bitmap ValidMoveImage { get; }
    public int X { get => CellBase.X; }
    public int Y { get => CellBase.Y; }
    private ChessPieceViewModel? _ChessPieceVm;
    public ChessPieceViewModel? ChessPieceVm { get => _ChessPieceVm; }

    bool _ChessPieceIsVisible { get; set; }
    public bool ChessPieceIsVisible
    {
        get => _ChessPieceIsVisible;
        set
        {
            _ChessPieceIsVisible = value;
            OnPropertyChanged(nameof(_ChessPieceIsVisible));
        }
    }

    public bool IsEmpty { get => ChessPieceVm == null; }
    private bool _IsValidMove { get => CellBase.IsValidMove; set { CellBase.IsValidMove = value; } }
    public bool IsValidMove
    {
        get => _IsValidMove; set
        {
            _IsValidMove = value;
        }
    }
    static int boardSizeX;
    static int boardSizeY;
    public double Height { get; }
    public double Width { get; }
    public CellViewModel(CellBase cell, ChessBoardViewModel chessBoard) : base()
    {
        CellBase = cell;
        boardSizeX = cell.ChessBoard.ColumnCount;
        boardSizeY = cell.ChessBoard.RowCount;
        BackgroundImage = GetBackgroundImage();
        Height = BackgroundImage.PixelSize.Height;
        Width = BackgroundImage.PixelSize.Width;
        _ChessPieceVm = null;
        cell.PropertyChanged += ChessPiece_PropertyChanged;
        ChessBoard = chessBoard;
    }

    private Bitmap GetBackgroundImage()
    {
        if (CellBase is ChineseChessCell chineseChessCell)
        {
            Image backgroundImage = new Image();
            var bitmap = DrawChineseChessBoardCell.DrawChineseChessBoard(chineseChessCell);
            return bitmap;
        }
        return null;
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
        OnPropertyChanged(nameof(ChessPieceImage));
        ChessPieceIsVisible = _ChessPieceVm != null;
    }
    public void ResolveMove()
    {
        CellBase.ResolveMove();
    }



}