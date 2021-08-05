using Newtonsoft.Json;
using OTLog.Core.Enums;
using OTLog.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static readonly string LinkFileFullPath;

        /// <summary>
        /// 应用程序配置文件完整路径
        /// </summary>
        public static string SettingsFileFullPath;

        public static string DataFileFullPath;

        static AppFileHelper()
        {
            _basePath =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    $"SuenceSoft/OTLog/");
            SettingsFileFullPath = Path.Combine(_basePath, "Settings.xml");
            DataFileFullPath = Path.Combine(_basePath, "data.json");

            string systemStartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            LinkFileFullPath = Path.Combine(systemStartupFolder, "OTLog.lnk");
        }

        // 创建快捷方式
        public static void CreateShortcut(string lnkFilePath, string args = "")
        {
            var shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var shortcut = shell.CreateShortcut(lnkFilePath);
            shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
            shortcut.Arguments = args;
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            shortcut.Save();
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
            if (!File.Exists(SettingsFileFullPath))
            {
                CreateNewSettingsFile();
            }
            if (!File.Exists(DataFileFullPath))
            {
                File.Create(DataFileFullPath);
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
                     new XElement(nameof(Theme), nameof(Theme.Default)),
                     new XElement("OpenAtBoot", "True"));
            doc.Add(objects);
            doc.Save(SettingsFileFullPath);
        }

        /// <summary>
        /// 读取主题设置
        /// </summary>
        /// <returns></returns>
        public static Theme ReadUserThemeSettings()
            => Enum.TryParse(
                XDocument.Load(SettingsFileFullPath)
                               .Root.Element(nameof(Theme))
                               .Value, out Theme theme)
            ? theme
            : Theme.Default;

        public static bool ReadAutoOpenStatus()
            => Boolean.TryParse(
                XDocument.Load(SettingsFileFullPath)
                         .Root
                         .Element("OpenAtBoot")
                         .Value, out bool openAtBoot)
               ? openAtBoot
               : true;

        public static void WriteAutoOpenStatus(bool status)
        {
            var doc = XDocument.Load(SettingsFileFullPath);
            doc.Root.Element("OpenAtBoot").Value = status.ToString();
            doc.Save(SettingsFileFullPath);
        }

        /// <summary>
        /// 写入主题设置
        /// </summary>
        /// <param name="theme"></param>
        public static void WriteUserThemeSettings(Theme theme)
        {
            var doc = XDocument.Load(SettingsFileFullPath);
            doc.Root.Element(nameof(Theme)).Value = $"{theme}";
            doc.Save(SettingsFileFullPath);
        }

        public static void SaveOTRecords(List<OTRecord> records)
        {
            string data = JsonConvert.SerializeObject(records);
            File.WriteAllText(DataFileFullPath, data);
        }

        public static List<OTRecord> GetOTRecords()
        {
            string data = File.ReadAllText(DataFileFullPath);
            return JsonConvert.DeserializeObject<List<OTRecord>>(data);
        }
    }
}
