using System.Windows.Media;

namespace OTLog.Core.Models
{
    public struct ThemeColor
    {
        public ThemeColor(Color color, string remark)
            => (Color, Remark) = (color, remark);

        public Color Color { get; }
        public string Remark { get; }
    }
}
