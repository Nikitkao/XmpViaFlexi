using System;
using Android.Support.V4.Widget;
using Android.Text;
using Android.Widget;
using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using FlexiMvvm.Weak.Subscriptions;

namespace VacationsTracker.Droid.Views
{
    public static class CustomBindings
    {
        public static TargetItemBinding<ImageView, int> SetImageResourceBinding(
            this IItemReference<ImageView> imageViewReference)
        {
            return new TargetItemOneWayCustomBinding<ImageView, int>(
                imageViewReference,
                (imageView, resId) => { imageView.SetImageResource(resId); },
                () => "SetImageResource");
        }

        public static TargetItemBinding<SwipeRefreshLayout, bool> ValueChangedBinding(
            this IItemReference<SwipeRefreshLayout> switchReference,
            bool trackCanExecuteCommandChanged = false)
        {
            if (switchReference == null)
                throw new ArgumentNullException(nameof(switchReference));

            return new TargetItemOneWayToSourceCustomBinding<SwipeRefreshLayout, bool>(
                switchReference,
                (refresher, eventHandler) => refresher.NotNull().Refresh += eventHandler,
                (refresher, eventHandler) => refresher.NotNull().Refresh -= eventHandler,
                (refresher, canExecuteCommand) =>
                {
                    if (trackCanExecuteCommandChanged)
                    {
                        refresher.NotNull().Refreshing = canExecuteCommand;
                    }
                },
                refresher => refresher.NotNull().Refreshing,
                () => "ValueChanged");
        }

        public static TargetItemBinding<TextView, string> TextChangedBinding(
            this IItemReference<TextView> textViewReference,
            bool trackCanExecuteCommandChanged = false)
        {
            if (textViewReference == null)
                throw new ArgumentNullException(nameof(textViewReference));

            return new TargetItemOneWayToSourceCustomBinding<TextView, string, TextChangedEventArgs>(
                textViewReference,
                (textView, eventHandler) => textView.NotNull().TextChanged += eventHandler,
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
        }
    }
}
