namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public abstract class WindowViewModelBase : ViewModelBase
    {
        public MainWindowViewModel Parent { get; }
        protected WindowViewModelBase(MainWindowViewModel parent)
        {
            Parent = parent;
        }
    }
}
