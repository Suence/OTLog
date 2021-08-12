using Microsoft.Win32;
using OTLog.Core.Constants;
using OTLog.Core.Events;
using OTLog.Core.Models;
using OTLog.Core.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OTLog.Home.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware
    {
        #region private 
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<OTRecordTodo> _todoList;
        #endregion

        public ObservableCollection<OTRecordTodo> TodoList
        {
            get => _todoList;
            set => SetProperty(ref _todoList, value);
        }

        public DelegateCommand<string> GoToTargetViewCommand { get; }
        private async void GoToTargetView(string viewName)
        {
            await Task.Delay(150);

            _regionManager.RequestNavigate(
                RegionNames.HomeRegion, 
                viewName,
                viewName == ViewNames.Notice
                ? new NavigationParameters
                {
                    { "TodoList", TodoList }
                }
                : null);
        }

        public HomePageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            GoToTargetViewCommand = new DelegateCommand<string>(GoToTargetView);
            SystemEvents.SessionSwitch += UpdateScreenLockTime;
            LoadData();
        }

        private void LoadData()
        {
            TodoList = new ObservableCollection<OTRecordTodo>(AppFileHelper.GetOTRecordToDos());
        }
        private void UpdateScreenLockTime(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                SessionLocked();
            }

            if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                SessionUnlocked();
            }

            AppFileHelper.SaveRecordToDo(TodoList.ToList());
        }
        private void SessionLocked()
        {
            var lockTime = DateTime.Now;
            var date = lockTime.Hour < 7 ? lockTime.Date.AddDays(-1) : lockTime.Date;

            var todoItem = TodoList.FirstOrDefault(t => t.BeginTime.Date == date);
            if (todoItem == null)
            {
                TodoList.Add(new OTRecordTodo
                {
                    Id = Guid.NewGuid(),
                    BeginTime = lockTime,
                    EndTime = lockTime
                });
                return;
            }

            todoItem.EndTime = lockTime;
        }

        private void SessionUnlocked()
        {
            var activeTime = DateTime.Now;
            var date = activeTime.Hour < 7 ? activeTime.Date.AddDays(-1) : activeTime.Date;
            var todoItem = TodoList.FirstOrDefault(t => t.BeginTime.Date == date);
            if (todoItem == null)
            {
                TodoList.Add(new OTRecordTodo
                {
                    Id = Guid.NewGuid(),
                    BeginTime = activeTime,
                    EndTime = activeTime
                });
                return;
            }

            if (TodoList.Any())
            {
                GoToTargetView(ViewNames.Notice);
                _eventAggregator.GetEvent<NewNoticeEvent>().Publish();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
