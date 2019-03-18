using FlexiMvvm;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Navigation;

namespace VacationsTracker.Core.Presentation.ViewModels
{
    public class EntryViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISecureStorage _secureStorage;

        public EntryViewModel(INavigationService navigationService, ISecureStorage secureStorage)
        {
            _navigationService = navigationService;
            _secureStorage = secureStorage;
        }

        protected  override async void Initialize()
        {
            base.Initialize();

            var token = await _secureStorage.GetAsync(Constants.TokenStorageKey);

            if (string.IsNullOrEmpty(token))
            {
                _navigationService.NavigateToLogin(this);
            }
            else
            {
                _navigationService.NavigateToHome(this);
            }
        }
    }
}
