using Avalonia.Threading;
using ChessModelLib;
using ReactiveUI;
using System.Collections.Generic;
namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private WindowViewModelBase _CurrentWindow;
        private StartWindowViewModel _StartWindowViewModel;
        private LocalGameWindowViewModel _LocalGameWindowViewModel;
        private NetworkGameSetupViewModel _NetworkGameSetupViewModel;
        private NetworkGameWindowViewModel _NetworkGameWindowViewModel;
        List<ICreateBoardCommand> _Games = new();
        public ICreateBoardCommand GameToCreate { get => _StartWindowViewModel.GameToCreate; }
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
            _NetworkGameWindowViewModel = null;
            _NetworkGameSetupViewModel = new NetworkGameSetupViewModel(this);
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
            Dispatcher.UIThread.Invoke(() =>
            {
                CurrentWindow = _LocalGameWindowViewModel;
            });
        }
        public void ToNetworkSetupWindow()
        {
            _NetworkGameSetupViewModel.Reset();
            Dispatcher.UIThread.Invoke(() =>
            {
                CurrentWindow = _NetworkGameSetupViewModel;
            });
        }
        public void ToNetworkGameWindow(NetworkGameWindowViewModel networkGameVm)
        {
            _NetworkGameWindowViewModel = networkGameVm;
            Dispatcher.UIThread.Invoke(() =>
            {
                CurrentWindow = _NetworkGameWindowViewModel;
            });
        }
        public void DisconnectNetworkGame()
        {
            _NetworkGameWindowViewModel = null;
            Dispatcher.UIThread.Invoke(() =>
            {
                CurrentWindow = _StartWindowViewModel;
            });
        }
    }

}
