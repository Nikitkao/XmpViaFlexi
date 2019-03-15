using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Weak.Subscriptions;

namespace VacationsTracker.Droid.Views
{
    public static class SubscribtionExtensions
    {
        public static IDisposable ImageButtonClickWeakSubscribe(this ImageButton button, EventHandler onClick)
        {
            return new WeakEventSubscription<ImageButton>(
                button,
                (btn, handler) => btn.Click += handler,
                (btn, handler) => btn.Click -= handler,
                onClick);
        }

        public static IDisposable ViewGroupClickWeakSubscribe(this ViewGroup layout, EventHandler onClick)
        {
            return new WeakEventSubscription<ViewGroup>(layout,
                (lyt, handler) => lyt.Click += handler,
                (lyt, handler) => lyt.Click -= handler, onClick);
        }
    }
}