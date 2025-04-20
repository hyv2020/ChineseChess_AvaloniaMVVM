using ChineseChess_AvaloniaMVVM.Models;
using ChineseChess_AvaloniaMVVM.Models.ChineseChess.Utils;
using GameCommons;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class LocalGameWindowViewModel : GameWindowViewModelBase
    {
        public string GameDescription { get => BoardUserControl.ChessBoardVm.GameDescription; }
        public string Message { get; } = "Local Game";
        List<Turn> _turnRecord = new();
        public int SelectedTurnIndex { get; set; }
        public List<Turn> TurnRecord
        {
            get { return _turnRecord; }
            set { this.RaiseAndSetIfChanged(ref _turnRecord, value); }
        }
        bool _isAutoSaveEnabled = true;
        string _saveFileName;
        int _currentTurn = 0;
        public Side CurrentPlayerTurn
        {
            get { return BoardUserControl.ChessBoardVm.CurrentPlayerTurn; }
            set { BoardUserControl.ChessBoardVm.CurrentPlayerTurn = value; }
        }
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
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            SelectedTurnIndex = 0;
            var boardState = vm.SaveGame();
            Turn currentTurnState = new Turn(_currentTurn, CurrentPlayerTurn, boardState.ToList());
            currentTurnState.SaveToFile();
            var currentRecord = new List<Turn>() { currentTurnState };
            TurnRecord = currentRecord;
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
        public override void UpdateUIPostMove(ChessBoardBase chessBoard)
        {
            var boardState = chessBoard.SaveGame();
            _currentTurn++;
            Turn currentTurnState = new Turn(_currentTurn, CurrentPlayerTurn, boardState.ToList());
            currentTurnState.SaveToFile();
            var currentRecord = new List<Turn>(TurnRecord) { currentTurnState };
            TurnRecord = currentRecord;
            SelectedTurnIndex = TurnRecord.Count - 1;
        }
        public bool CheckWinner(out Side side)
        {
            return BoardUserControl.ChessBoardVm.CheckWinner(out side);
        }
    }
}
