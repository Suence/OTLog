using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class AboutViewModel : BindableBase, IRegionMemberLifetime
    {
        public AboutViewModel()
        {

        }

        public bool KeepAlive => false;
    }
}
