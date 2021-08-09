using Microsoft.Win32;
using Prism.Mvvm;
using System;

namespace OTLog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region private
        private string _title = "OTLog";
        private DateTime _lastActiveTime = DateTime.Now;
        #endregion

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DateTime LastActiveTime
        {
            get => _lastActiveTime;
            set => SetProperty(ref _lastActiveTime, value);
        }

        public MainWindowViewModel()
        {
        }
    }
}
