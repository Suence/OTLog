using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Enums
{
    [Flags]
    public enum StartupScheme
    {
        Normal = 0,
        NoWindow = 1,
    }
}
