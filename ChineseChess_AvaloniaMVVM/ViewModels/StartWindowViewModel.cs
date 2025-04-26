using Avalonia.Controls;
using ChessModelLib;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public partial class StartWindowViewModel : WindowViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! Start Window";
        public ICommand ToLocalGameWindowCommand { get; }
        public ICommand ToNetworkGameWindowCommand { get; }
        private List<ICreateBoardCommand> _Games = new();

        public List<ICreateBoardCommand> Games { get { return _Games; } set { this.RaiseAndSetIfChanged(ref _Games, value); } }
        public int SelectedGameIndex { get; set; } = 0;
        public ICreateBoardCommand GameToCreate { get => Games.Any() ? Games[SelectedGameIndex] : null; }

        public StartWindowViewModel(MainWindowViewModel parent) : base(parent)
        {
            LoadGame();
            ToLocalGameWindowCommand = ReactiveCommand.Create(ToLocalGameWindow);
            ToNetworkGameWindowCommand = ReactiveCommand.Create(ToNetworkGameWindow);
        }
        public StartWindowViewModel() : this(new MainWindowViewModel())
        {
            // This constructor is used for design-time data
        }
        protected virtual void ToLocalGameWindow()
        {
            Parent.ToLocalGameWindow();
        }
        protected virtual void ToNetworkGameWindow()
        {
            Parent.ToNetworkSetupWindow();
        }

        private void LoadGame()
        {
            var window = new Avalonia.Controls.Window
            {
                Width = 0,
                Height = 0,
                Opacity = 0,
                ShowInTaskbar = false,
                SystemDecorations = SystemDecorations.None,
            };
            var sp = window.StorageProvider;
            var folders = sp.TryGetFolderFromPathAsync(new Uri(Environment.CurrentDirectory + "/Assets/Dlls"));
            var result = folders.Result;
            var getItems = result.GetItemsAsync().ToBlockingEnumerable();
            var games = new List<ICreateBoardCommand>();
            foreach (var item in getItems)
            {
                var plugin = LoadPlugin(item.Path.LocalPath);
                var commands = CreateCommands(plugin);
                games.AddRange(commands);
            }
            Games = games;
            window.Hide();
        }
        static Assembly LoadPlugin(string assemblyPath)
        {
            Console.WriteLine($"Loading commands from: {assemblyPath}");
            var loadContext = Assembly.LoadFile(assemblyPath);
            return loadContext;
        }
        static IEnumerable<ICreateBoardCommand> CreateCommands(Assembly assembly)
        {
            var count = 0;
            foreach (var type in assembly.GetTypes())
            {
                if (type is not null)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Any(intf => intf.FullName is not null && intf.FullName.Contains(nameof(ICreateBoardCommand))))
                    {
                        var result = Activator.CreateInstance(type) as ICreateBoardCommand;
                        if (result != null)
                        {
                            count++;
                            yield return result;
                        }
                    }
                }
            }
            /*if (count == 0)
            {
                var availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }*/
        }
    }
}
