using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class StartWindowViewModel: WindowViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! UserControl";
        WindowViewModelBase _Board;
        MainWindowViewModel Parent { get; }
        public WindowViewModelBase Board
        {
            get { return _Board; }
            private set { this.RaiseAndSetIfChanged(ref _Board, value); }
        }
        
        public StartWindowViewModel(MainWindowViewModel parent)
        {
            Parent = parent;
            _Board = new ChessBoardViewModel();
        }
    }
}
