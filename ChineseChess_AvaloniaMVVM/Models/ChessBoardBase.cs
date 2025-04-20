using ChineseChess_AvaloniaMVVM.Models.ChineseChess;
using GameCommons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ChineseChess_AvaloniaMVVM.Models
{
    public abstract class ChessBoardBase
    {
        public abstract string GameDescription { get; }
        private Side _CurrentPlayerTurn = Side.Red;
        public Side CurrentPlayerTurn { get => _CurrentPlayerTurn; set { _CurrentPlayerTurn = value; } }
        [JsonIgnore]
        public CellBase? SelectedCell { get; set; } = null;
        // The grid is a 2D array of CellBase objects
        CellBase[][] _Grid;
        public CellBase[][] Grid { get => _Grid; set { _Grid = value; } }
        [JsonIgnore]
        public CellBase[] GridArr { get => _Grid is null ? Array.Empty<CellBase>() : _Grid.SelectMany(x => x).ToArray(); }
        public int RowCount { get => _Grid == null ? 0 : _Grid.Length; }
        public int ColumnCount { get => _Grid == null ? 0 : _Grid[0].Length; }
        public int BoardSizeX { get => ColumnCount; }
        public int BoardSizeY { get => RowCount; }

        JsonSerializerSettings _JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };
        bool _loading = false;
        public bool Loading { get => _loading; set => _loading = value; }
        public abstract CellBase[][] InitialiseGrid(int rows, int cols, PropertyChangedEventHandler postChessPieceMove);

        protected abstract void LoadGameBoard(List<string> matchData = null);
        public void SetupGameBoard(List<string> matchData = null)
        {
            _loading = true;
            LoadGameBoard(matchData);
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
        public ChineseChessBoard LoadFromJson(string json)
        {
            _loading = true;
            var loadedJson = JsonConvert.DeserializeObject<ChineseChessBoard>(json, _JsonSettings);
            loadedJson.SelectedCell = null;
            for (int i = 0; i < loadedJson.RowCount; i++)
            {
                for (int j = 0; j < loadedJson.ColumnCount; j++)
                {
                    var cell = loadedJson.Grid[i][j];
                    cell.IsSelected = false;
                    cell.SetCellCoordinates(loadedJson, j, i);
                    if (cell.ChessPiece is not null)
                    {
                        cell.ChessPiece.SetLocation(cell);
                    }
                }
            }
            _loading = false;
            return loadedJson;
        }
        public abstract bool CheckWinner(out Side side);
    }
}
