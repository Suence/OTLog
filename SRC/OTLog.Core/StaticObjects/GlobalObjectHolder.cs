using OTLog.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.StaticObjects
{
    public static class GlobalObjectHolder
    {
        public static string CurrentViewName = ViewNames.StartPage;
        public static string CurrentRegionName = RegionNames.MainRegion;
    }
}
