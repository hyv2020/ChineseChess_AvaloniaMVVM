namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class NetworkGameWindowViewModel : GameWindowViewModelBase
    {
        public string Message { get; } = "Network Game";
        public NetworkGameWindowViewModel(MainWindowViewModel parent) : base(parent)
        {

        }
        public NetworkGameWindowViewModel() : this(new MainWindowViewModel())
        {
            // This constructor is used for design-time data
        }
        public override void Reset()
        {

        }
    }
}
