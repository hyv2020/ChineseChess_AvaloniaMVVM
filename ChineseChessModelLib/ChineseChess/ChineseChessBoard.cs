using ChessModelLib;
using ChineseChessModelLib.ChessPiece.Factory;
using ChineseChessModelLib.Enum;
using GameCommons;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ChineseChessModelLib
{
    public class ChineseChessBoard : ChessBoardBase
    {
        public override string GameDescription => "Chinese Chess Game Description";

        public override ReadOnlyCollection<string> DefaultMatchData => GameCommons.DefaultVariables.ChineseChessDefaultBoardStart.AsReadOnly();

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

        public override bool CheckWinner(out Side side)
        {
            var allGenerals = GridArr.Where(x => x.ChessPiece != null &&
            ((ChineseChessPieceBase)x.ChessPiece).GetChessPieceType() == ChessPieceType.General);
            var generalCount = allGenerals.Count();
            if (generalCount == 1)
            {
                side = allGenerals.Select(g => ((ChineseChessPieceBase)g.ChessPiece).Side).Single();
                return true;
            }
            // double ko is impossible
            if (generalCount == 0)
            {
                throw new System.Exception("No generals on the board");
            }
            side = Side.Red;
            return false;
        }

        protected override void LoadChessPieceToCell(string chessPieceCode, CellBase cell)
        {
            char[] arr = chessPieceCode.ToCharArray();
            if (arr.Length > 1)
            {
                Side side = (Side)System.Enum.Parse(typeof(Side), arr.First().ToString());
                ChessPieceType chessPieceType = (ChessPieceType)System.Enum.Parse(typeof(ChessPieceType), arr.Last().ToString());

                cell.ChessPiece = ChessPieceFactory.CreateChessPiece(side, chessPieceType,
                    cell as ChineseChessCell);
            }
            else
            {
                cell.ChessPiece = null;
            }
        }
    }
}
