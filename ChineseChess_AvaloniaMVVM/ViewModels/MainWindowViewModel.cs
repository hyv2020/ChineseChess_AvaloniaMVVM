using DynamicData;
using ReactiveUI;
using System.Windows.Input;
namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";
        private WindowViewModelBase _CurrentWindow;
        readonly WindowViewModelBase[] _Windows;
        private int _currentIndex;
        // The default is the first page

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
            _Windows = new WindowViewModelBase[1] { new StartWindowViewModel(this) };
            _currentIndex = 0;
            _CurrentWindow = _Windows[_currentIndex];
        }
    }
}
