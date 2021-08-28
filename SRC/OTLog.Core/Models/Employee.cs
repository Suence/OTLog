using OTLog.Core.Enums;
using Prism.Mvvm;
using System;

namespace OTLog.Core.Models
{
    public class Employee : BindableBase
    {
        private string _name;

        public Employee(string name, string englishName, Gender gender)
            => (Name, EnglishName, Gender) = (name, englishName, gender);

        public string Name
        {
            get => _name;
            set
            {
                string newName = String.IsNullOrWhiteSpace(value) ? DefaultName : value.Trim();
                SetProperty(ref _name, newName);
            }
        }

        public string DefaultName { get; set; }
        public string EnglishName { get; set; }
        public Gender Gender { get; set; }
    }
}
