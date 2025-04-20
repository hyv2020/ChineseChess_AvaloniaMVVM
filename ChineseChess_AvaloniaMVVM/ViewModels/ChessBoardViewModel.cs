using ChineseChess_AvaloniaMVVM.Models;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessBoardViewModel : ViewModelBase
{
    private ChessBoardBase _ChessBoard { get; }
    public List<CellViewModel> GridVm { get; private set; }
    public int ColumnCount => _ChessBoard.ColumnCount;
    public bool Loading { get => _ChessBoard.Loading; set { _ChessBoard.Loading = value; } }
    public ChessBoardViewModel(ChessBoardBase chessBoard)
    {
        _ChessBoard = chessBoard;
        GridVm = new();
        foreach (var cell in _ChessBoard.GridArr)
        {
            GridVm.Add(new CellViewModel(cell, this));
        }
    }
    public void ClearAllValidMoves()
    {
        foreach (var cell in GridVm)
        {
            cell.IsValidMove = false;
            cell.CellBase.IsSelected = false;
        }
    }
    public void ClearBoard()
    {
        _ChessBoard.ClearBoard();
    }
    public void LoadGame(List<string> matchData = null)
    {
        _ChessBoard.SetupGameBoard(matchData);
    }
    public void SaveGame()
    {
        _ChessBoard.SaveGame();
    }
}