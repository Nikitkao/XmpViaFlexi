using System;
using System.Collections.Generic;
using System.Text;
using FlexiMvvm.Operations;

namespace VacationsTracker.Core.Operations
{
    public static class OperationBuilderExtensions
    {
        public static IOperationBuilder WithLoadingNotification(

            this IOperationBuilder operationBuilder,
            int delay = 0,
            int duration = 0,
            bool isCancellable = false)
        {
            return operationBuilder
                .WithNotification(new LoadingOperationNotification(delay, duration, isCancellable));
        }
        public static IOperationBuilder WithInternetConnectionCondition(
            this IOperationBuilder operationBuilder)
        {
            return operationBuilder
                .WithCondition(new InternetConnectionCondition());
        }
    }
}
