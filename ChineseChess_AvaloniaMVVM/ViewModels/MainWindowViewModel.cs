using Avalonia.Threading;
using ReactiveUI;
namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private WindowViewModelBase _CurrentWindow;
        private StartWindowViewModel _StartWindowViewModel;
        private LocalGameWindowViewModel _LocalGameWindowViewModel;
        private NetworkGameWindowViewModel _NetworkGameWindowViewModel;

        /// <summary>
        /// Gets the current page. The property is read-only
        /// </summary>
        public WindowViewModelBase CurrentWindow
        {
            get { return _CurrentWindow; }
            private set { this.RaiseAndSetIfChanged(ref _CurrentWindow, value); }
        }

        public MainWindowViewModel()
        {
            _StartWindowViewModel = new StartWindowViewModel(this);
            _NetworkGameWindowViewModel = new NetworkGameWindowViewModel(this);
            _CurrentWindow = _StartWindowViewModel;
        }
        public void ToStartWindow()
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                CurrentWindow = _StartWindowViewModel;
            });
        }
        public void ToLocalGameWindow()
        {
            _LocalGameWindowViewModel = new LocalGameWindowViewModel(this);
            _LocalGameWindowViewModel.Reset();
            CurrentWindow = _LocalGameWindowViewModel;
        }
        public void ToNetworkGameWindow()
        {
            _NetworkGameWindowViewModel.Reset();
            CurrentWindow = _NetworkGameWindowViewModel;
        }
    }
}
