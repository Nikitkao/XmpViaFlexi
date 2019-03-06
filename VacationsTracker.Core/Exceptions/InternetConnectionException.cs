using System;
using System.Collections.Generic;
using System.Text;

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
