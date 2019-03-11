using System;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views.V7;
using VacationsTracker.Droid.Views.MainList;
using FlexiMvvm.Weak.Subscriptions;
using VacationsTracker.Core.Presentation.ViewModels.Home;

namespace VacationsTracker.Droid.Views
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : FlxBindableAppCompatActivity<HomeViewModel>
    {
        private MainListActivityViewHolder ViewHolder { get; set; }
        private VacationsAdapter VacationsAdapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main_list);

            ViewHolder = new MainListActivityViewHolder(this);

            var menuButton = FindViewById<ImageButton>(Resource.Id.menu_button);

            var subscribtion = new WeakEventSubscription<ImageButton>(menuButton,
                (button, handler) => button.Click += handler,
                (button, handler) => button.Click -= handler, ButtonClickHandler);

            SetupRecyclerView();
        }

        private void ButtonClickHandler(object sender, EventArgs e)
        {
            if (ViewHolder.DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                ViewHolder.DrawerLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                ViewHolder.DrawerLayout.OpenDrawer(GravityCompat.Start);
            }
        }

        private void SetupRecyclerView()
        {
            VacationsAdapter = new VacationsAdapter(ViewHolder.RecyclerView)
            {
                Items = ViewModel.Vacations
            };
            ViewHolder.RecyclerView.SetAdapter(VacationsAdapter);
            ViewHolder.RecyclerView.SetLayoutManager(new LinearLayoutManager(this, 1, false));
        }

        public override void Bind(BindingSet<HomeViewModel> bindingSet)
        {
            base.Bind(bindingSet);
            bindingSet.Bind(VacationsAdapter)
                .For(v => v.ItemClickedBinding())
                .To(vm => vm.VacationSelectedCommand);

            bindingSet.Bind(ViewHolder.Refresher)
                .For(v => v.Refreshing)
                .To(vm => vm.Busy);

            bindingSet.Bind(ViewHolder.Refresher)
                .For(v => v.ValueChangedBinding())
                .To(vm => vm.RefreshCommand);
        }
    }
}
