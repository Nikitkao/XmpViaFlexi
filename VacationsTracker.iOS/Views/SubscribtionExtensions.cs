using System;
using FlexiMvvm.Collections;
using FlexiMvvm.Weak.Subscriptions;

namespace VacationsTracker.iOS.Views
{
    public static class SubscribtionExtensions
    {
        public static IDisposable CurrentItemIndexChangedWeakSubscribe(this UIPageViewControllerObservableDataSource button, EventHandler<IndexChangedEventArgs> onClick)
        {
            return new WeakEventSubscription<UIPageViewControllerObservableDataSource, IndexChangedEventArgs>(
                button,
                (btn, handler) => btn.CurrentItemIndexChanged += handler,
                (btn, handler) => btn.CurrentItemIndexChanged -= handler,
                onClick);
        }
    }
}
