using OTLog.Core.Models;
using Prism.Events;

namespace OTLog.Core.Events
{
    public class OTRecordChangedEvent : PubSubEvent<OTRecord>
    {
        public OTRecord OTRecord { get; set; }
    }
}
