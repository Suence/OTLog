using Microsoft.Win32;
using OTLog.Core.Enums;
using OTLog.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace OTLog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region private
        private string _title = "OTLog";
        private DateTime _lastActiveTime = DateTime.Now;
        private TaskProgress _taskProgress;
        private IEventAggregator _eventAggregator;
        private Theme? _newTheme;
        #endregion

        public TaskProgress TaskProgress
        {
            get => _taskProgress;
            set => SetProperty(ref _taskProgress, value);
        }

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

        public DelegateCommand ResetTaskStatusCommand { get; }
        private void ResetTaskStatus()
        {
            TaskProgress = TaskProgress.Normal;
        }

        public DelegateCommand ChangeThemeCommand { get; }
        private void ChangeTheme()
        {
            _eventAggregator.GetEvent<ThemeChangedEvent>().Publish((Theme)_newTheme);
            TaskProgress = TaskProgress.Finished;
        }


        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ThemeChangingEvent>().Subscribe(ThemeChanging);
            ResetTaskStatusCommand = new DelegateCommand(ResetTaskStatus);
            ChangeThemeCommand = new DelegateCommand(ChangeTheme);
        }

        private void ThemeChanging(Theme theme)
        {
            TaskProgress = TaskProgress.InProgress;
            _newTheme = theme;
        }
    }
}
