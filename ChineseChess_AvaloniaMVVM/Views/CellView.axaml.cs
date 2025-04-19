using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.ViewModels;
using System.Diagnostics;

namespace ChineseChess_AvaloniaMVVM.Views;

public partial class CellView : UserControl
{
    public CellView()
    {
        InitializeComponent();
    }

    private void ChessBoardBackground_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        var chessBoardBackgroundImage = sender as Image;
        if (chessBoardBackgroundImage != null)
        {
            var cell = chessBoardBackgroundImage.DataContext as CellViewModel;
            var board = cell.ChessBoard;
            board.ClearAllValidMoves();
            // Handle the background click event here
            // For example, you can clear the selected cell or perform some action
            Debug.WriteLine("Background clicked!");

        }

    }

    private void ValidMove_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        // Handle the rectangle click event here
        // For example, you can update the cell value or perform some action
        var validMoveImage = sender as Rectangle;
        if (validMoveImage != null)
        {
            var destination = validMoveImage.DataContext as CellBase; // Assuming CellBase is your data model

            // Get the cell index from the rectangle's name
            // Perform some action with the cell index
            //Debug.WriteLine($"Cell clicked! X: {destination.X}, Y: {destination.Y}");
            //Debug.WriteLine($"");
            destination.ResolveMove();
            destination.ChessBoard.ClearAllValidMoves();
            //cellData.OnPropertyChanged(nameof(cellData));
        }
    }
    private void ChessPiece_PointerPressed(object sender, PointerPressedEventArgs e)
    {

        var chessPieceImage = sender as Image;
        if (chessPieceImage != null)
        {
            var cellVm = chessPieceImage.DataContext as CellViewModel;
            if (cellVm != null)
            {
                var chessPiece = cellVm.ChessPieceVm;
                var board = chessPiece.ChessPiece.Location.ChessBoard;
                board.ClearAllValidMoves();
                if (chessPiece.ChessPiece.CanMove)
                {
                    board.SelectedCell = chessPiece.ChessPiece.Location;
                    chessPiece.ChessPiece.Location.IsSelected = true;
                    var validCells = chessPiece.ChessPiece.FindValidMove();
                    foreach (var cell in validCells)
                    {
                        cell.IsValidMove = true;
                    }
                }
            }

        }
    }
}