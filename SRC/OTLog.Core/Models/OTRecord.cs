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
        private string _remark = String.Empty;
        private Guid _id;
        #endregion

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public DateTime? BeginTime
        {
            get => _beginTime;
            set
            {
                SetProperty(ref _beginTime, value);
                RaisePropertyChanged(nameof(OTTime));
            }
        }

        public DateTime? EndTime
        {
            get => _endTime;
            set
            {
                SetProperty(ref _endTime, value);
                RaisePropertyChanged(nameof(OTTime));
            }

        }

        public string Remark
        {
            get => _remark;
            set => SetProperty(ref _remark, value);
        }

        public TimeSpan? OTTime => _endTime - _beginTime;
    }
}
