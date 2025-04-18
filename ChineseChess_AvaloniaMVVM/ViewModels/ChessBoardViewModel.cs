using ChineseChess_AvaloniaMVVM.Models;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessBoardViewModel
{
    private ChessBoardBase ChessBoard { get; }
    public List<CellViewModel> GridVm { get; private set; }
    public int ColumnCount => ChessBoard.ColumnCount;
    public ChessBoardViewModel(ChessBoardBase chessBoard)
    {
        ChessBoard = chessBoard;
        GridVm = new();
        foreach (var cell in ChessBoard.GridArr)
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
        ChessBoard.ClearBoard();
    }
    public void LoadGame(List<string> matchData = null)
    {
        ChessBoard.LoadGame(matchData);
    }
    public void SaveGame()
    {
        ChessBoard.SaveGame();
    }
}