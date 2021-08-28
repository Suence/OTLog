using OTLog.Core.Enums;
using OTLog.Core.Models;
using OTLog.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace OTLog.Core.StaticObjects
{
    public static class GlobalObjectHolder
    {
        public static Config Config { get; set; }

        static GlobalObjectHolder()
        {
            ThemeColors = new List<ThemeColor>
            {
                new ThemeColor(Color.FromRgb(255, 185, 0), "黄金色"),
                new ThemeColor(Color.FromRgb(255, 140, 0), "金色"),
                new ThemeColor(Color.FromRgb(247, 99, 12), "亮橙黄色"),
                new ThemeColor(Color.FromRgb(202, 80, 16), "深橙黄色"),
                new ThemeColor(Color.FromRgb(218, 59, 1), "铁锈色"),
                new ThemeColor(Color.FromRgb(239, 135, 80), "浅铁锈色"),
                new ThemeColor(Color.FromRgb(209, 52, 56), "砖红色"),
                new ThemeColor(Color.FromRgb(255, 67, 67), "中等红色"),
                new ThemeColor(Color.FromRgb(231, 72, 86), "浅红色"),
                new ThemeColor(Color.FromRgb(232, 17, 35), "红色"),
                new ThemeColor(Color.FromRgb(234, 0, 94), "亮玫红"),
                new ThemeColor(Color.FromRgb(195, 0, 82), "玫瑰红"),
                new ThemeColor(Color.FromRgb(227, 0, 140), "浅紫红色"),
                new ThemeColor(Color.FromRgb(191, 0, 119), "玫红色"),
                new ThemeColor(Color.FromRgb(194, 57, 179), "浅兰花紫"),
                new ThemeColor(Color.FromRgb(154, 0, 137), "兰花紫色"),
                new ThemeColor(Color.FromRgb(0, 120, 212), "蓝色"),
                new ThemeColor(Color.FromRgb(0, 99, 177), "深蓝色"),
                new ThemeColor(Color.FromRgb(142, 140, 216), "紫影色"),
                new ThemeColor(Color.FromRgb(107, 105, 214), "深紫影色"),
                new ThemeColor(Color.FromRgb(135, 100, 184), "柔和彩虹色"),
                new ThemeColor(Color.FromRgb(116, 77, 169), "鸢尾花色"),
                new ThemeColor(Color.FromRgb(177, 70, 194), "浅紫红色"),
                new ThemeColor(Color.FromRgb(136, 23, 152), "紫红色"),
                new ThemeColor(Color.FromRgb(0, 153, 188), "亮酷蓝色"),
                new ThemeColor(Color.FromRgb(45, 125, 154), "酷蓝色"),
                new ThemeColor(Color.FromRgb(0, 183, 195), "海沫绿"),
                new ThemeColor(Color.FromRgb(3, 131, 135), "蓝绿色"),
                new ThemeColor(Color.FromRgb(0, 178, 148), "浅薄荷色"),
                new ThemeColor(Color.FromRgb(1, 133, 116), "深薄荷色"),
                new ThemeColor(Color.FromRgb(0, 204, 106), "浅绿色"),
                new ThemeColor(Color.FromRgb(16, 137, 62), "运动绿"),
                new ThemeColor(Color.FromRgb(122, 117, 116), "灰色"),
                new ThemeColor(Color.FromRgb(93, 90, 88), "灰棕色"),
                new ThemeColor(Color.FromRgb(104, 118, 138), "钢蓝色"),
                new ThemeColor(Color.FromRgb(81, 92, 107), "金属蓝色"),
                new ThemeColor(Color.FromRgb(86, 124, 115), "浅苔藓色"),
                new ThemeColor(Color.FromRgb(72, 104, 96), "苔藓色"),
                new ThemeColor(Color.FromRgb(73, 130, 5), "草绿色"),
                new ThemeColor(Color.FromRgb(16, 124, 16), "绿色"),
                new ThemeColor(Color.FromRgb(118, 118, 118), "天灰色"),
                new ThemeColor(Color.FromRgb(76, 74, 72), "雾灰色"),
                new ThemeColor(Color.FromRgb(105, 121, 126), "蓝灰色"),
                new ThemeColor(Color.FromRgb(74, 84, 89), "深灰色"),
                new ThemeColor(Color.FromRgb(100, 124, 100), "艳绿色"),
                new ThemeColor(Color.FromRgb(82, 94, 84), "鼠尾草色"),
                new ThemeColor(Color.FromRgb(132, 117, 69), "沙漠迷彩"),
                new ThemeColor(Color.FromRgb(126, 115, 95), "保护色"),
            };
            Employees = new Lazy<List<Employee>>(() => new List<Employee>
            {
                new ("大华", "biye", Gender.Female),
                new ("坚哥", "chenjian", Gender.Male),
                new ("陈老板", "chenshiqi", Gender.Male),
                new ("楚文龙", "chuwenlong", Gender.Male),
                new ("崔春艳", "cuichunyan", Gender.Female),
                new ("丁露丹", "dingludan", Gender.Female),
                new ("范范", "fanmengdi", Gender.Female),
                new ("小范", "fanjinchang", Gender.Male),
                new ("冯主任", "fengcheng", Gender.Male),
                new ("高歌", "gaoge", Gender.Female),
                new ("关嘉莹", "guanjiaying", Gender.Female),
                new ("古老板", "gujiwei", Gender.Male),
                new ("韩蜜慧", "hanmihui", Gender.Female),
                new ("玖哥", "huangjiangjiu", Gender.Male),
                new ("霍嘉宁", "huojianing", Gender.Female),
                new ("勇哥", "kangyong", Gender.Male),
                new ("雷兵", "leibing", Gender.Male),
                new ("Leo", "zhangshiyu", Gender.Male),
                new ("李定明", "lidingming", Gender.Male),
                new ("杰克李", "lifeihong", Gender.Male),
                new ("李皇奕", "lihuangyi", Gender.Male),
                new ("李盼", "lipan", Gender.Female),
                new ("李平", "liping", Gender.Female),
                new ("刘丹阳", "liudanyang", Gender.Female),
                new ("刘洪坤", "liuhongkun", Gender.Male),
                new ("刘欢", "liuhuan", Gender.Female),
                new ("刘会芳", "liuhuifang", Gender.Female),
                new ("刘嘉宁", "liujianing", Gender.Male),
                new ("刘俊", "liujun", Gender.Male),
                new ("刘彤丹", "liutongdan", Gender.Female),
                new ("刘文苹", "liuwenping", Gender.Female),
                new ("刘智", "liuzhi", Gender.Male),
                new ("李想成", "lixiangcheng", Gender.Male),
                new ("可可大人", "lvkeying", Gender.Female),
                new ("马雨培", "mayupei", Gender.Female),
                new ("孟军良", "mengjunliang", Gender.Male),
                new ("母老板", "mouyisheng", Gender.Male),
                new ("穆玉清", "muyuqing", Gender.Male),
                new ("牛海江", "niuhaijiang", Gender.Male),
                new ("潘嗣捷", "pansijie", Gender.Male),
                new ("佐伊", "qinlinlin", Gender.Female),
                new ("亓燕", "qiyan", Gender.Female),
                new ("石长松", "shichangsong", Gender.Male),
                new ("宋蕾", "songlei", Gender.Female),
                new ("雨晴", "yuqing", Gender.Female),
                new ("苏绘", "suhui", Gender.Female),
                new ("孙超杰", "sunchaojie", Gender.Male),
                new ("孙婷婷", "suntingting", Gender.Female),
                new ("孙肖雅", "sunxiaoya", Gender.Female),
                new ("孙喆", "sunzhe", Gender.Male),
                new ("塔库尔", "tawquir", Gender.Male),
                new ("田水泉", "tianshuiquan", Gender.Male),
                new ("小万", "wanfangyang", Gender.Male),
                new ("王贺", "wanghe", Gender.Male),
                new ("王瑾", "wangjin", Gender.Female),
                new ("王萍", "wangping", Gender.Female),
                new ("王亚楠", "wangyanan", Gender.Female),
                new ("王颖悕", "wangyingxi", Gender.Female),
                new ("王泽", "wangze", Gender.Male),
                new ("魏琳峰", "weilinfeng", Gender.Male),
                new ("坤坤", "kunkun", Gender.Female),
                new ("吴静", "wujing", Gender.Female),
                new ("吴敏生", "wuminsheng", Gender.Male),
                new ("吴文涛", "wuwentao", Gender.Male),
                new ("Janet", "wuzhenglei", Gender.Female),
                new ("谢秀红", "xiexiuhong", Gender.Female),
                new ("邢宝珠", "xingbaozhu", Gender.Female),
                new ("薛孟", "xuemeng", Gender.Male),
                new ("Jeff", "xujunjie", Gender.Male),
                new ("杨育宽", "yangyukuan", Gender.Male),
                new ("姚灿", "yaocan", Gender.Female),
                new ("姚郭帅", "yaoguoshuai", Gender.Male),
                new ("龙龙", "yaoweilong", Gender.Male),
                new ("Shaelyn", "yaoziyang", Gender.Female),
                new ("尹妍榀", "yinyanpin", Gender.Female),
                new ("张博博", "zhangbobo", Gender.Male),
                new ("张健", "zhangjian", Gender.Male),
                new ("张晶", "zhangjing", Gender.Female),
                new ("张九龙", "zhangjiulong", Gender.Male),
                new ("张宁", "zhangning", Gender.Male),
                new ("盈仔", "zhangruiying", Gender.Female),
                new ("张原源", "zhangyuanyuan", Gender.Female),
                new ("张悦", "zhangyue", Gender.Female),
                new ("张子衡", "zhangziheng", Gender.Male),
                new ("甄晴", "zhenqing", Gender.Female),
                new ("周波", "zhoubo", Gender.Male),
                new ("周菁", "zhoujing", Gender.Female),
                new ("周郁朝", "zhouyuchao", Gender.Male),
                new ("朱琳", "zhulin", Gender.Female)
            });

            InitConfig();
        }

        public static List<ThemeColor> ThemeColors { get; }
        public static Lazy<List<Employee>> Employees { get; }

        private static void InitConfig()
        {
            if (!File.Exists(AppFileHelper.SettingsFileFullPath))
            {
                Employee currentEmployee = GetCurrentEmployee();
                Config = new Config
                {
                    NotificationAfterMin = true,
                    OpenAtBoot = true,
                    ThemeColor = Color.FromRgb(0, 120, 212),
                    Theme = Theme.DarkTheme,
                    //UserName = WindowsIdentity.GetCurrent().Name;
                    Employee = currentEmployee
                };

                using (File.Create(AppFileHelper.SettingsFileFullPath)) { }

                AppFileHelper.SaveAppConfig(Config);
                return;
            }
            Config = AppFileHelper.LoadAppConfig();


            Employee GetCurrentEmployee()
            {
#if DEBUG
                string englishName = Environment.GetEnvironmentVariable("OTLogUserName", EnvironmentVariableTarget.User);
#else
                string englishName = Environment.UserName;
#endif
                Employee employee = Employees.Value.FirstOrDefault(e => e.EnglishName.ToLower() == englishName.ToLower())
                                    ?? new(englishName, englishName, Gender.Unknown);

                string friendlyName = employee.Gender switch
                {
                    Gender.Male => $"伟大的{employee.Name}",
                    Gender.Female => $"亲爱的{employee.Name}",
                    _ => employee.Name
                };
                employee.Name = friendlyName;
                employee.DefaultName = friendlyName;

                return employee;
            }
        }
    }
}
