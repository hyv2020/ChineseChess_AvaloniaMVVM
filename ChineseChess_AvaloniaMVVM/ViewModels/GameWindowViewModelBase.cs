using ReactiveUI;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public abstract class GameWindowViewModelBase : WindowViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! UserControl";
        ChessBoardUserControlViewModel _boardUserControl;
        public ICommand ToStartWindowCommand { get; }
        public MainWindowViewModel Parent { get; }

        public ChessBoardUserControlViewModel BoardUserControl
        {
            get { return _boardUserControl; }
            private set { this.RaiseAndSetIfChanged(ref _boardUserControl, value); }
        }

        protected GameWindowViewModelBase(MainWindowViewModel parent) : base(parent)
        {
            Parent = parent;
            _boardUserControl = new ChessBoardUserControlViewModel(PostChessPieceMove);
            ToStartWindowCommand = ReactiveCommand.Create(ToStartWindow);

        }
        public abstract void Reset();
        protected virtual void ToStartWindow()
        {
            Parent.ToStartWindow();
        }
        protected void PostChessPieceMove(object? sender, PropertyChangedEventArgs e)
        {
            // Notify the UI to update the chessboard
            Debug.WriteLine($"Property changed: {e.PropertyName}");
            Debug.WriteLine("");
        }
    }
}
