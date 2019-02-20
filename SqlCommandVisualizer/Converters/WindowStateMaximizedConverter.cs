using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SqlCommandVisualizer.Converters
{
    public class WindowStateMaximizedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? WindowState.Maximized : WindowState.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((WindowState)value) == WindowState.Maximized;
        }
    }
}
