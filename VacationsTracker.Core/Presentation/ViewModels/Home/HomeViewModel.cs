using System.Diagnostics;
using System.Security.Authentication;
using System.Threading.Tasks;
using FlexiMvvm;
using FlexiMvvm.Collections;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Exceptions;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Operations;

namespace VacationsTracker.Core.Presentation.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase, IViewModelWithOperation
    {
        private readonly INavigationService _navigationService;
        private readonly IVacationRepository _vacationsRepository;
        private readonly ISynchronizationService _synchronizationService;

        private bool _busy;

        public RangeObservableCollection<VacationCellViewModel> Vacations { get; } =
            new RangeObservableCollection<VacationCellViewModel>();

        public bool Busy
        {
            get => _busy;
            set => Set(ref _busy, value);
        }

        public ICommand RefreshCommand => CommandProvider.GetForAsync(LoadVacations);

        public ICommand<VacationCellViewModel> VacationSelectedCommand => CommandProvider.Get<VacationCellViewModel>(VacationSelected);

        public ICommand AddCommand => CommandProvider.Get(Add);

        public ICommand OpenOperationsCommand => CommandProvider.Get(OpenOperations);

        public HomeViewModel(INavigationService navigationService, IVacationRepository vacationsRepository, ISynchronizationService synchronizationService,
            IOperationFactory operationFactory) : base(operationFactory)
        {
            _navigationService = navigationService;
            _vacationsRepository = vacationsRepository;
            _synchronizationService = synchronizationService;
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await _synchronizationService.TrySynchronize();
        }

        public Task LoadVacations()
        {
            Busy = true;

            return OperationFactory
                .CreateOperation(OperationContext)
                .WithInternetConnectionCondition()
                .WithLoadingNotification()
                .WithExpressionAsync(token => _vacationsRepository.GetVacationsAsync(token))
                .OnSuccess(vacations =>
                {
                    Vacations.Clear();

                    foreach (var vacation in vacations)
                    {
                        Vacations.Add(new VacationCellViewModel(vacation));
                    }
                })
                .OnError<InternetConnectionException>(_ => { })
                .OnError<AuthenticationException>(ex => Debug.WriteLine(ex))
                .ExecuteAsync();
        }

        private void VacationSelected(VacationCellViewModel cellViewModel)
        {
            _navigationService.NavigateToDetails(this, cellViewModel.Id);
        }

        private void OpenOperations()
        {
            _navigationService.NavigateToPendingOperations(this);
        }

        private void Add()
        {
            _navigationService.NavigateToDetails(this, null);
        }
    }
}
