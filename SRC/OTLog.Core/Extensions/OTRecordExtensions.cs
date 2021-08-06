using OTLog.Core.Models;
using System;

namespace OTLog.Core.Extensions
{
    public static class OTRecordExtensions
    {
        public static (DateTime?, DateTime?) CoincidenceInterval(
            this OTRecord @this, OTRecord record)
        {
            if (@this == null || record == null)
            {
                return (null, null);
            }

            if (@this.BeginTime > record.BeginTime && @this.BeginTime <= record.EndTime)
            {
                return (@this.BeginTime, record.EndTime);
            }
            if (@this.EndTime >= record.BeginTime && @this.EndTime < record.EndTime)
            {
                return (record.BeginTime, @this.EndTime);
            }
            if (@this.BeginTime < record.BeginTime && @this.EndTime > record.EndTime)
            {
                return (record.BeginTime, record.EndTime);
            }

            return (null, null);
        }

        public static bool CheckIsCoincidence(this OTRecord @this, OTRecord record)
        {
            if (@this == null || record == null) return false;

            if ((@this.BeginTime > record.BeginTime && @this.BeginTime <= record.EndTime) ||
                (@this.EndTime >= record.BeginTime && @this.EndTime < record.EndTime) ||
                (@this.BeginTime < record.BeginTime && @this.EndTime > record.EndTime))
            {
                return true;
            }

            return false;
        }
    }
}
