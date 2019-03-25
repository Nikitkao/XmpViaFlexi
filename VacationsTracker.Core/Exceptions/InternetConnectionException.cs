using System;

namespace VacationsTracker.Core.Exceptions
{
    public class InternetConnectionException : Exception
    {
        public InternetConnectionException(string noInternetConnectionTodoLocalize)
            : base(noInternetConnectionTodoLocalize)
        {
        }
    }
}
