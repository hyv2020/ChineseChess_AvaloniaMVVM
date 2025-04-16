using GameCommons;
using System.Drawing;
using System.Windows.Forms;

namespace ChineseChess_Avalonia
{
    public static class DrawFunctions
    {
        const int XOffset = 25;
        const int YOffset = 25;
        /// <summary>
        /// size of chess piece
        /// </summary>
        const int ChessSize = 55;
        //size of each square
        const int CellSize = 65;
        //size of legal move indicator
        const int LegalMoveBoxSize = 25;

        //extra padding to make everything snip to grid
        const int ChessToCell = (CellSize - ChessSize) / 2;
        const int IndicatorToCell = (CellSize - LegalMoveBoxSize) / 2;

        static string rootBoardImageFilePath = FilePaths.rootBoardImageFilePath;
        static int boardSizeX = ChessBoard.BoardSizeX;
        static int boardSizeY = ChessBoard.BoardSizeY;

        public static PictureBox DrawBoard(int x, int y)
        {

            //show each cell as picturebox
            //make new picturebox call chessCell
            PictureBox boardCell = new PictureBox
            {
                //properties of picturebox
                //change name of the picturebox
                Name = "boardTile" + x + y,
                //size of the box
                Size = new Size(CellSize, CellSize),
                //colour of the box
                BackColor = Color.Blue,
                //the location changes to make the whole board
                Location = new Point(XOffset + x * (CellSize),
                YOffset + y * (CellSize)),
                //border style
                BorderStyle = BorderStyle.None,
                //stretch image to fit in box
                SizeMode = PictureBoxSizeMode.StretchImage,
                //show the box
                Visible = true,
            };
            //draw the board with pictureBox tiles
            //draw the top edge and adviser area
            DrawTopEdge(boardCell, x, y);
            //draw the bottom edge and adviser area
            DrawBottomEdge(boardCell, x, y);
            //draw the sides
            DrawSideEdge(boardCell, x, y);
            //draw the river
            DrawRiver(boardCell, x, y);
            //fill the rest of the board with tiles
            DrawCentreTile(boardCell);

            return boardCell;
        }
        private static void DrawTopEdge(PictureBox chessCell, int x, int y)
        {
            //draw top edge

            // draw left side of top edge
            if (x > 0 && x < 3 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }

            //draw the middle bit where the general is
            else if (x == 4 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }

            // draw right side of top edge
            else if (x > 5 && x < boardSizeX - 1 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }

            //draw top left corner
            else if (x == 0 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top left.gif");
            }

            //draw top right corner
            else if (x == boardSizeX - 1 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top right.gif");
            }

            //draw top advisor area
            //left of area
            else if (x == 3 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area left edge.gif");
            }
            //right of area
            else if (x == 5 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area right edge.gif");
            }
            //the centre of area
            else if (x == 4 && y == 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile advisor area centre.gif");
            }
            //bottom left of area
            else if (x == 3 && y == 2)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area bottom left.gif");
            }
            //bottom right of area
            else if (x == 5 && y == 2)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area bottom right.gif");
            }
        }

        private static void DrawBottomEdge(PictureBox chessCell, int x, int y)
        {
            // draw left side of bottom edge
            if (x > 0 && x < 3 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }

            //draw the middle bit where the general is
            else if (x == 4 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }

            // draw right side of bottom edge
            else if (x > 5 && x < boardSizeX - 1 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }

            //draw bottom left corner
            else if (x == 0 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom left.gif");
            }

            //draw bottom right corner
            else if (x == boardSizeX - 1 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom right.gif");
            }

            //draw bottom advisor area
            //left of area
            else if (x == 3 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area left edge.gif");
            }
            //right of area
            else if (x == 5 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area right edge.gif");
            }
            //the centre of area
            else if (x == 4 && y == boardSizeY - 2)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile advisor area centre.gif");
            }
            //top left of area
            else if (x == 3 && y == boardSizeY - 3)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area top left.gif");
            }
            //top right of area
            else if (x == 5 && y == boardSizeY - 3)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area top right.gif");
            }
        }

        private static void DrawSideEdge(PictureBox chessCell, int x, int y)
        {
            //draw left edge
            if (x == 0 && y > 0 && y < boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile left edge.gif");
            }

            //draw right edge
            else if (x == boardSizeX - 1 && y > 0 && y < boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile right edge.gif");
            }
        }

        private static void DrawRiver(PictureBox chessCell, int x, int y)
        {
            //top side of river
            if (y == 4 && x > 0 && x < boardSizeX - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }
            //bottom side of river
            else if (y == 5 && x > 0 && x < boardSizeX - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }
        }

        private static void DrawCentreTile(PictureBox chessCell)
        {
            //fill empty tile with centre tile
            if (chessCell.Image == null)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile.gif");
            }
        }
        public static PictureBox DrawLegalMoveIndictor(int x, int y)
        {
            PictureBox legalMoveIndicator = new PictureBox
            {
                //properties of picturebox
                //change name of the picturebox
                //use colour and type of the piece
                //for example "red horse"
                Name = "legalMoveIndicator" + x + y,
                //size of the legal move box
                Size = new Size(LegalMoveBoxSize, LegalMoveBoxSize),
                //colour of the box
                BackColor = Color.Black,
                //border style
                BorderStyle = BorderStyle.None,
                //stretch image to fit in box
                SizeMode = PictureBoxSizeMode.StretchImage,
                //location
                Location = new Point(XOffset + IndicatorToCell + x * (CellSize),
                        YOffset + IndicatorToCell + y * (CellSize)),
                //hide the box
                Visible = false
            };
            legalMoveIndicator.BringToFront();
            return legalMoveIndicator;
        }

        public static PictureBox DrawChessPiece(ChessPiece chessPiece)
        {
            //ideally it could use the location of the cell of the board to snap to the grid
            //there is a loop to determine its location and draw
            //the loop look through ChessPieceList to find out how many it needs to draw
            //the location is set depending on colour and piece type

            //show each cell as picturebox
            //make new picturebox call chessCell
            PictureBox chessPictureBox = new PictureBox
            {
                //properties of picturebox
                //change name of the picturebox
                //use colour and type of the piece
                //for example "red horse"
                Name = chessPiece.Name,
                //size of the chess piece box
                Size = new Size(ChessSize, ChessSize),
                //colour of the box
                BackColor = Color.White,
                //border style
                BorderStyle = BorderStyle.None,
                //stretch image to fit in box
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(FilePaths.rootChessImageFilePath + $"{chessPiece.Side}{chessPiece.GetChessPieceType()}.gif"),
                Location = new Point(XOffset + ChessToCell + chessPiece.X * CellSize, YOffset + ChessToCell + chessPiece.Y * CellSize),
                //show the box
                Visible = true

            };
            chessPictureBox.BringToFront();
            return chessPictureBox;
        }

    }
}
