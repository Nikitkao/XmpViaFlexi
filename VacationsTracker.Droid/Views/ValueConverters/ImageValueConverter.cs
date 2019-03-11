using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using VacationsTracker.Core.Data;

namespace VacationsTracker.Droid.Views.ValueConverters
{
    public class ImageValueConverter : ValueConverter<VacationType, int>
    {
        protected override ConversionResult<int> Convert(VacationType value, Type targetType, object parameter,
            CultureInfo culture)
        {
            int imageId;

            switch (value)
            {
                case VacationType.Regular:
                    imageId = Resource.Drawable.Icon_Request_Green;
                    break;
                case VacationType.Sick:
                    imageId = Resource.Drawable.Icon_Request_Plum;
                    break;
                case VacationType.Exceptional:
                    imageId = Resource.Drawable.Icon_Request_Gray;
                    break;
                case VacationType.Overtime:
                    imageId = Resource.Drawable.Icon_Request_Blue;
                    break;
                case VacationType.LeaveWithoutPay:
                    imageId = Resource.Drawable.Icon_Request_Dark;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return ConversionResult<int>.SetValue(imageId);
        }
    }
}
