using System;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views.V7;
using VacationsTracker.Core.Presentation.ViewModels.Home;

namespace VacationsTracker.Droid.Views.Home
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : FlxBindableAppCompatActivity<HomeViewModel>
    {
        private MainListActivityViewHolder ViewHolder { get; set; }

        private VacationsAdapter VacationsAdapter { get; set; }

        private ImageButton _menuButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main_list);

            ViewHolder = new MainListActivityViewHolder(this);

            _menuButton = FindViewById<ImageButton>(Resource.Id.menu_button);

            //ImageButton menuButton = FindViewById<ImageButton>(Resource.Id.menu_button);
            //menuButton.ClickWeakSubscribe(ButtonClickHandler);

            SetupRecyclerView();
        }

        protected override async void OnResume()
        {
            base.OnResume();

            await ViewModel.LoadVacations();
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

            bindingSet.Bind(ViewHolder.Fab)
                .For(v => v.ClickBinding())
                .To(vm => vm.AddCommand);

            bindingSet.Bind(_menuButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.OpenOperationsCommand);
        }
    }
}
