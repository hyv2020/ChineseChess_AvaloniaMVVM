using GameCommons;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChineseChess_Avalonia
{
    public abstract class ChessPiece
    {
        public string Name { get; set; }
        public readonly int X;
        public readonly int Y;
        public readonly Side Side;
        public bool IsSelected { get; set; }
        public PictureBox ChessPicture { get; set; }
        public bool CanMove { get; set; }
        public ChessPiece(int x, int y, Side side, ChessBoard chessBoard)
        {
            ChessPieceType chessPieceType = this.GetChessPieceType();
            var count = chessBoard.Cells.SelectMany(col => col)
                .Where(cell => cell.ChessPiece.GetChessPieceType() == chessPieceType).Count() + 1;
            this.Name = $"{side}{chessPieceType}{count}";
            this.X = x;
            this.Y = y;
            this.Side = side;
            this.ChessPicture = DrawFunctions.DrawChessPiece(this);
            this.IsSelected = false;
            this.CanMove = false;
        }

        /// <summary>
        /// Constructor for moving piece while keeping the same name
        /// </summary>
        /// <param name="chessPiece"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public ChessPiece(ChessPiece chessPiece, int x, int y)
        {
            this.Name = chessPiece.Name;
            this.X = x;
            this.Y = y;
            this.Side = chessPiece.Side;
            this.IsSelected = false;
            this.CanMove = false;
            this.ChessPicture = DrawFunctions.DrawChessPiece(this);
        }
        public void RemoveChessPiecePic()
        {
            this.ChessPicture.Dispose();
        }
        public abstract IEnumerable<Cell> FindValidMove(ChessBoard chessBoard);
        public IEnumerable<Cell> FliterCellsToValidPoints(IEnumerable<Cell> cells)
        {
            var validateCells = cells.Where(c => c.ChessPiece == null || this.Side != c.ChessPiece.Side);
            return validateCells;
        }
        public override string ToString()
        {
            return typeof(ChessPiece).ToString();
        }
    }
}
