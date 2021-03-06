﻿using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using VacationsTracker.Core.Data;

namespace VacationsTracker.Core.Presentation.ValueConverters
{
    public class VacationTypeToIntConverter : ValueConverter<VacationType, int>
    {
        protected override ConversionResult<int> Convert(VacationType value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConversionResult<int>.SetValue((int)value);
        }

        protected override ConversionResult<VacationType> ConvertBack(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConversionResult<VacationType>.SetValue((VacationType)value);
        }
    }
}