using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlexiMvvm.Operations;

namespace VacationsTracker.Core.Infrastructure
{
    public class ExceptionHandler : IErrorHandler
    {
        public Task HandleAsync(OperationContext context, OperationError<Exception> error, CancellationToken cancellationToken)
        {
            Debug.WriteLine("oops...");
            Debug.WriteLine(error.Exception);
            return Task.CompletedTask;
        }
    }
}
