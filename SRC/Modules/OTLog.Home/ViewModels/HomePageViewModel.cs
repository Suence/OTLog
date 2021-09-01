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
        private string _nameOfCurrentTab;
        #endregion

        public string NameOfCurrentTab
        {
            get => _nameOfCurrentTab;
            set => SetProperty(ref _nameOfCurrentTab, value);
        }

        public ObservableCollection<OTRecordTodo> TodoList
        {
            get => _todoList;
            set => SetProperty(ref _todoList, value);
        }
    
        public DelegateCommand<string> GoToTargetViewCommand { get; }
        private async void GoToTargetView(string viewName)
        {
            await Task.Delay(100);

            var todo = TodoList.Skip(1).FirstOrDefault(r => r.Status == Core.Enums.TodoStatus.Untreated);

            if (viewName == ViewNames.Notice && NameOfCurrentTab == ViewNames.Notice)
                _regionManager.Regions[RegionNames.HomeRegion].RemoveAll();

            _regionManager.RequestNavigate(
                RegionNames.HomeRegion, 
                viewName,
                viewName == ViewNames.Notice
                ? new NavigationParameters
                {
                    { "Todo", todo }
                }
                : null);

            NameOfCurrentTab = viewName;
        }

        public HomePageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            GoToTargetViewCommand = new DelegateCommand<string>(GoToTargetView);
            SystemEvents.SessionSwitch += UpdateScreenLockTime;
            
            NameOfCurrentTab = ViewNames.Overview;

            _eventAggregator.GetEvent<OTRecordTodoChangedEvent>().Subscribe(ToDoChanged);
            _eventAggregator.GetEvent<RequestViewEvent>().Subscribe(RequestView);
            LoadData();
            SessionUnlocked();
        }

        private void RequestView(string viewName)
        {
            _regionManager.Regions[RegionNames.MessageRegion].RemoveAll();
            GoToTargetView(viewName);
        }

        private void ToDoChanged()
        {
            AppFileHelper.SaveRecordToDoAsync(TodoList.ToList());
        }

        private void LoadData()
        {
            TodoList = new ObservableCollection<OTRecordTodo>(AppFileHelper.GetOTRecordToDos().OrderByDescending(r => r.BeginTime));
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

            AppFileHelper.SaveRecordToDoAsync(TodoList.ToList());
        }
        private void SessionLocked()
        {
            var lockTime = DateTime.Now;
            var date = lockTime.Hour < 7 ? lockTime.Date.AddDays(-1) : lockTime.Date;
             
            var todoItem = TodoList.FirstOrDefault(t => t.BeginTime.Date == date);
            if (todoItem == null)
            {
                TodoList.Insert(0, new OTRecordTodo
                {
                    Id = Guid.NewGuid(),
                    BeginTime = lockTime,
                    EndTime = lockTime
                });
                return;
            }

            todoItem.EndTime = lockTime;
            if (lockTime.Hour < 7 || lockTime.Hour > 21)
            {
                todoItem.Status = Core.Enums.TodoStatus.Untreated;
            }
        }

        private async void SessionUnlocked()
        {
            var activeTime = DateTime.Now;
            var date = activeTime.Hour < 7 ? activeTime.Date.AddDays(-1) : activeTime.Date;
            var todoItem = TodoList.FirstOrDefault(t => t.BeginTime.Date == date);
            if (todoItem == null)
            {
                TodoList.Insert(0, new OTRecordTodo
                {
                    Id = Guid.NewGuid(),
                    BeginTime = date.Date + TimeSpan.FromHours(8),
                    EndTime = activeTime
                });
            }

            if (TodoList.Skip(1).FirstOrDefault(r => r.Status == Core.Enums.TodoStatus.Untreated) != null)
            {
                _eventAggregator.GetEvent<NewNoticeEvent>().Publish();
                await Task.Delay(1000);
                GoToTargetView(ViewNames.Notice);
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
