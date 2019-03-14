using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;

namespace VacationsTracker.Core.Presentation.ValueConverters
{
    public class DateTimeToStringConverter : ValueConverter<DateTime, string>
    {
        protected override ConversionResult<string> Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value.ToString(parameter?.ToString(), CultureInfo.InvariantCulture);

            return ConversionResult<string>.SetValue(s);
        }
    }
}
