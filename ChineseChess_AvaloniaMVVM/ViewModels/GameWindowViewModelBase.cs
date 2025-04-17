using ReactiveUI;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public abstract class GameWindowViewModelBase : WindowViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! UserControl";
        ChessBoardViewModel _Board;
        public ICommand ToStartWindowCommand { get; }
        public MainWindowViewModel Parent { get; }

        public ChessBoardViewModel Board
        {
            get { return _Board; }
            private set { this.RaiseAndSetIfChanged(ref _Board, value); }
        }

        protected GameWindowViewModelBase(MainWindowViewModel parent) : base(parent)
        {
            Parent = parent;
            _Board = new ChessBoardViewModel(PostChessPieceMove);
            ToStartWindowCommand = ReactiveCommand.Create(ToStartWindow);

        }
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
