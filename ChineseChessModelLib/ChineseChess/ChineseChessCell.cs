using Avalonia.Media.Imaging;
using ChessModelLib;
using ChineseChessModelLib.ChessPiece.Factory;
using GameCommons;
using System.ComponentModel;
namespace ChineseChessModelLib
{
    public class ChineseChessCell : CellBase
    {
        public bool AdvisorArea { get; }
        public Side Side { get; }
        public ChineseChessCell(int x, int y, string value, ChineseChessBoard chineseChessBoard,
            PropertyChangedEventHandler postChessPieceMove)
            : base(x, y, value, chineseChessBoard, postChessPieceMove)
        {
            Side = (y > 4) ? Side.Red : Side.Black;
            if ((x < 6 && x > 2) && (y < 3 || y > ChessBoard.BoardSizeY - 4))
            {
                AdvisorArea = true;
            }
            else
            {
                AdvisorArea = false;
            }
        }
        public ChineseChessCell(bool advisorZone, Side side)
        {
            AdvisorArea = advisorZone;
            Side = side;
            // Empty constructor for serialization
        }
        public override void ResolveMove()
        {
            var previousCell = ChessBoard.SelectedCell;
            this.ChessPiece = ChessPieceFactory.CloneChessPieceToNewLocation(previousCell.ChessPiece as ChineseChessPieceBase, this);
            ChessBoard.Grid[previousCell.Y][previousCell.X].ChessPiece = null;
            ChessBoard.SelectedCell = null;
        }

        public override string ToSaveCode()
        {
            if (this.ChessPiece is null)
            {
                return "0";
            }
            else
            {
                return $"{(int)this.ChessPiece.Side}{(int)GetChessPiece().GetChessPieceType()}";
            }
        }

        public override ChineseChessPieceBase? GetChessPiece()
        {
            if (this.ChessPiece is null)
            {
                return null;
            }
            else
            {
                return (ChineseChessPieceBase)this.ChessPiece;
            }
        }
        public override Bitmap GetBackgroundImage()
        {
            var bitmap = DrawChineseChessBoardCell.DrawChineseChessBoard(this);
            return bitmap;
        }
    }
}
