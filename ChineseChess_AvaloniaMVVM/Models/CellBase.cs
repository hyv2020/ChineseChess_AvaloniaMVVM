using Newtonsoft.Json;
using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public abstract class CellBase : INotifyPropertyChanged
    {
        private ChessBoardBase _ChessBoard;
        [JsonIgnore]
        public ChessBoardBase ChessBoard { get => _ChessBoard; }
        private int _X;
        private int _Y;
        public int X { get; private set; }
        public int Y { get; private set; }
        private bool _IsValidMove;
        public bool IsValidMove
        {
            get => _IsValidMove; set
            {
                _IsValidMove = value; OnPropertyChanged(nameof(IsValidMove));
            }
        }
        private bool _IsSelected { get; set; }
        public bool IsSelected
        {
            get => _IsSelected; set
            {
                _IsSelected = value; OnPropertyChanged(nameof(IsSelected));
            }
        }
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
            _ChessBoard = chessBoard;
            X = x;
            Y = y;
            Value = value;
            IsValidMove = true;
            IsSelected = false;
            PropertyChanged += postChessPieceMove;
            ChessPiece = null;
        }
        protected CellBase()
        {

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract void ResolveMove();
        public abstract string ToSaveCode();
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnChessPieceChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChessPiece)));
        }
        public abstract ChessPieceBase? GetChessPiece();
        internal void SetCellCoordinates(ChessBoardBase chessBoard, int x, int y)
        {
            _ChessBoard = chessBoard;
            X = x;
            Y = y;
        }
    }
}
