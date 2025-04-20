using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Utils;
using GameCommons;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class LocalGameWindowViewModel : GameWindowViewModelBase
    {
        public string Message { get; } = "Local Game";
        List<Turn> _turnRecord = new();
        public List<Turn> TurnRecord
        {
            get { return _turnRecord; }
            set { this.RaiseAndSetIfChanged(ref _turnRecord, value); }
        }
        bool _isAutoSaveEnabled = true;
        string _saveFileName;
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
            PropertyChanged += ResolveMove_UIUpdate;
        }
        public LocalGameWindowViewModel() : this(new MainWindowViewModel())
        {
            // This constructor is used for design-time data
        }
        public override void Reset()
        {
            BoardUserControl.ChessBoardVm.ClearBoard();
            BoardUserControl.ChessBoardVm.LoadGame();
        }
        protected override void ToStartWindow()
        {
            Reset();
            base.ToStartWindow();
        }
        public void LoadGame()
        {
            // Load game logic here
            BoardUserControl.ChessBoardVm.LoadGame();
        }
        private void LoadSave(string saveFilePath)
        {
            try
            {
                TurnRecord = new(UtilOps.LoadSaveFile(saveFilePath));

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Failed to load save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var selectedTurn = TurnRecord.Last();
            BoardUserControl.ChessBoardVm.ClearBoard();
            BoardUserControl.ChessBoardVm.LoadGame(selectedTurn.BoardState);
            this._currentTurn = selectedTurn.TurnNumber;
            //this.UpdateTurnLabel();
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
        public ICommand LoadCommand { get; }
        public void TurnRecordComboBox_PropertyChanged()
        {
            // Load the selected turn from the TurnRecord
            //var selectedTurn = TurnRecord.Last();
            //BoardUserControl.ChessBoardVm.ClearBoard();
            //BoardUserControl.ChessBoardVm.LoadGame(selectedTurn.BoardState);
            //this._currentTurn = selectedTurn.TurnNumber;
        }
        public void ResolveMove_UIUpdate(object? sender, PropertyChangedEventArgs e)
        {
            // Notify the UI to update the chessboard
            if (sender is ChessBoardUserControlViewModel cbControlVm)
            {
                // Update the UI with the new chessboard state
                //this.UpdateTurnLabel();
            }
        }
    }
}
