using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTLog.Core.Models
{
    public class OTRecord : BindableBase
    {
        #region private
        private DateTime? _beginTime;
        private DateTime? _endTime;
        private string _remark;
        #endregion

        public DateTime? BeginTime
        {
            get => _beginTime;
            set => SetProperty(ref _beginTime, value);
        }

        public DateTime? EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        public string Remark
        {
            get => _remark;
            set => SetProperty(ref _remark, value);
        }

        public TimeSpan? OTTime => _endTime - _beginTime;
    }
}
