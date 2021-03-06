﻿using FlexiMvvm;
using FlexiMvvm.Navigation;
using UIKit;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Details;
using VacationsTracker.Core.Presentation.ViewModels.Home;
using VacationsTracker.Core.Presentation.ViewModels.Login;
using VacationsTracker.iOS.Views;
using VacationsTracker.iOS.Views.Details;
using VacationsTracker.iOS.Views.Home;
using VacationsTracker.iOS.Views.Login;
using VacationsTracker.iOS.Views.PendingOperations;

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
            var homeViewController = GetViewController<HomeViewModel, HomeViewController>(fromViewModel);
            homeViewController.NotNull().NavigationController.PushViewController(new DetailsViewController(new VacationDetailsParameters(vacationId)), true);
        }

        public void CloseDetails(DetailsViewModel fromViewModel)
        {
            var detailsViewModel = GetViewController<DetailsViewModel, DetailsViewController>(fromViewModel);
            detailsViewModel.NotNull().NavigationController.PopViewController(true);
        }

        public void NavigateToPendingOperations(HomeViewModel fromViewModel)
        {
            var homeViewModel = GetViewController<HomeViewModel, HomeViewController>(fromViewModel);
            homeViewModel.NotNull().NavigationController.PushViewController(new PendingOperationsController(), true);
        }
    }
}