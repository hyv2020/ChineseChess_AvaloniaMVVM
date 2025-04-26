using Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChineseChess_AvaloniaMVVM.ViewModels
{
    public class ViewModelBase : ReactiveObject, IViewToViewModelBridge
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
        private readonly WeakReference<Visual?> _visualRef = new WeakReference<Visual?>(default);
        Visual? IViewToViewModelBridge.View
        {
            get => _visualRef.TryGetTarget(out var target) ? target : default;
            set => _visualRef.SetTarget(value);
        }

        public Avalonia.Controls.TopLevel? TopLevel
        {
            get
            {
                if (_visualRef.TryGetTarget(out var view) && view is not null)
                {
                    return Avalonia.Controls.TopLevel.GetTopLevel(view);
                }
                return default;
            }
        }
    }
    public interface IViewToViewModelBridge
    {
        Visual? View { get; set; }
    }

}
