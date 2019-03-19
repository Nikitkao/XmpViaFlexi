using System;
using Foundation;

namespace VacationsTracker.iOS.Extensions
{
    public static class DateTimeExtension
    {
        public static NSDate ToNsDateTime(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            return (NSDate)dateTime;
        }
    }
}
