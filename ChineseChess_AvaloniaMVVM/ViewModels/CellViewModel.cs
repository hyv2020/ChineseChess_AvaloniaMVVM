using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess;
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
    const string _ChineseChessBoardImageRootFilePath = "Assets/BoardImages/ChineseChess/";
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
            var bitmap = DrawChineseChessBoard();
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

    public Bitmap DrawChineseChessBoard()
    {
        int x = this.X;
        int y = this.Y;
        //draw the board with pictureBox tiles
        //draw the top edge and adviser area
        var topTile = DrawTopEdge(x, y);
        if (topTile != null)
        {
            return topTile;
        }
        //draw the bottom edge and adviser area
        var bottomEdge = DrawBottomEdge(x, y);
        if (bottomEdge != null)
        {
            return bottomEdge;
        }
        //draw the sides
        var sideEdge = DrawSideEdge(x, y);
        if (sideEdge != null)
        {
            return sideEdge;
        }
        //draw the river
        var river = DrawRiver(x, y);
        if (river != null)
        {
            return river;
        }
        //fill the rest of the board with tiles
        return DrawCentreTile();
    }

    private Bitmap DrawTopEdge(int x, int y)
    {
        //draw top edge

        // draw left side of top edge
        if (x > 0 && x < 3 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top edge.gif");
        }

        //draw the middle bit where the general is
        else if (x == 4 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top edge.gif");
        }

        // draw right side of top edge
        else if (x > 5 && x < boardSizeX - 1 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top edge.gif");
        }

        //draw top left corner
        else if (x == 0 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top left.gif");
        }

        //draw top right corner
        else if (x == boardSizeX - 1 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top right.gif");
        }

        //draw top advisor area
        //left of area
        else if (x == 3 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top advisor area left edge.gif");
        }
        //right of area
        else if (x == 5 && y == 0)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top advisor area right edge.gif");
        }
        //the centre of area
        else if (x == 4 && y == 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile advisor area centre.gif");
        }
        //bottom left of area
        else if (x == 3 && y == 2)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top advisor area bottom left.gif");
        }
        //bottom right of area
        else if (x == 5 && y == 2)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top advisor area bottom right.gif");
        }
        return null;
    }

    private Bitmap DrawBottomEdge(int x, int y)
    {
        // draw left side of bottom edge
        if (x > 0 && x < 3 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom edge.gif");
        }

        //draw the middle bit where the general is
        else if (x == 4 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom edge.gif");
        }

        // draw right side of bottom edge
        else if (x > 5 && x < boardSizeX - 1 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom edge.gif");
        }

        //draw bottom left corner
        else if (x == 0 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom left.gif");
        }

        //draw bottom right corner
        else if (x == boardSizeX - 1 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom right.gif");
        }

        //draw bottom advisor area
        //left of area
        else if (x == 3 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom advisor area left edge.gif");
        }
        //right of area
        else if (x == 5 && y == boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom advisor area right edge.gif");
        }
        //the centre of area
        else if (x == 4 && y == boardSizeY - 2)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile advisor area centre.gif");
        }
        //top left of area
        else if (x == 3 && y == boardSizeY - 3)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom advisor area top left.gif");
        }
        //top right of area
        else if (x == 5 && y == boardSizeY - 3)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom advisor area top right.gif");
        }
        return null;
    }

    private Bitmap DrawSideEdge(int x, int y)
    {
        //draw left edge
        if (x == 0 && y > 0 && y < boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile left edge.gif");
        }

        //draw right edge
        else if (x == boardSizeX - 1 && y > 0 && y < boardSizeY - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile right edge.gif");
        }
        return null;
    }

    private Bitmap DrawRiver(int x, int y)
    {
        //top side of river
        if (y == 4 && x > 0 && x < boardSizeX - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile bottom edge.gif");
        }
        //bottom side of river
        else if (y == 5 && x > 0 && x < boardSizeX - 1)
        {
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile top edge.gif");
        }
        return null;
    }

    private Bitmap DrawCentreTile()
    {
        //fill empty tile with centre tile
        return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile.gif");
    }

}