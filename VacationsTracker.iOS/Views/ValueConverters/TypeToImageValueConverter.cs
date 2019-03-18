using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using UIKit;
using VacationsTracker.Core.Data;

namespace VacationsTracker.iOS.Views.ValueConverters
{
    internal class TypeToImageValueConverter : ValueConverter<VacationType, UIImage>
    {
        protected override ConversionResult<UIImage> Convert(VacationType value, Type targetType, object parameter, CultureInfo culture)
        {
            UIImage image;

            switch (value)
            {
                case VacationType.Regular:
                    image = UIImage.FromBundle("Icon_Request_Green");
                    break;
                case VacationType.Sick:
                    image = UIImage.FromBundle("Icon_Request_Plum");
                    break;
                case VacationType.Exceptional:
                    image = UIImage.FromBundle("Icon_Request_Gray");
                    break;
                case VacationType.Overtime:
                    image = UIImage.FromBundle("Icon_Request_Blue");
                    break;
                case VacationType.LeaveWithoutPay:
                    image = UIImage.FromBundle("Icon_Request_Dark");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return ConversionResult<UIImage>.SetValue(image);
        }
    }
}
