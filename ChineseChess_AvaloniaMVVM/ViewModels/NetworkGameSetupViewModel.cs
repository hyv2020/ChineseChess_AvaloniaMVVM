using Avalonia.Threading;
using MsBox.Avalonia;
using ReactiveUI;
using System.Net;
using System.Windows.Input;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public class NetworkGameSetupViewModel : WindowViewModelBase
    {
        public ICommand HostCommand { get; }
        public ICommand JoinCommand { get; }
        public ICommand CancelCommand { get; }
        private string _ipAddress = string.Empty;
        public string IpAddress { get => _ipAddress; set { this.RaiseAndSetIfChanged(ref _ipAddress, value); } }

        public NetworkGameSetupViewModel(MainWindowViewModel parent) : base(parent)
        {
            CancelCommand = ReactiveCommand.Create(ToStartWindow);
            JoinCommand = ReactiveCommand.Create(Join);
            HostCommand = ReactiveCommand.Create(Host);
        }
        public NetworkGameSetupViewModel() : this(new MainWindowViewModel())
        {
            // This constructor is used for design-time data
        }
        public void Reset()
        {
            // Reset the view model properties to their initial state
            IpAddress = string.Empty;
        }
        private void ToStartWindow()
        {
            Reset();
            Parent.ToStartWindow();
        }
        private void Host()
        {
            NetworkGameWindowViewModel game = new(Parent);
            Parent.ToNetworkGameWindow(game);
        }
        private void Join()
        {
            if (_ipAddress.Length > 0 && IPAddress.TryParse(_ipAddress, out var iPAddress))
            {
                NetworkGameWindowViewModel game = new(Parent, _ipAddress);
                if (game.ClientConnected)
                {
                    Parent.ToNetworkGameWindow(game);
                }
            }
            else
            {
                var invalidIP = MessageBoxManager.GetMessageBoxStandard("Invalid IP", "Enter valid IP", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    var result = await invalidIP.ShowAsync();
                    if (result == MsBox.Avalonia.Enums.ButtonResult.Ok)
                    {
                        // Handle the OK button click
                    }
                });
            }
        }
    }
}
