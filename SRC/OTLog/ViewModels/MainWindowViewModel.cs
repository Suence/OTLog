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
            SystemEvents.SessionSwitch += UpdateLastActiveTime;
        }

        private void UpdateLastActiveTime(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                LastActiveTime = DateTime.Now;
                return;
            }

            //if (e.Reason == SessionSwitchReason.SessionUnlock)
            //{
            //    // do nothing
            //}
        }
    }
}
