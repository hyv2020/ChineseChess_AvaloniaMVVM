using ChineseChess_AvaloniaMVVM.Models.ChineseChess.ChessPiece.Factory;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Enum;
using GameCommons;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ChineseChess_AvaloniaMVVM.Models.ChineseChess
{
    public class ChineseChessBoard : ChessBoardBase
    {
        public ChineseChessBoard(PropertyChangedEventHandler postChessPieceMove, int rows = 10, int cols = 9) : base(rows, cols, postChessPieceMove)
        {
        }

        public override CellBase[][] InitialiseGrid(int rows, int cols, PropertyChangedEventHandler postChessPieceMove)
        {
            // Initialize a 9x9 grid
            var grid = new CellBase[rows][];
            for (int i = 0; i < rows; i++)
            {
                var row = new CellBase[cols];
                for (int j = 0; j < cols; j++)
                {
                    var cell = new ChineseChessCell(j, i, string.Empty, this, postChessPieceMove);
                    row[j] = cell;

                }
                grid[i] = row;
            }
            return grid;
        }

        public override void LoadGame(List<string> matchData = null)
        {
            matchData = matchData ?? GameCommons.DefaultVariables.defaultBoardStart;
            List<List<string>> board = new List<List<string>>();
            foreach (var row in matchData)
            {
                List<string> rowData = row.Split(' ').ToList();
                board.Add(rowData);
            }
            for (int y = 0; y < board.Count; y++)
            {
                for (int x = 0; x < board[y].Count; x++)
                {
                    char[] cell = board[y][x].ToCharArray();
                    if (cell.Length > 1)
                    {
                        Side side = (Side)System.Enum.Parse(typeof(Side), cell.First().ToString());
                        ChessPieceType chessPieceType = (ChessPieceType)System.Enum.Parse(typeof(ChessPieceType), cell.Last().ToString());

                        this.Grid[y][x].ChessPiece = ChessPieceFactory.CreateChessPiece(side, chessPieceType,
                            this.Grid[y][x] as ChineseChessCell);
                    }
                    else
                    {
                        this.Grid[y][x].ChessPiece = null;
                    }

                }
            }
        }
        public override IEnumerable<string> SaveGame()
        {
            /*var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
            };
            var save = JsonConvert.SerializeObject(this, jsonSettings);
            var loadedJson = JsonConvert.DeserializeObject<ChineseChessBoard>(save, jsonSettings);*/
            List<string> saveData = new List<string>();
            var allCellsInGroupsOfX = GridArr.GroupBy(c => c.Y);
            foreach (var group in allCellsInGroupsOfX)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var cell in group)
                {
                    sb.Append(cell.ToSaveCode() + " ");
                }
                sb.Remove(sb.Length - 1, 1);
                yield return sb.ToString();
            }
        }
    }
}
