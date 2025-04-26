using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.ViewModels;
using GameCommons;
using MsBox.Avalonia;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
                if (vm.CheckWinner(out Side winner))
                {
                    // Handle the winner here
                    // For example, you can show a message or update the UI
                    var winnnerMessage = MessageBoxManager.GetMessageBoxStandard("We have a winner", $"{winner} Side Wins", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info);
                    Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        var result = await winnnerMessage.ShowAsync();
                    });
                    Debug.WriteLine($"Winner: {winner}");
                }
            }
        }
    }
    private void TurnRecordComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is Turn selectedTurn)
        {
            var vm = comboBox.Parent.DataContext as LocalGameWindowViewModel;
            vm.SelectedTurnIndex = comboBox.SelectedIndex;
            if (vm.UpdateBoardAfterComboBoxUpdate)
            {
                vm.LoadGame(selectedTurn);
                vm.SetTurnLabel();
            }
        }
    }
    private async void LoadButton_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is TextBlock block)
        {
            var vm = block.Parent.DataContext as LocalGameWindowViewModel;
            await LoadFile(vm);
            vm.SetTurnLabel();
            TurnRecordComboBox.SelectedIndex = vm.TurnRecord.Count - 1;
        }
    }
    private async Task LoadFile(LocalGameWindowViewModel vm)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);
        FilePickerOpenOptions filePickerOpenOptions = new FilePickerOpenOptions()
        {
            Title = "Open Save File",
            AllowMultiple = false
        };
        UtilOps.CheckSaveDirectory();
        IStorageFolder storageFolder = await topLevel.StorageProvider.TryGetFolderFromPathAsync(FilePaths.rootSaveFilePath);
        filePickerOpenOptions.SuggestedStartLocation = storageFolder;
        filePickerOpenOptions.FileTypeFilter = new List<FilePickerFileType>()
        {
            new FilePickerFileType("Save File")
            {
                Patterns = new List<string>() { "*.sav" }
            }
        };
        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(filePickerOpenOptions);

        if (files.Count == 1)
        {
            var selectedFile = files[0];
            UtilOps.ClearTempFolder();
            string saveFileName = selectedFile.Path.AbsolutePath;
            vm.LoadGameFromFile(saveFileName);
        }
    }
}