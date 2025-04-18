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
        var background = sender as Image;
        if (background != null)
        {
            var cell = background.DataContext as CellViewModel;
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
        var validMove = sender as Rectangle;
        if (validMove != null)
        {
            var cellData = validMove.DataContext as CellBase; // Assuming CellBase is your data model
            // Get the cell index from the rectangle's name
            // Perform some action with the cell index
            Debug.WriteLine($"Cell clicked! X: {cellData.X}, Y: {cellData.Y}");
            Debug.WriteLine($"");
            cellData.ResolveMove();
            //cellData.OnPropertyChanged(nameof(cellData));
        }
    }
    private void ChessPiece_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        var chessPiece = sender as Image;
        if (chessPiece != null)
        {

        }
    }
}