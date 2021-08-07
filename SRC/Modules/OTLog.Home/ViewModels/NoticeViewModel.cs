using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OTLog.Home.ViewModels
{
    public class NoticeViewModel : BindableBase, IRegionMemberLifetime
    {
        public NoticeViewModel()
        {

        }

        public bool KeepAlive => false;
    }
}
