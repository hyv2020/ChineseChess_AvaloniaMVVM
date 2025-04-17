using System.ComponentModel;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public class ChineseChessBoard : ChessBoardBase
    {
        public ChineseChessBoard(PropertyChangedEventHandler postChessPieceMove, int rows = 10, int cols = 10) : base(rows, cols, postChessPieceMove)
        {
        }
        public override CellBase[][] InitialiseGrid(int rows, int cols, PropertyChangedEventHandler postChessPieceMove)
        {
            // Initialize a 10x10 grid
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
