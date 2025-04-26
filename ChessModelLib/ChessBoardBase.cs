using GameCommons;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChessModelLib
{
    public abstract class ChessBoardBase
    {
        public bool ActiveGame { get; set; } = true;
        public abstract string GameDescription { get; }
        private Side _CurrentPlayerTurn = Side.Red;
        public Side CurrentPlayerTurn { get => _CurrentPlayerTurn; set { _CurrentPlayerTurn = value; } }
        public CellBase? SelectedCell { get; set; } = null;
        // The grid is a 2D array of CellBase objects
        CellBase[][] _Grid;
        public CellBase[][] Grid { get => _Grid; set { _Grid = value; } }
        public CellBase[] GridArr { get => _Grid is null ? Array.Empty<CellBase>() : _Grid.SelectMany(x => x).ToArray(); }
        public int RowCount { get => _Grid == null ? 0 : _Grid.Length; }
        public int ColumnCount { get => _Grid == null ? 0 : _Grid[0].Length; }
        public int BoardSizeX { get => ColumnCount; }
        public int BoardSizeY { get => RowCount; }
        public abstract ReadOnlyCollection<string> DefaultMatchData { get; }

        bool _loading = false;
        public bool Loading { get => _loading; set => _loading = value; }
        public abstract CellBase[][] InitialiseGrid(int rows, int cols, PropertyChangedEventHandler postChessPieceMove);

        protected void LoadGameBoard(List<string> defaultMatchData, List<string> matchData = null)
        {
            matchData = matchData ?? defaultMatchData;
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
                    LoadChessPieceToCell(board[y][x], this.Grid[y][x]);
                }
            }
        }
        protected abstract void LoadChessPieceToCell(string chessPieceCode, CellBase cell);
        public void SetupGameBoard(List<string> matchData = null)
        {
            _loading = true;
            LoadGameBoard(DefaultMatchData.ToList(), matchData);
            _loading = false;
        }
        public abstract IEnumerable<string> SaveGame();

        protected ChessBoardBase(int rows, int cols, PropertyChangedEventHandler postChessPieceMove)
        {
            _loading = true;
            _Grid = InitialiseGrid(rows, cols, postChessPieceMove);
            _loading = false;
        }
        public void ClearAllValidMoves()
        {
            foreach (var cell in GridArr)
            {
                cell.IsValidMove = false;
                cell.IsSelected = false;
            }
        }
        public bool FindSpecificCell(int x, int y, out CellBase cell)
        {
            if (x < 0 || y < 0 || x > ColumnCount - 1 || y > RowCount - 1)
            {
                cell = null;
                return false;
            }
            try
            {
                cell = _Grid[y][x];
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ClearBoard()
        {
            _loading = true;
            foreach (var cell in GridArr)
            {
                cell.IsValidMove = false;
                cell.IsSelected = false;
                cell.ChessPiece = null;
            }
            _loading = false;
        }
        public abstract bool CheckWinner(out Side side);
    }
}
