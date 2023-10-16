﻿using System.ComponentModel;

namespace CashDispenserWinApp.ViewModels
{
    public abstract class NotifyModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
