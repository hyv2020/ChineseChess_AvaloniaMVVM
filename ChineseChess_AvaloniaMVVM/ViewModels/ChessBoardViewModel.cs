using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class ChessBoardViewModel:WindowViewModelBase
    {
        public string Message { get; } = "Chessboard";
        public ChessBoardViewModel() { }
    }
}
