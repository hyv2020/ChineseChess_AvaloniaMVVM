using Avalonia.Controls;
using ChineseChess_AvaloniaMVVM.ViewModels;
using GameCommons;

namespace ChineseChess_AvaloniaMVVM.Views;

public partial class LocalGameWindowView : UserControl
{
    public LocalGameWindowView()
    {
        InitializeComponent();
    }
    public void TurnRecordComboBox_PropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is Turn selectedTurn)
        {
            var vm = comboBox.Parent.Parent.DataContext as LocalGameWindowViewModel;
            //BoardUserControl.ChessBoardVm.ClearBoard();
            //BoardUserControl.ChessBoardVm.LoadGame(selectedTurn.BoardState);
            //this._currentTurn = selectedTurn.TurnNumber;
            //this.UpdateTurnLabel();
        }
    }
}