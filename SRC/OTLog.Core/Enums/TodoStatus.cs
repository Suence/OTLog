using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Enums
{
    public enum TodoStatus
    {
        /// <summary>
        /// 新的待办
        /// </summary>
        Default,
        /// <summary>
        /// 未处理（指加班）
        /// </summary>
        Untreated,
        /// <summary>
        /// 已忽略
        /// </summary>
        Negligible
    }
}
