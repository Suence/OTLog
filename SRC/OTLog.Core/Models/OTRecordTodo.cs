using OTLog.Core.Enums;
using Prism.Mvvm;
using System;

namespace OTLog.Core.Models
{
    public class OTRecordTodo : BindableBase
    {
        public Guid Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public TodoStatus Status { get; set; }
    }
}
