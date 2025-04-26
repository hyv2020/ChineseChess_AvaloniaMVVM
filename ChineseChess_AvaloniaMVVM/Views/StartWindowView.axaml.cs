using Avalonia.Controls;
using ChessModelLib;
using ChineseChess_AvaloniaMVVM.ViewModels;
using System.Collections.Generic;

namespace ChineseChess_AvaloniaMVVM.Views;

public partial class StartWindowView : UserControl
{
    public StartWindowView()
    {
        InitializeComponent();
    }
    private void GameComboBox_DataContextChanged(object? sender, System.EventArgs e)
    {
        if (sender is ComboBox comboBox)
        {
            comboBox.Items.Clear();
            if (comboBox.DataContext is List<ICreateBoardCommand> gameTypes)
            {
                foreach (var gameType in gameTypes)
                {
                    comboBox.Items.Add(gameType);
                }
                comboBox.SelectedIndex = 0; // Set the default selected index to the first item
            }
        }
    }
    private void GameComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is ICreateBoardCommand selectedGame)
        {
            var vm = comboBox.Parent.DataContext as StartWindowViewModel;
            vm.SelectedGameIndex = comboBox.SelectedIndex;
        }
    }
}