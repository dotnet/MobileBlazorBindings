using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinTodoXaml.Converters
{
    public class StrikethroughIfDoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TextDecorations.Strikethrough : TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // This is used only for one-way bindings, so converting back is not supported
            throw new NotSupportedException();
        }
    }
}
