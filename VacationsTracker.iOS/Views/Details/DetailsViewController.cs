using System;
using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using FlexiMvvm.Views;
using UIKit;
using VacationsTracker.Core.Presentation.ValueConverters;
using VacationsTracker.Core.Presentation.ViewModels.Details;
using VacationsTracker.Core.Resources;
using VacationsTracker.iOS.Extensions;
using VacationsTracker.iOS.Views.Details.PageControl;
using VacationsTracker.iOS.Views.ValueConverters;

namespace VacationsTracker.iOS.Views.Details
{
    public class DetailsViewController : FlxBindableViewController<DetailsViewModel, VacationDetailsParameters>
    {
        private UIPageViewController VacationsPageViewController { get; set; }

        private UIPageViewControllerObservableDataSource VacationsDataSource { get; set; }

        private UIBarButtonItem SaveButton { get; } = new UIBarButtonItem(Strings.SaveNavigationButton_Text, UIBarButtonItemStyle.Done, null);

        private readonly UITapGestureRecognizer _startRecognizer;
        private readonly UITapGestureRecognizer _endRecognizer;

        public new DetailsView View
        {
            get => (DetailsView)base.View.NotNull();
            set => base.View = value;
        }

        public DetailsViewController(VacationDetailsParameters parameters) : base(parameters)
        {
            _startRecognizer = new UITapGestureRecognizer(OnStartDayViewTap);
            _endRecognizer = new UITapGestureRecognizer(OnEndDayViewTap);
        }

        public override void LoadView()
        {
            View = new DetailsView();

            Title = Strings.VacationDetailsPage_Title;

            VacationsPageViewController = new UIPageViewController(
                UIPageViewControllerTransitionStyle.Scroll,
                UIPageViewControllerNavigationOrientation.Horizontal);

            VacationsDataSource = new UIPageViewControllerObservableDataSource(
                VacationsPageViewController,
                PagerFactory);

            this.AddChildViewControllerAndView(VacationsPageViewController, View.VacationsPager);

            //VacationsDataSource.CurrentItemIndexChangedWeakSubscribe(HandleEventHandler);

            VacationsPageViewController.DataSource = VacationsDataSource;

            NavigationItem.RightBarButtonItem = SaveButton;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            View.StartDayView.AddGestureRecognizer(_startRecognizer);
            View.EndDayView.AddGestureRecognizer(_endRecognizer);
        }

        public override void ViewDidDisappear(bool animated)
        {
            View.StartDayView.RemoveGestureRecognizer(_startRecognizer);
            View.EndDayView.RemoveGestureRecognizer(_endRecognizer);

            base.ViewDidDisappear(animated);
        }

        private void OnStartDayViewTap()
        {
            View.VacationStartDatePicker.SetDate(ViewModel.Start.ToNsDateTime(), true);

            View.EndDateConstraintBottom.Active = false;
            View.EndDateConstraintBelow.Active = true;

            View.StartDateConstraintBottom.Active = !View.StartDateConstraintBottom.Active;
            View.StartDateConstraintBelow.Active = !View.StartDateConstraintBelow.Active;

            UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseIn, View.LayoutIfNeeded, null);
        }

        private void OnEndDayViewTap()
        {
            View.VacationEndDatePicker.SetDate(ViewModel.End.ToNsDateTime(), true);

            View.StartDateConstraintBottom.Active = false;
            View.StartDateConstraintBelow.Active = true;

            View.EndDateConstraintBottom.Active = !View.EndDateConstraintBottom.Active;
            View.EndDateConstraintBelow.Active = !View.EndDateConstraintBelow.Active;

            UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseIn, View.LayoutIfNeeded, null);
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(VacationsDataSource)
                .For(v => v.Items)
                .To(vm => vm.VacationTypes);

            bindingSet.Bind(VacationsDataSource)
                .For(v => v.CurrentItemIndexAndCurrentItemIndexChangedBinding())
                .To(vm => vm.VacationType)
                .WithConvertion<VacationTypeToIntConverter>();

            bindingSet.Bind(View.StartDayLabel)
                .For(v => v.Text)
                .To(vm => vm.Start)
                .WithConvertion<DateTimeToStringConverter>("dd");

            bindingSet.Bind(View.StartMonthLabel)
                .For(v => v.Text)
                .To(vm => vm.Start)
                .WithConvertion<DateTimeToStringConverter>("MMM");

            bindingSet.Bind(View.StartYearLabel)
                .For(v => v.Text)
                .To(vm => vm.Start)
                .WithConvertion<DateTimeToStringConverter>("yyyy");

            bindingSet.Bind(View.EndDayLabel)
                .For(v => v.Text)
                .To(vm => vm.End)
                .WithConvertion<DateTimeToStringConverter>("dd");

            bindingSet.Bind(View.EndMonthLabel)
                .For(v => v.Text)
                .To(vm => vm.End)
                .WithConvertion<DateTimeToStringConverter>("MMM");

            bindingSet.Bind(View.EndYearLabel)
                .For(v => v.Text)
                .To(vm => vm.End)
                .WithConvertion<DateTimeToStringConverter>("yyyy");

            bindingSet.Bind(View.StatusSegmentedControl)
                .For(v => v.SelectedSegmentAndValueChangedBinding())
                .To(vm => vm.VacationStatus)
                .WithConvertion<VacationStatusSegmentedControlConverter>();

            bindingSet.Bind(SaveButton)
                .For(v => v.NotNull().ClickedBinding())
                .To(vm => vm.SaveCommand);

            bindingSet.Bind(View.VacationStartDatePicker)
                .For(v => v.DateAndValueChangedBinding())
                .To(vm => vm.Start);

            bindingSet.Bind(View.VacationEndDatePicker)
                .For(v => v.DateAndValueChangedBinding())
                .To(vm => vm.End);

            bindingSet.Bind(UIApplication.SharedApplication)
                .For(v => v.NetworkActivityIndicatorVisible)
                .To(vm => vm.Busy);

            bindingSet.Bind(View.VacationPageControl)
                .For(v => v.CurrentPage)
                .To(vm => vm.VacationType);
        }

        private UIViewController PagerFactory(object parameters)
        {
            if (parameters is VacationTypeItemParameters pagerParameters)
            {
                return new VacationTypePagerViewController(pagerParameters);
            }

            throw new ArgumentException(nameof(parameters));
        }
    }
}
