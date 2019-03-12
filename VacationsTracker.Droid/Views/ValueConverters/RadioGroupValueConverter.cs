using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using VacationsTracker.Core.Data;

namespace VacationsTracker.Droid.Views.ValueConverters
{
    public class RadioGroupValueConverter : ValueConverter<VacationStatus, int>
    {
        protected override ConversionResult<int> Convert(VacationStatus value, Type targetType, object parameter, CultureInfo culture)
        {
            int radioButtonId;

            switch (value)
            {
                case VacationStatus.Approved:
                    radioButtonId = Resource.Id.approved_radio;
                    break;
                case VacationStatus.Closed:
                    radioButtonId = Resource.Id.closed_radio;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            return ConversionResult<int>.SetValue(radioButtonId);
        }

        protected override ConversionResult<VacationStatus> ConvertBack(int value, Type targetType, object parameter, CultureInfo culture)
        {
            VacationStatus status;
            switch (value)
            {
                case Resource.Id.approved_radio:
                    status = VacationStatus.Approved;
                    break;
                case Resource.Id.closed_radio:
                    status = VacationStatus.Closed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            return ConversionResult<VacationStatus>.SetValue(status);
        }
    }
}
