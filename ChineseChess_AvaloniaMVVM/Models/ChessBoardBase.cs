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
        public Side CurrentPlayerTurn { get; set; } = Side.Red;
        [JsonIgnore]
        public CellBase? SelectedCell { get; set; } = null;
        // The grid is a 2D array of CellBase objects
        CellBase[][] _Grid;
        public CellBase[][] Grid { get => _Grid; set { _Grid = value; } }
        [JsonIgnore]
        public CellBase[] GridArr { get => _Grid is null ? Array.Empty<CellBase>() : _Grid.SelectMany(x => x).ToArray(); }
        public int RowCount { get; }
        public int ColumnCount { get; }
        public int BoardSizeX { get => ColumnCount; }
        public int BoardSizeY { get => RowCount; }
        public abstract CellBase[][] InitialiseGrid(int rows, int cols, PropertyChangedEventHandler postChessPieceMove);
        public abstract void LoadGame(List<string> matchData = null);
        public abstract IEnumerable<string> SaveGame();

        protected ChessBoardBase(int rows, int cols, PropertyChangedEventHandler postChessPieceMove)
        {
            RowCount = rows;
            ColumnCount = cols;
            _Grid = InitialiseGrid(rows, cols, postChessPieceMove);
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
                var allCells = GridArr;
                cell = allCells.Single(c => c.X == x && c.Y == y);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ClearBoard()
        {
            foreach (var cell in GridArr)
            {
                cell.IsValidMove = false;
                cell.IsSelected = false;
                cell.ChessPiece = null;
            }
        }
    }
}
