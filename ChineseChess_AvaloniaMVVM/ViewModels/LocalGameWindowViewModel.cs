namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class LocalGameWindowViewModel : GameWindowViewModelBase
    {
        public string Message { get; } = "Local Game";
        public LocalGameWindowViewModel(MainWindowViewModel parent) : base(parent)
        {

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
    }
}
