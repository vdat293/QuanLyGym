using System;
using System.Globalization;
using System.Windows.Data;

namespace QuanLyPhongGym.Utils
{
    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return 0;

            if ((bool)value)
            {
                if (int.TryParse(parameter.ToString(), out int result))
                    return result;
            }

            return Binding.DoNothing;
        }
    }
}
