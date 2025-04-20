using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using GameCommons;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public abstract class ChessPieceBase
    {
        public string Name { get; set; }
        public int X { get => Location.X; }
        public int Y { get => Location.Y; }
        public readonly Side Side;
        public bool CanMove { get => _Location.ChessBoard.CurrentPlayerTurn == Side; }
        CellBase _Location;
        [JsonIgnore]
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
            return typeof(ChineseChessPieceBase).ToString();
        }
        public abstract IEnumerable<CellBase> FindValidMove();
        internal void SetLocation(CellBase cell)
        {
            _Location = cell;
        }
    }
}
