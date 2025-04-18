using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using GameCommons;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public abstract class ChessPieceBase
    {
        public string Name { get; set; }
        public readonly int X;
        public readonly int Y;
        public readonly Side Side;
        public bool CanMove { get; set; }
        [JsonIgnore]
        public CellBase Location { get; set; }
        protected ChessPieceBase(Side side, CellBase cell)
        {
            Location = cell;

            this.X = cell.X;
            this.Y = cell.Y;
            this.Side = side;
            this.CanMove = false;
        }
        protected ChessPieceBase()
        {

        }
        protected ChessPieceBase(ChessPieceBase oldPiece, CellBase cell)
        {
            Location = cell;
            this.Name = oldPiece.Name;
            this.X = cell.X;
            this.Y = cell.Y;
            this.Side = oldPiece.Side;
            this.CanMove = false;
        }
        public override string ToString()
        {
            return typeof(ChineseChessPieceBase).ToString();
        }
        public abstract IEnumerable<CellBase> FindValidMove();

    }
}
