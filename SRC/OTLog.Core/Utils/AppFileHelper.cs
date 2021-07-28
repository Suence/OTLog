using OTLog.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OTLog.Core.Utils
{
    public static class AppFileHelper
    {
        /// <summary>
        /// 应用程序配置文件目录
        /// </summary>
        private static readonly string _settingsFileBasePath;

        /// <summary>
        /// 应用程序配置文件完整路径
        /// </summary>
        private static string _settingsFileFullPath;

        static AppFileHelper()
        {
            _settingsFileBasePath =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    $"SuenceSoft/OTLog/");
            _settingsFileFullPath = Path.Combine(_settingsFileBasePath, "Settings.xml");
        }

        /// <summary>
        /// 验证并修复应用程序文件
        /// </summary>
        public static void ValidateApplicationFiles()
        {
            if (!Directory.Exists(_settingsFileBasePath))
            {
                Directory.CreateDirectory(_settingsFileBasePath);
            }
            if (!File.Exists(_settingsFileFullPath))
            {
                CreateNewSettingsFile();
            }
        }

        /// <summary>
        /// 创建默认配置文件
        /// </summary>
        private static void CreateNewSettingsFile()
        {
            var doc = new XDocument();
            doc.Declaration = new XDeclaration("1.0", "UTF8", "yes");
            var objects =
                new XElement("ApplicationInfo",
                     new XElement(nameof(Theme), nameof(Theme.Default)));
            doc.Add(objects);
            doc.Save(_settingsFileFullPath);
        }

        /// <summary>
        /// 读取主题设置
        /// </summary>
        /// <returns></returns>
        public static Theme ReadUserThemeSettings()
            => Enum.TryParse(
                XDocument.Load(_settingsFileFullPath)
                               .Root.Element(nameof(Theme))
                               .Value, out Theme theme)
            ? theme
            : Theme.Default;

        /// <summary>
        /// 写入主题设置
        /// </summary>
        /// <param name="theme"></param>
        public static void WriteUserThemeSettings(Theme theme)
        {
            var doc = XDocument.Load(_settingsFileFullPath);
            doc.Root.Element(nameof(Theme)).Value = $"{theme}";
            doc.Save(_settingsFileFullPath);
        }
    }
}
