using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;

namespace VacationsTracker.Core.Presentation.ValueConverters
{
    public class EnumToStringConverter : ValueConverter<Enum, string>
    {
        protected override ConversionResult<string> Convert(Enum value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConversionResult<string>.SetValue(value?.ToString());
        }
    }
}
