using Android.OS;
using Android.Views;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views.V4;
using VacationsTracker.Core.Presentation.ValueConverters;
using VacationsTracker.Core.Presentation.ViewModels.Details;
using VacationsTracker.Droid.Views.ValueConverters;

namespace VacationsTracker.Droid.Views.Details
{
    public class VacationTypeFragment : FlxBindableFragment<VacationTypeItemViewModel, VacationTypeItemParameters>
    {
        private VacationTypeFragmentViewHolder ViewHolder { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_vacation_type, container, false);

            ViewHolder = new VacationTypeFragmentViewHolder(view);
            return view;
        }

        public override void Bind(BindingSet<VacationTypeItemViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(ViewHolder.TextViewVacationName)
                .For(v => v.Text)
                .To(vm => vm.VacationType)
                .WithConvertion<EnumToStringConverter>();

            bindingSet.Bind(ViewHolder.ImageVacationType)
                .For(v => v.SetImageResourceBinding())
                .To(vm => vm.VacationType)
                .WithConvertion<ImageValueConverter>();
        }
    }
}
