using Avalonia.Media.Imaging;
using ChessModelLib;
using ReactiveUI;
using System.ComponentModel;
namespace ChineseChess_AvaloniaMVVM.ViewModels;

public partial class CellViewModel : ViewModelBase
{
    public ChessBoardViewModel ChessBoard { get; }
    public CellViewModel Self { get => this; }
    public CellBase CellBase { get; }
    public Bitmap BackgroundImage { get; }
    Bitmap? _ChessPieceImage;
    public Bitmap? ChessPieceImage { get => _ChessPieceImage; private set { this.RaiseAndSetIfChanged(ref _ChessPieceImage, value, nameof(ChessPieceImage)); } }
    public int X { get => CellBase.X; }
    public int Y { get => CellBase.Y; }
    private ChessPieceViewModel? _ChessPieceVm;
    public ChessPieceViewModel? ChessPieceVm { get => _ChessPieceVm; set { _ChessPieceVm = value; SetChessPieceImage(); } }

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
    public double Height { get; }
    public double Width { get; }
    public CellViewModel(CellBase cell, ChessBoardViewModel chessBoard) : base()
    {
        CellBase = cell;
        BackgroundImage = cell.GetBackgroundImage();
        Height = BackgroundImage.PixelSize.Height;
        Width = BackgroundImage.PixelSize.Width;
        _ChessPieceVm = null;
        CellBase.PropertyChanged += ChessPiece_PropertyChanged;
        ChessBoard = chessBoard;
    }
    public CellViewModel() : base()
    {
        CellBase = null;
        BackgroundImage = null;
        Height = 0;
        Width = 0;
        _ChessPieceVm = null;
    }

    private void SetChessPieceImage()
    {
        ChessPieceImage = _ChessPieceVm is null ? null : _ChessPieceVm.ChessPieceImage;
    }
    private void ChessPiece_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var cell = sender as CellBase;
        if (cell != null)
        {
            if (e.PropertyName == nameof(cell.ChessPiece))
            {
                if (cell.ChessPiece == null)
                {
                    ChessPieceVm = null;
                }
                else
                {
                    ChessPieceVm = new ChessPieceViewModel(cell.ChessPiece);
                }
                ChessPieceIsVisible = _ChessPieceVm != null;
            }
            if (e.PropertyName == nameof(cell.IsValidMove))
            {
            }
        }

    }
    public void ResolveMove()
    {
        CellBase.ResolveMove();
    }



}