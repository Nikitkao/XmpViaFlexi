using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Login;

namespace VacationsTracker.Core.Navigation
{
    public interface INavigationService
    {
        void NavigateToLogin(EntryViewModel fromViewModel);

        void NavigateToHome(LoginViewModel fromViewModel);
    }
}
