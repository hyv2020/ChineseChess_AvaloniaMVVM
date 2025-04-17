using Avalonia.Controls;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public abstract class CellBase : INotifyPropertyChanged
    {
        public ChessBoardBase ChessBoard { get; }
        public int X { get; }
        public int Y { get; }
        public bool IsValidMove { get; set; }
        public bool IsSelected { get; set; }
        public Image BackgroundImage { get; }
        public string Value { get; set; }
        public ChessPieceBase? ChessPiece { get; set; }
        public bool IsEmpty => ChessPiece == null;
        protected CellBase(int x, int y, string value, ChessBoardBase chessBoard, PropertyChangedEventHandler postChessPieceMove)
        {
            ChessBoard = chessBoard;
            X = x;
            Y = y;
            Value = value;
            IsValidMove = false;
            IsSelected = false;
            PropertyChanged += postChessPieceMove;
            BackgroundImage = GetBackgroundImage();
            ChessPiece = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract void ResolveMove();
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public abstract Image GetBackgroundImage();
    }
}
