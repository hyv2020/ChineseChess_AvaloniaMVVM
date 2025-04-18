namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class NetworkGameWindowViewModel : GameWindowViewModelBase
    {
        public string Message { get; } = "Network Game";
        public NetworkGameWindowViewModel(MainWindowViewModel parent) : base(parent)
        {

        }

        public override void Reset()
        {

        }
    }
}
