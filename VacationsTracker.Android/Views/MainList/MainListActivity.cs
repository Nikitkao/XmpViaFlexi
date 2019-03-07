using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Views.V7;
using VacationsTracker.Core.Presentation.ViewModels.MainList;

namespace VacationsTracker.Android.Views
{
    [Activity(Label = "MainListActivity")]
    public class MainListActivity : FlxBindableAppCompatActivity<MainListViewModel>
    {
        private MainListActivityViewHolder ViewHolder { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main_list);

            ViewHolder = new MainListActivityViewHolder(this);

            var menuButton = FindViewById<ImageButton>(Resource.Id.menu_button);

            menuButton.Click += (sender, args) =>
            {
                if (ViewHolder.DrawerLayout1.IsDrawerOpen(GravityCompat.Start))
                {
                    ViewHolder.DrawerLayout1.CloseDrawer(GravityCompat.Start);
                }
                else
                {
                    ViewHolder.DrawerLayout1.OpenDrawer(GravityCompat.Start);
                }
                
            };
        }
    }
}