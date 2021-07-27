using OTLog.Core.Models;
using Prism.Events;

namespace OTLog.Core.Events
{
    /// <summary>
    /// 正在导航到新视图
    /// </summary>
    public class GoingToNewViewEvent : PubSubEvent<ViewHistory>
    {
        public ViewHistory ViewHistory { get; set; }
    }
}
