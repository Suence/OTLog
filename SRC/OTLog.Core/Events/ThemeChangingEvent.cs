using OTLog.Core.Enums;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Events
{
    public class ThemeChangingEvent : PubSubEvent<Theme>
    {
    }
}
