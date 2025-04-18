using Avalonia.Media.Imaging;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess;

namespace ChineseChess_AvaloniaMVVM.ViewModels.ChineseChess
{
    internal static class DrawChineseChessBoardCell
    {
        const string _ChineseChessBoardImageRootFilePath = "Assets/BoardImages/ChineseChess/";
        static int boardSizeX;
        static int boardSizeY;
        public static Bitmap DrawChineseChessBoard(ChineseChessCell cell)
        {
            int x = cell.X;
            int y = cell.Y;
            boardSizeX = cell.ChessBoard.BoardSizeX;
            boardSizeY = cell.ChessBoard.BoardSizeY;
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

        private static Bitmap DrawTopEdge(int x, int y)
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

        private static Bitmap DrawBottomEdge(int x, int y)
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

        private static Bitmap DrawSideEdge(int x, int y)
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

        private static Bitmap DrawRiver(int x, int y)
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

        private static Bitmap DrawCentreTile()
        {
            //fill empty tile with centre tile
            return new Bitmap(_ChineseChessBoardImageRootFilePath + "chinese tile.gif");
        }
    }
}
