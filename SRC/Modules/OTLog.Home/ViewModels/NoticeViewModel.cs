using OTLog.Core.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class NoticeViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
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

        public NoticeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        public bool KeepAlive => false;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var data = navigationContext.Parameters["TodoList"] as IEnumerable<OTRecordTodo>;
            TodoList = new ObservableCollection<OTRecordTodo>(data);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
