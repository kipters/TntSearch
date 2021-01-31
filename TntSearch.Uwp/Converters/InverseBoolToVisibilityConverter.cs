using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TntSearch.Uwp.Converters
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                bool b when b == false => Visibility.Visible,
                bool => Visibility.Collapsed,
                _ => throw new InvalidCastException()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                bool b when b == false => Visibility.Visible,
                bool => Visibility.Collapsed,
                _ => throw new InvalidCastException()
            };
        }
    }
}
