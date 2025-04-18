using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public abstract class CellBase : INotifyPropertyChanged
    {
        public ChessBoardBase ChessBoard { get; }
        public int X { get; }
        public int Y { get; }
        private bool _IsValidMove;
        public bool IsValidMove
        {
            get => _IsValidMove; set
            {
                _IsValidMove = value; OnPropertyChanged(nameof(IsValidMove));
            }
        }
        public bool IsSelected { get; set; }
        public string Value { get; set; }
        private ChessPieceBase? _ChessPiece;
        public ChessPieceBase? ChessPiece
        {
            get => _ChessPiece;
            set
            {
                _ChessPiece = value;
                OnChessPieceChanged();
            }
        }
        protected CellBase(int x, int y, string value, ChessBoardBase chessBoard, PropertyChangedEventHandler postChessPieceMove)
        {
            ChessBoard = chessBoard;
            X = x;
            Y = y;
            Value = value;
            IsValidMove = true;
            IsSelected = false;
            PropertyChanged += postChessPieceMove;
            ChessPiece = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract void ResolveMove();
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnChessPieceChanged()
        {
            PropertyChanged?.Invoke(ChessPiece, new PropertyChangedEventArgs(nameof(ChessPiece)));
        }
    }
}
