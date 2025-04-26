using Avalonia.Media.Imaging;
using GameCommons;

namespace ChessModelLib
{
    public abstract class ChessPieceBase
    {
        public string Name { get; set; }
        public int X { get => Location.X; }
        public int Y { get => Location.Y; }
        public readonly Side Side;
        public bool CanMove { get => _Location.ChessBoard.CurrentPlayerTurn == Side; }
        CellBase _Location;
        public CellBase Location { get => _Location; }
        protected ChessPieceBase(Side side, CellBase cell)
        {
            _Location = cell;
            this.Side = side;
        }
        protected ChessPieceBase()
        {

        }
        protected ChessPieceBase(ChessPieceBase oldPiece, CellBase cell)
        {
            _Location = cell;
            this.Name = oldPiece.Name;
            this.Side = oldPiece.Side;
        }
        public override string ToString()
        {
            return GetType().ToString();
        }
        public abstract IEnumerable<CellBase> FindValidMove();
        internal void SetLocation(CellBase cell)
        {
            _Location = cell;
        }
        public abstract Bitmap GetChessPieceImage();

    }
}
