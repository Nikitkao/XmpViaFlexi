using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using VacationsTracker.Core.Presentation.ViewModels;

namespace VacationsTracker.Core.Presentation.ValueConverters
{
    public class DurationToString : ValueConverter<Duration, string>
    {
        protected override ConversionResult<string> Convert(Duration value, Type targetType, object parameter,
            CultureInfo culture)
        {
            return ConversionResult<string>.SetValue(
                $"{value?.Start.ToString(parameter?.ToString(), CultureInfo.InvariantCulture)} - {value?.End.ToString(parameter?.ToString(), CultureInfo.InvariantCulture)}".ToUpper());
        }
    }
}
