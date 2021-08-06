using OTLog.Core.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class ErrorTipsViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        #region private
        private readonly IRegionManager _regionManager;
        private string _errorText;
        #endregion

        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value);
        }

        public DelegateCommand CloseCommand { get; }
        private void Close()
        {
            _regionManager.Regions[RegionNames.ErrorRegion].RemoveAll();
        }


        public ErrorTipsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            CloseCommand = new DelegateCommand(Close);
        }
        
        public bool KeepAlive => false;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ErrorText = navigationContext.Parameters["Message"].ToString();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
