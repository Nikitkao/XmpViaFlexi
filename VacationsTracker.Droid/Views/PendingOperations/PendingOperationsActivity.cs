using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views.V7;
using VacationsTracker.Core.Presentation.ViewModels.PendingOperations;
using VacationsTracker.Droid.Views.Home;

namespace VacationsTracker.Droid.Views.PendingOperations
{
    [Activity(Label = "PendingOperationsActivity")]
    public class PendingOperationsActivity : FlxBindableAppCompatActivity<PendingOperationsViewModel>
    {
        private PendingOperationsActivityViewHolder ViewHolder { get; set; }
        private VacationsAdapter VacationsAdapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_pending_operations);

            ViewHolder = new PendingOperationsActivityViewHolder(this);
            
            SetupRecyclerView();
        }
        
        private void SetupRecyclerView()
        {
            VacationsAdapter = new VacationsAdapter(ViewHolder.RecyclerView2)
            {
                Items = ViewModel.Vacations
            };
            ViewHolder.RecyclerView2.SetAdapter(VacationsAdapter);
            ViewHolder.RecyclerView2.SetLayoutManager(new LinearLayoutManager(this, 1, false));
        }

        public override void Bind(BindingSet<PendingOperationsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(VacationsAdapter)
                .For(v => v.ItemClickedBinding())
                .To(vm => vm.VacationSelectedCommand);

            bindingSet.Bind(ViewHolder.Refresher2)
                .For(v => v.Refreshing)
                .To(vm => vm.Busy);

            bindingSet.Bind(ViewHolder.Refresher2)
                .For(v => v.ValueChangedBinding())
                .To(vm => vm.RefreshCommand);
        }
    }
}