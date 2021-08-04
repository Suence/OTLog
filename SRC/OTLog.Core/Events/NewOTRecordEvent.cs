using OTLog.Core.Models;
using Prism.Events;

namespace OTLog.Core.Events
{
    public class NewOTRecordEvent : PubSubEvent<OTRecord>
    {
        public OTRecord OTRecord { get; set; }
    }
}
