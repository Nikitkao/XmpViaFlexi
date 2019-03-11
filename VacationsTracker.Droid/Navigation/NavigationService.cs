using Android.Content;
using FlexiMvvm;
using FlexiMvvm.Navigation;
using VacationsTracker.Droid.Views;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Home;
using VacationsTracker.Core.Presentation.ViewModels.Login;
using FlexiMvvm.Views;
using VacationsTracker.Core.Presentation.ViewModels.Details;

namespace VacationsTracker.Droid.Navigation
{
    public class NavigationService : NavigationServiceBase, INavigationService
    {
        public void NavigateToHome(LoginViewModel fromViewModel)
        {
            var loginActivity = GetActivity<LoginViewModel, LoginActivity>(fromViewModel);
            var mainListIntent = new Intent(loginActivity, typeof(HomeActivity));
            mainListIntent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.ClearTop | ActivityFlags.NewTask);
            loginActivity.NotNull().StartActivity(mainListIntent);
        }

        public void NavigateToHome(EntryViewModel fromViewModel)
        {
            var splashScreenActivity = GetActivity<EntryViewModel, SplashScreenActivity>(fromViewModel);
            var mainListIntent = new Intent(splashScreenActivity, typeof(HomeActivity));
            splashScreenActivity.NotNull().StartActivity(mainListIntent);
        }

        public void NavigateToDetails(HomeViewModel fromViewModel, string vacationId)
        {
            var homeActivity = GetActivity<HomeViewModel, HomeActivity>(fromViewModel);
            var detailsIntent = new Intent(homeActivity, typeof(DetailsActivity));
            detailsIntent.PutViewModelParameters(new VacationDetailsParameters(vacationId));
            homeActivity.NotNull().StartActivity(detailsIntent);
        }

        public void NavigateToLogin(EntryViewModel fromViewModel)
        {
            var splashScreenActivity = GetActivity<EntryViewModel, SplashScreenActivity>(fromViewModel);
            var loginIntent = new Intent(splashScreenActivity, typeof(LoginActivity));
            splashScreenActivity.NotNull().StartActivity(loginIntent);
        }
    }
}
