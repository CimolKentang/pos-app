using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public string Format { get; set; } = "dd/MM/yyyy";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                string format = parameter as string ?? Format;
                return dateTime.ToString(format);
            }

            return value;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}