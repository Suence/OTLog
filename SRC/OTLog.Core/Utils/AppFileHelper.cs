using Newtonsoft.Json;
using OTLog.Core.Enums;
using OTLog.Core.Models;
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
        private static readonly string _basePath;

        /// <summary>
        /// 应用程序配置文件完整路径
        /// </summary>
        private static string _settingsFileFullPath;

        private static string _dataFileFullPath;

        static AppFileHelper()
        {
            _basePath =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    $"SuenceSoft/OTLog/");
            _settingsFileFullPath = Path.Combine(_basePath, "Settings.xml");
            _dataFileFullPath = Path.Combine(_basePath, "data.json");
        }

        /// <summary>
        /// 验证并修复应用程序文件
        /// </summary>
        public static void ValidateApplicationFiles()
        {
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
            if (!File.Exists(_settingsFileFullPath))
            {
                CreateNewSettingsFile();
            }
            if (!File.Exists(_dataFileFullPath))
            {
                File.Create(_dataFileFullPath);
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

        public static void SaveOTRecords(List<OTRecord> records)
        {
            string data = JsonConvert.SerializeObject(records);
            File.WriteAllText(_dataFileFullPath, data);
        }

        public static List<OTRecord> GetOTRecords()
        {
            string data = File.ReadAllText(_dataFileFullPath);
            return JsonConvert.DeserializeObject<List<OTRecord>>(data);
        }
    }
}
