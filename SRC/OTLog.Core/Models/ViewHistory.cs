using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Models
{
    /// <summary>
    /// 视图历史信息
    /// </summary>
    public class ViewHistory
    {
        public ViewHistory(string regionName, string viewName, object parameters)
            => (RegionName, ViewName, Parameters) = (regionName, viewName, parameters);
        /// <summary>
        /// Region 名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public object Parameters { get; set; }
    }
}
