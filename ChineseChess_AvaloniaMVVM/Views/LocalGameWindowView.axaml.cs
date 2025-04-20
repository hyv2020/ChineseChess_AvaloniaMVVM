using Avalonia.Controls;
using ChineseChess_AvaloniaMVVM.ViewModels;
using GameCommons;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ChineseChess_AvaloniaMVVM.Views;

public partial class LocalGameWindowView : UserControl
{
    public LocalGameWindowView()
    {
        InitializeComponent();
    }
    private void TurnRecordComboBox_DataContextChanged(object? sender, System.EventArgs e)
    {
        if (sender is ComboBox comboBox)
        {
            var vm = comboBox.Parent.DataContext as LocalGameWindowViewModel;
            comboBox.Items.Clear();
            if (comboBox.DataContext is List<Turn> turns)
            {
                foreach (var turn in turns)
                {
                    comboBox.Items.Add(turn);
                }
                if (comboBox.Items.Any())
                {
                    comboBox.SelectedItem = turns[vm.SelectedTurnIndex];
                }
                if (vm.CheckWinner(out Side side))
                {
                    // Handle the winner here
                    // For example, you can show a message or update the UI
                    Debug.WriteLine($"Winner: {side}");
                }
            }
        }
    }
    private void TurnRecordComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is Turn selectedTurn)
        {
            var vm = comboBox.Parent.DataContext as LocalGameWindowViewModel;
        }
    }
}