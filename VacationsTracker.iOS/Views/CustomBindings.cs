using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using UIKit;

namespace VacationsTracker.iOS.Views
{
    public static class CustomBindings
    {
        /*
        public static TargetItemBinding<UIDatePicker, string> TextChangedBinding(
            this IItemReference<UIDatePicker> textViewReference,
            bool trackCanExecuteCommandChanged = false)
        {
            if (textViewReference == null)
                throw new ArgumentNullException(nameof(textViewReference));

            return new TargetItemTwoWayCustomBinding<TextView, string, TextChangedEventArgs>(
                textViewReference,
                (textView, eventHandler) => textView.NotNull().da += eventHandler,
                (textView, eventHandler) => textView.NotNull().TextChanged -= eventHandler,
                (textView, canExecuteCommand) =>
                {
                    if (trackCanExecuteCommandChanged)
                    {
                        textView.NotNull().Enabled = canExecuteCommand;
                    }
                },
                (textView, eventArgs) => textView.NotNull().Text,
                () => $"{nameof(TextView.TextChanged)}");
        }*/
    }
}
