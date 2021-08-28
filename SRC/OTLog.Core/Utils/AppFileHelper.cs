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

        // 数据文件完整路径
        public static string DataFileFullPath;


        public static string OTRecordTodoFileFullPath;
        static AppFileHelper()
        {
            _basePath =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    $"SuenceSoft/OTLog/");
            SettingsFileFullPath = Path.Combine(_basePath, "settings.json");
            DataFileFullPath = Path.Combine(_basePath, "data.json");
            OTRecordTodoFileFullPath = Path.Combine(_basePath, "todo.json");

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

        public static async void CreateShortcutAsync(string lnkFilePath, string args = "")
            => await Task.Run(() => CreateShortcut(lnkFilePath, args));

        public static List<OTRecordTodo> GetOTRecordToDos()
        {
            string data = File.ReadAllText(OTRecordTodoFileFullPath);
            if (String.IsNullOrWhiteSpace(data))
            {
                return new List<OTRecordTodo>();
            }

            return JsonConvert.DeserializeObject<List<OTRecordTodo>>(data);
        }

        public static Task<List<OTRecordTodo>> GetOTRecordToDosAsync()
            => Task.Run(() => GetOTRecordToDos());

        /// <summary>
        /// 验证并修复应用程序文件
        /// </summary>
        public static void ValidateApplicationFiles()
        {
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
            if (!File.Exists(DataFileFullPath))
            {
                File.Create(DataFileFullPath);
            }
            if (!File.Exists(OTRecordTodoFileFullPath))
            {
                File.Create(OTRecordTodoFileFullPath);
            }
        }

        public static void SaveRecordToDo(List<OTRecordTodo> oTRecordTodos)
        {
            string data = JsonConvert.SerializeObject(oTRecordTodos);
            File.WriteAllText(OTRecordTodoFileFullPath, data);
        }

        public static async void SaveRecordToDoAsync(List<OTRecordTodo> oTRecordTodos)
            => await Task.Run(() => SaveRecordToDo(oTRecordTodos));

        public static void SaveOTRecords(List<OTRecord> records)
        {
            string data = JsonConvert.SerializeObject(records);
            File.WriteAllText(DataFileFullPath, data);
        }

        public static async void SaveOTRecordsAsync(List<OTRecord> records)
            => await Task.Run(() => SaveOTRecords(records));


        public static List<OTRecord> GetOTRecords()
        {
            string data = File.ReadAllText(DataFileFullPath);
            if (String.IsNullOrWhiteSpace(data))
            {
                return new List<OTRecord>();
            }

            return JsonConvert.DeserializeObject<List<OTRecord>>(data);
        }

        public static Task<List<OTRecord>> GetOTRecordsAsync()
            => Task.Run(() => GetOTRecords());


        public static Config LoadAppConfig()
        {
            string configString = File.ReadAllText(SettingsFileFullPath);
            return JsonConvert.DeserializeObject<Config>(configString);
        }

        public static Task<Config> LoadAppConfigAsync()
            => Task.Run(() => LoadAppConfig());

        public static void SaveAppConfig(Config config)
        {
            string configString = JsonConvert.SerializeObject(config);
            File.WriteAllText(SettingsFileFullPath, configString);
        }

        public static async void SaveAppConfigAsync(Config config)
            => await Task.Run(() => SaveAppConfig(config));
    }
}
