using Avalonia.Controls;
using ChineseChess_AvaloniaMVVM.Models;

namespace ChineseChess_AvaloniaMVVM.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
        }

    }
}