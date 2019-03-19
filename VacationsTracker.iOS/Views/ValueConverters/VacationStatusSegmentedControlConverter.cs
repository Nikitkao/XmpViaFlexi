using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using VacationsTracker.Core.Data;

namespace VacationsTracker.iOS.Views.ValueConverters
{
    internal class VacationStatusSegmentedControlConverter : ValueConverter<VacationStatus, nint>
    {
        protected override ConversionResult<nint> Convert(VacationStatus value, Type targetType, object parameter, CultureInfo culture)
        {
            nint index;

            switch (value)
            {
                case VacationStatus.Approved:
                    index = 0;
                    break;

                case VacationStatus.Closed:
                    index = 1;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return ConversionResult<nint>.SetValue(index);
        }

        protected override ConversionResult<VacationStatus> ConvertBack(nint value, Type targetType, object parameter, CultureInfo culture)
        {
            VacationStatus status;

            switch (value)
            {
                case 0:
                    status = VacationStatus.Approved;
                    break;

                case 1:
                    status = VacationStatus.Closed;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return ConversionResult<VacationStatus>.SetValue(status);
        }
    }
}
