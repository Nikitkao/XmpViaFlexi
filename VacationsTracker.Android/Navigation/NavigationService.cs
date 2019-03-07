using Android.Content;
using FlexiMvvm;
using FlexiMvvm.Navigation;
using VacationsTracker.Android.Views;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Login;

namespace VacationsTracker.Android.Navigation
{
    public class NavigationService : NavigationServiceBase, INavigationService
    {
        public void NavigateToMainList(LoginViewModel fromViewModel)
        {
            var loginActivity = GetActivity<LoginViewModel, LoginActivity>(fromViewModel);
            var mainListIntent = new Intent(loginActivity, typeof(MainListActivity));
            mainListIntent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.ClearTop | ActivityFlags.NewTask);
            loginActivity.NotNull().StartActivity(mainListIntent);
        }

        public void NavigateToMainList(EntryViewModel fromViewModel)
        {
            var splashScreenActivity = GetActivity<EntryViewModel, SplashScreenActivity>(fromViewModel);
            var mainListIntent = new Intent(splashScreenActivity, typeof(MainListActivity));
            splashScreenActivity.NotNull().StartActivity(mainListIntent);
        }

        public void NavigateToLogin(EntryViewModel fromViewModel)
        {
            var splashScreenActivity = GetActivity<EntryViewModel, SplashScreenActivity>(fromViewModel);
            var loginIntent = new Intent(splashScreenActivity, typeof(LoginActivity));
            splashScreenActivity.NotNull().StartActivity(loginIntent);
        }
    }
}
