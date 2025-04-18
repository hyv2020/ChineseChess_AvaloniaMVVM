using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess
{
    public class ChineseChessBoard : ChessBoardBase
    {
        public ChineseChessBoard(PropertyChangedEventHandler postChessPieceMove, int rows = 9, int cols = 9) : base(rows, cols, postChessPieceMove)
        {
        }
        public override CellBase[][] InitialiseGrid(int rows, int cols, PropertyChangedEventHandler postChessPieceMove)
        {
            // Initialize a 9x9 grid
            var grid = new CellBase[cols][];
            for (int i = 0; i < cols; i++)
            {
                var row = new CellBase[rows];
                for (int j = 0; j < rows; j++)
                {
                    var cell = new ChineseChessCell(j, i, string.Empty, this, postChessPieceMove);
                    row[j] = cell;

                }
                grid[i] = row;
            }
            return grid;
        }
    }
}
