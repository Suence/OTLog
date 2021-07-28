using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OTLog.Controls.Decorators
{
    public class IlluminationDecorator : Decorator
    {
        public Color CenterColor { get; set; }
        protected override Size MeasureOverride(Size constraint)
        {
            if (this.Child is null)
            {
                return Size.Empty;
            }
            Child.Measure(constraint);
            return Child.DesiredSize;
        }
        public static readonly DependencyProperty BackgroundColorProperty;
        static IlluminationDecorator()
        {
            BackgroundColorProperty =
                DependencyProperty.Register(
                    nameof(BackgroundColor),
                    typeof(Color),
                    typeof(IlluminationDecorator),
                    new FrameworkPropertyMetadata
                    {
                        DefaultValue = Colors.Transparent,
                        AffectsRender = true
                    });
        }
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InvalidateVisual();
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            InvalidateVisual();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var bounds = new Rect(0, 0, ActualWidth, ActualHeight);
            drawingContext.DrawRectangle(GetForegoundBrush(), default, bounds);
        }

        private Brush GetForegoundBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);
            }

            var brush = new RadialGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop
                    {
                        //Color = Color.FromArgb(0x30, 0x00, 0x00, 0x00),
                        Color = CenterColor,
                        Offset = 0
                    },
                    new GradientStop
                    {
                        Color = Colors.Transparent,
                        Offset = 1
                    }
                }
            };
            Point absoluteGradientOrigin = Mouse.GetPosition(this);
            var relativeGradientOrigin = new Point(
                absoluteGradientOrigin.X / ActualWidth,
                absoluteGradientOrigin.Y / ActualHeight);
            brush.GradientOrigin = relativeGradientOrigin;
            brush.Center = relativeGradientOrigin;
            brush.RadiusX = 1;
            brush.RadiusY = 4;
            return brush;
        }
    }
}
