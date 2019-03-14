using FlexiMvvm;
using FlexiMvvm.Navigation;
using UIKit;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Home;
using VacationsTracker.Core.Presentation.ViewModels.Login;
using VacationsTracker.iOS.Views;
using VacationsTracker.iOS.Views.Details;
using VacationsTracker.iOS.Views.Home;
using VacationsTracker.iOS.Views.Login;

namespace VacationsTracker.iOS.Navigation
{
    public class NavigationService : NavigationServiceBase, INavigationService
    {
        public void NavigateToLogin(EntryViewModel fromViewModel)
        {
            var rootViewController = GetViewController<EntryViewModel, RootNavigationController>(fromViewModel);
            rootViewController.NotNull().PushViewController(new LoginViewController(), false);
        }

        public void NavigateToHome(LoginViewModel fromViewModel)
        {
            var loginViewController = GetViewController<LoginViewModel, LoginViewController>(fromViewModel);
            loginViewController.NotNull().NavigationController
                .SetViewControllers(new UIViewController[] {new HomeViewController()}, true);
        }

        public void NavigateToHome(EntryViewModel fromViewModel)
        {
            var rootViewController = GetViewController<EntryViewModel, RootNavigationController>(fromViewModel);
            rootViewController.NotNull().PushViewController(new HomeViewController(), false);
        }

        public void NavigateToDetails(HomeViewModel fromViewModel, string vacationId)
        {
            //var homeViewController = GetViewController<HomeViewModel, HomeViewController>(fromViewModel);
            //homeViewController.NotNull().NavigationController.PushViewController(new DetailsViewController(parameters), true);
        }
    }
}