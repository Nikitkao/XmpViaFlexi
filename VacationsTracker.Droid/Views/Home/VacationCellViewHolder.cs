using Android.Views;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using VacationsTracker.Core.Presentation.ValueConverters;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Droid.Views.ValueConverters;

namespace VacationsTracker.Droid.Views
{
    public partial class VacationCellViewHolder : RecyclerViewBindableItemViewHolder<object, VacationCellViewModel>
    {
        public VacationCellViewHolder(View itemView) : base(itemView)
        {
        }

        public override void Bind(BindingSet<VacationCellViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(VacationType)
                .For(v => v.Text)
                .To(vm => vm.Type)
                .WithConvertion<EnumToStringConverter>();

            bindingSet.Bind(VacationStatus)
                .For(v => v.Text)
                .To(vm => vm.Status)
                .WithConvertion<EnumToStringConverter>();

            bindingSet.Bind(VacationDuration)
                .For(v => v.Text)
                .To(vm => vm.Duration)
                .WithConvertion<DurationToString>("MMM dd");
            
            bindingSet.Bind(VacationImage)
                .For(v => v.SetImageResourceBinding())
                .To(vm => vm.Type)
                .WithConvertion<ImageValueConverter>();
        }
    }
}