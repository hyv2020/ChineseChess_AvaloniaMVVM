using Avalonia.Threading;
using ChessModelLib;
using ChineseChess_AvaloniaMVVM.Models;
using GameCommons;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class LocalGameWindowViewModel : GameWindowViewModelBase
    {
        public override string Message { get; } = "Local Game";
        List<Turn> _turnRecord = new();
        int _currentTurnIndex = 0;
        public int SelectedTurnIndex { get { return _currentTurn; } set { this.RaiseAndSetIfChanged(ref _currentTurn, value); } }
        public List<Turn> TurnRecord
        {
            get { return _turnRecord; }
            set { this.RaiseAndSetIfChanged(ref _turnRecord, value); }
        }
        public bool UpdateBoardAfterComboBoxUpdate { get; set; } = false;
        bool _isAutoSaveEnabled = false;
        string _saveFileName = string.Empty;
        int _currentTurn = 0;

        public string SaveFileName
        {
            get { return _saveFileName; }
            set { this.RaiseAndSetIfChanged(ref _saveFileName, value); }
        }
        public bool IsAutoSaveEnabled
        {
            get { return _isAutoSaveEnabled; }
            set { this.RaiseAndSetIfChanged(ref _isAutoSaveEnabled, value); }
        }
        public LocalGameWindowViewModel(MainWindowViewModel parent) : base(parent)
        {
            RestartCommand = ReactiveCommand.Create(() =>
            {
                Reset();
            });
            SaveCommand = ReactiveCommand.Create(() =>
            {
                SaveCommand_Executed();
            });

        }
        public LocalGameWindowViewModel() : this(new MainWindowViewModel())
        {
            // This constructor is used for design-time data
        }
        public override void Reset()
        {
            var vm = BoardUserControl.ChessBoardVm;
            vm.ClearBoard();
            vm.LoadGame();
            vm.ActiveGame = true;
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            SelectedTurnIndex = 0;
            var resetSide = TurnRecord[0].WhosTurn;
            var boardState = vm.SaveGame();
            Turn currentTurnState = new Turn(vm.GameMode, _currentTurn, resetSide, boardState.ToList());
            currentTurnState.SaveToFile();
            var currentRecord = new List<Turn>() { currentTurnState };
            TurnRecord = currentRecord;
        }
        protected override void ToStartWindow()
        {
            base.ToStartWindow();
        }
        public void LoadGame(Turn turn)
        {
            // Load game logic here
            BoardUserControl.ChessBoardVm.ClearBoard();
            BoardUserControl.ChessBoardVm.LoadGame(turn.BoardState);
            CurrentPlayerTurn = turn.WhosTurn;
            this._currentTurn = turn.TurnNumber;
            CheckWinner(out Side side);
            this.RaisePropertyChanged(nameof(CurrentPlayerTurn));
        }

        private void LoadSave(string saveFilePath)
        {
            try
            {
                var toLoad = UtilOps.LoadSaveFile(saveFilePath);
                TurnRecord = new(UtilOps.LoadSaveFile(saveFilePath));
            }
            catch (Exception ex)
            {
                var errorMessageMobx = MessageBoxManager.GetMessageBoxStandard("Failed to load save file", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    await errorMessageMobx.ShowAsync();
                    return;
                });
            }
            var selectedTurn = TurnRecord.Last();
            BoardUserControl.ChessBoardVm.ClearBoard();
            BoardUserControl.ChessBoardVm.LoadGame(selectedTurn.BoardState);
            this._currentTurn = selectedTurn.TurnNumber;
            CurrentPlayerTurn = selectedTurn.WhosTurn;
            UpdateBoardAfterComboBoxUpdate = false;
            SelectedTurnIndex = TurnRecord.Count - 1;
            UpdateBoardAfterComboBoxUpdate = true;
            SetTurnLabel();
        }
        private string GetSaveFileName()
        {
            string fileName;
            if (SaveFileName == "")
            {
                fileName = $"GameSave";
            }
            else
            {
                fileName = $"{SaveFileName}";
            }
            return fileName;
        }
        private void AutoSaveToFile()
        {
            if (IsAutoSaveEnabled)
            {
                string fileName = GetSaveFileName();
                UtilOps.SaveFile(fileName, true);
            }
        }
        public ICommand RestartCommand { get; }
        public ICommand SaveCommand { get; }
        public void TurnRecordComboBox_PropertyChanged()
        {
            // Load the selected turn from the TurnRecord
            //var selectedTurn = TurnRecord.Last();
            //BoardUserControl.ChessBoardVm.ClearBoard();
            //BoardUserControl.ChessBoardVm.LoadGame(selectedTurn.BoardState);
            //this._currentTurn = selectedTurn.TurnNumber;
        }
        public override void UpdateUIPostMove(ChessBoardBase chessBoard)
        {
            if (SelectedTurnIndex < TurnRecord.Count - 1)
            {
                UtilOps.DeleteTempFilesAfterTurn(SelectedTurnIndex);
                TurnRecord.RemoveRange(SelectedTurnIndex + 1, TurnRecord.Count - SelectedTurnIndex - 1);
            }
            var boardState = chessBoard.SaveGame();
            _currentTurn++;
            Turn currentTurnState = new Turn(chessBoard.GetType().Name, _currentTurn, CurrentPlayerTurn, boardState.ToList());
            currentTurnState.SaveToFile();
            var currentRecord = new List<Turn>(TurnRecord) { currentTurnState };
            TurnRecord = currentRecord;
            UpdateBoardAfterComboBoxUpdate = false;
            SelectedTurnIndex = TurnRecord.Count - 1;
            UpdateBoardAfterComboBoxUpdate = true;
            AutoSaveToFile();
        }
        public void LoadGameFromFile(string saveFileName)
        {
            LoadSave(saveFileName);
        }
        private void SaveCommand_Executed()
        {
            string fileName = GetSaveFileName();
            if (System.IO.File.Exists(FilePaths.rootSaveFilePath + fileName + ".sav"))
            {
                var overwriteMessage = MessageBoxManager.GetMessageBoxStandard("Save file exist", "Overwrite existing file?", MsBox.Avalonia.Enums.ButtonEnum.YesNoCancel, MsBox.Avalonia.Enums.Icon.Question);
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    var result = await overwriteMessage.ShowAsync();
                    if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
                    {
                        UtilOps.SaveFile(fileName, true);
                        SaveGameMessage();
                    }
                    else if (result == MsBox.Avalonia.Enums.ButtonResult.No)
                    {
                        UtilOps.SaveFile(fileName, false);
                        SaveGameMessage();
                    }
                });
            }
            else
            {
                UtilOps.SaveFile(fileName, false);
                SaveGameMessage();
            }
        }
        private void SaveGameMessage()
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Game saved", "Game saved", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info);
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await message.ShowAsync();
            });
        }
    }
}
