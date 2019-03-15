using System;
using Android.App;
using Android.OS;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using FlexiMvvm.ValueConverters;
using FlexiMvvm.Views;
using FlexiMvvm.Views.V7;
using FlexiMvvm.Weak.Subscriptions;
using VacationsTracker.Core.Presentation.ValueConverters;
using VacationsTracker.Core.Presentation.ViewModels.Details;
using VacationsTracker.Droid.Views.ValueConverters;
using Fragment = Android.Support.V4.App.Fragment;

namespace VacationsTracker.Droid.Views.Details
{
    [Activity(Label = "DetailsActivity")]
    public class DetailsActivity : FlxBindableAppCompatActivity<DetailsViewModel, VacationDetailsParameters>
    {
        private DetailsActivityViewHolder ViewHolder { get; set; }

        private FragmentPagerObservableAdapter VacationTypesAdapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_details);
            
            ViewHolder = new DetailsActivityViewHolder(this);

            SetTypePagerAdapter();
            SetPagerBottomDots();

            ViewHolder.DateStart.ClickWeakSubscribe(DateStartOnClick);
            ViewHolder.DateEnd.ClickWeakSubscribe(DateEndOnClick);
        }

        private void DateStartOnClick(object sender, EventArgs e)
        {
            var picker = new DatePickerDialog(this, CallBackStart, ViewModel.Start.Year,
                ViewModel.Start.Month - 1, ViewModel.Start.Day);
            picker.Show();
        }

        private void CallBackStart(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.Start = e.Date;
        }

        private void DateEndOnClick(object sender, EventArgs e)
        {
            var picker = new DatePickerDialog(this, CallBackEnd, ViewModel.End.Year,
                ViewModel.End.Month - 1, ViewModel.End.Day);
            picker.Show();
        }

        private void CallBackEnd(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.End = e.Date;
        }

        private void SetTypePagerAdapter()
        {
            VacationTypesAdapter = new FragmentPagerObservableAdapter(SupportFragmentManager, FragmentFactory);
            ViewHolder.VacationTypePager.Adapter = VacationTypesAdapter;
        }

        private void SetPagerBottomDots()
        {
            var pager = ViewHolder.VacationTypePager;
            var tabLayout = ViewHolder.TabDots;
            tabLayout.SetupWithViewPager(pager);
        }

        private Fragment FragmentFactory(object arg)
        {
            if (arg is VacationTypeItemParameters vacationTypePagerParameters)
            {
                var bundle = new Bundle();
                bundle.PutViewModelParameters(vacationTypePagerParameters);

                var fragment = new VacationTypeFragment()
                {
                    Arguments = bundle
                };

                return fragment;
            }

            throw new NotSupportedException(nameof(arg));
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            bindingSet.Bind(VacationTypesAdapter)
                .For(v => v.Items)
                .To(vm => vm.VacationTypes);
                
            bindingSet.Bind(ViewHolder.VacationStartDay)
                .For(v => v.Text)
                .To(vm => vm.Start)
                .WithConvertion<DateTimeToStringConverter>("dd");

            bindingSet.Bind(ViewHolder.VacationStartMonth)
                .For(v => v.Text)
                .To(vm => vm.Start)
                .WithConvertion<DateTimeToStringConverter>("MMM");

            bindingSet.Bind(ViewHolder.VacationStartYear)
                .For(v => v.Text)
                .To(vm => vm.Start)
                .WithConvertion<DateTimeToStringConverter>("yyyy");

            bindingSet.Bind(ViewHolder.VacationEndDay)
                .For(v => v.Text)
                .To(vm => vm.End)
                .WithConvertion<DateTimeToStringConverter>("dd");

            bindingSet.Bind(ViewHolder.VacationEndMonth)
                .For(v => v.Text)
                .To(vm => vm.End)
                .WithConvertion<DateTimeToStringConverter>("MMM");

            bindingSet.Bind(ViewHolder.VacationEndYear)
                .For(v => v.Text)
                .To(vm => vm.End)
                .WithConvertion<DateTimeToStringConverter>("yyyy");

            bindingSet.Bind(ViewHolder.VacationTypePager)
                .For(v => v.SetCurrentItemAndPageSelectedBinding())
                .To(vm => vm.VacationType)
                .WithConvertion<TypeToPagerItemValueConverter>();

            bindingSet.Bind(ViewHolder.StatusRadioGroup)
                .For(v => v.CheckAndCheckedChangeBinding())
                .To(vm => vm.VacationStatus)
                .WithConvertion<RadioGroupValueConverter>();

            bindingSet.Bind(ViewHolder.RootLayout)
                .For(v => v.Visibility)
                .To(vm => !vm.Busy)
                .WithConvertion<VisibleGoneVisibilityValueConverter>();

            bindingSet.Bind(ViewHolder.CtrlActivityIndicator)
                .For(v => v.Visibility)
                .To(vm => vm.Busy)
                .WithConvertion<VisibleGoneVisibilityValueConverter>();

            bindingSet.Bind(ViewHolder.SaveButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.SaveCommand);
        }
    }
}
