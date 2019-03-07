using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Login;

namespace VacationsTracker.Core.Navigation
{
    public interface INavigationService
    {
        void NavigateToLogin(EntryViewModel fromViewModel);

        void NavigateToMainList(LoginViewModel fromViewModel);

        void NavigateToMainList(EntryViewModel fromViewModel);
    }
}
