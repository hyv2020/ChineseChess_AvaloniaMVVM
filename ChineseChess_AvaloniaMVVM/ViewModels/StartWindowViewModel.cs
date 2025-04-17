using ReactiveUI;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class StartWindowViewModel : WindowViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! Start Window";
        public ICommand ToLocalGameWindowCommand { get; }
        public ICommand ToNetworkGameWindowCommand { get; }

        public StartWindowViewModel(MainWindowViewModel parent) : base(parent)
        {
            ToLocalGameWindowCommand = ReactiveCommand.Create(ToLocalGameWindow);
            ToNetworkGameWindowCommand = ReactiveCommand.Create(ToNetworkGameWindow);
        }
        protected virtual void ToLocalGameWindow()
        {
            Parent.ToLocalGameWindow();
        }
        protected virtual void ToNetworkGameWindow()
        {
            Parent.ToNetworkGameWindow();
        }
    }
}
