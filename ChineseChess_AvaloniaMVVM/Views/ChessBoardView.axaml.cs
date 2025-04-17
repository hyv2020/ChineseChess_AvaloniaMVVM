using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using ChineseChess_AvaloniaMVVM.Models;
using System.Diagnostics;
namespace ChineseChess_AvaloniaMVVM.Views;

public partial class ChessBoardView : UserControl
{
    public ChessBoardView()
    {
        InitializeComponent();
    }

    private void Rectangle_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        // Handle the rectangle click event here
        // For example, you can update the cell value or perform some action
        var rectangle = sender as Rectangle;
        if (rectangle != null)
        {
            var cellData = rectangle.Parent.DataContext as CellBase; // Assuming CellBase is your data model
            // Get the cell index from the rectangle's name
            // Perform some action with the cell index
            Debug.WriteLine($"Cell clicked! X: {cellData.X}, Y: {cellData.Y}");
            Debug.WriteLine($"");
            cellData.ResolveMove();
            cellData.OnPropertyChanged(nameof(cellData));
        }
    }

}