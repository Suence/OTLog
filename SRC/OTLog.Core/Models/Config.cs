using OTLog.Core.Enums;
using Prism.Mvvm;
using System;
using System.Windows.Media;

namespace OTLog.Core.Models
{
    public class Config : BindableBase
    {
        #region private
        private bool _openAtBoot;
        private bool _notificationAfterMin;
        private Color _themeColor;
        private Theme _theme;
        private string _userName;
        private bool _useSystemThemeColorScheme;
        #endregion
        public bool OpenAtBoot
        {
            get => _openAtBoot;
            set => SetProperty(ref _openAtBoot, value);
        }

        public bool NotificationAfterMin
        {
            get => _notificationAfterMin;
            set => SetProperty(ref _notificationAfterMin, value);
        }
        public bool UseSystemThemeColorScheme
        {
            get => _useSystemThemeColorScheme;
            set => SetProperty(ref _useSystemThemeColorScheme, value);
        }
        public Color ThemeColor
        {
            get => _themeColor;
            set => SetProperty(ref _themeColor, value);
        }
        public Theme Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, String.IsNullOrEmpty(value.Trim()) ? Environment.UserName : value.Trim());
        }
    }
}
