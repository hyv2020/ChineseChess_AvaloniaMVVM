using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using ChineseChess_AvaloniaMVVM.Models;
using Tmds.DBus.Protocol;

namespace ChineseChess_AvaloniaMVVM.ViewModels;

public class ChessBoardViewModel
{
    private ChessBoardBase ChessBoard { get; }
    public List<CellViewModel> GridVm { get; private set; }
    public int RowCount => ChessBoard.RowCount;
    public ChessBoardViewModel(ChessBoardBase chessBoard)
    {
        ChessBoard = chessBoard;
        GridVm = new();
        foreach (var cell in ChessBoard.GridArr)
        {
            GridVm.Add(new CellViewModel(cell));
        }
    }
}