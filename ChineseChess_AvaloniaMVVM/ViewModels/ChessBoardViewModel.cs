using ChessModelLib;
using GameCommons;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessBoardViewModel : ViewModelBase
{
    public string GameMode => _ChessBoard.GetType().Name;
    public string GameDescription { get => _ChessBoard.GameDescription; }
    public bool ActiveGame { get => _ChessBoard.ActiveGame; set { _ChessBoard.ActiveGame = value; } }
    private ChessBoardBase _ChessBoard { get; }
    public List<CellViewModel> GridVm { get; private set; }
    public int ColumnCount => _ChessBoard.ColumnCount;
    public bool Loading { get => _ChessBoard.Loading; set { _ChessBoard.Loading = value; } }
    public Side CurrentPlayerTurn
    {
        get { return _ChessBoard.CurrentPlayerTurn; }
        set { _ChessBoard.CurrentPlayerTurn = value; }
    }
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
    public IEnumerable<string> SaveGame()
    {
        return _ChessBoard.SaveGame();
    }
    public bool CheckWinner(out Side side)
    {
        return _ChessBoard.CheckWinner(out side);
    }
}