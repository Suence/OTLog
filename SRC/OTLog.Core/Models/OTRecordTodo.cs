using OTLog.Core.Enums;
using Prism.Mvvm;
using System;

namespace OTLog.Core.Models
{
    public class OTRecordTodo : BindableBase
    {
        #region private
        private Guid _id;
        private DateTime _beginTime;
        private DateTime _endTime;
        private TodoStatus _status;
        #endregion

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public DateTime BeginTime
        {
            get => _beginTime;
            set => SetProperty(ref _beginTime, value);
        }

        public DateTime EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        public TodoStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
    }
}
