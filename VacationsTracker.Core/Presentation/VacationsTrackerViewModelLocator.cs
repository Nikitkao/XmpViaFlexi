using FlexiMvvm;
using FlexiMvvm.Ioc;
using FlexiMvvm.Operations;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Presentation.ViewModels;
using VacationsTracker.Core.Presentation.ViewModels.Login;
using VacationsTracker.Core.Presentation.ViewModels.MainList;

namespace VacationsTracker.Core.Presentation
{
    public class VacationsTrackerViewModelLocator : ViewModelLocatorBase
    {
        private readonly IDependencyProvider _dependencyProvider;

        public VacationsTrackerViewModelLocator(IDependencyProvider dependencyProvider)
        {
            _dependencyProvider = dependencyProvider;
        }

        protected override void SetupFactory(ViewModelFactory factory)
        {
            factory.Register(() => new EntryViewModel(_dependencyProvider.Get<INavigationService>(), _dependencyProvider.Get<ISecureStorage>()));
            factory.Register(() => new LoginViewModel(
                _dependencyProvider.Get<INavigationService>(),
                _dependencyProvider.Get<IUserRepository>(),
                _dependencyProvider.Get<IOperationFactory>()));
            factory.Register(() => new MainListViewModel());
                //_dependencyProvider.Get<INavigationService>(),
                //_dependencyProvider.Get<IUserRepository>(),
                //_dependencyProvider.Get<IOperationFactory>()));
        }
    }
}
