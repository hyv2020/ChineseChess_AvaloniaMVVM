namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class LocalGameWindowViewModel : GameWindowViewModelBase
    {
        public string Message { get; } = "Local Game";
        public LocalGameWindowViewModel(MainWindowViewModel parent) : base(parent)
        {

        }
    }
}
