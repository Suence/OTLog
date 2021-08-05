using OTLog.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Events
{
    public class OTRecordDeletedEvent : PubSubEvent<OTRecord>
    {
        public OTRecord OTRecord { get; set; }
    }
}
