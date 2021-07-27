using System;
using System.Windows;
using System.Windows.Controls;

namespace OTLog.UI.Core.AttachedProperties
{
    public class BindableBlackoutDates : DependencyObject
    {
        public static DependencyProperty RegisterBlackoutDatesProperty =
            DependencyProperty.RegisterAttached(
                "RegisterBlackoutDates",
                typeof(DateTime),
                typeof(BindableBlackoutDates),
                new PropertyMetadata(default(DateTime), OnRegisterCommandBindingChanged));

        public static void SetRegisterBlackoutDates(UIElement element, DateTime value)
            => element?.SetValue(RegisterBlackoutDatesProperty, value);

        public static DateTime GetRegisterBlackoutDates(UIElement element)
            => (DateTime)element?.GetValue(RegisterBlackoutDatesProperty);

        private static void OnRegisterCommandBindingChanged(
            DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is DatePicker)) return;

            var element = sender as DatePicker;
            element.BlackoutDates.Clear();
            element.BlackoutDates.Add(new CalendarDateRange
            {
                Start = (DateTime)e.NewValue
            });
        }
    }
}
