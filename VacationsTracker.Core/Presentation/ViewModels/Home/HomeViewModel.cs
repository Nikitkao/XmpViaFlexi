using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using FlexiMvvm;
using FlexiMvvm.Collections;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Domain;
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
        private readonly IDbService _dbService;

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

        public HomeViewModel(
            INavigationService navigationService,
            IVacationRepository vacationsRepository,
            ISynchronizationService synchronizationService,
            IOperationFactory operationFactory,
            IDbService dbService)
            : base(operationFactory)
        {
            _navigationService = navigationService;
            _vacationsRepository = vacationsRepository;
            _synchronizationService = synchronizationService;
            _dbService = dbService;
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _synchronizationService.TrySynchronize();
        }

        public Task LoadVacations()
        {
            Busy = true;

            return OperationFactory
                .CreateOperation(OperationContext)
                .WithLoadingNotification()
                .WithExpressionAsync(token => _vacationsRepository.GetVacationsAsync(token))
                .OnSuccess(async vacations =>
                {
                    await UpdateOfflineVacations(vacations);
                    ShowVacations();
                })
                .OnError<InternetConnectionException>(
                    ex =>
                    {
                        ShowVacations();
                    })
                .OnError<AuthenticationException>(ex => Debug.WriteLine(ex))
                .ExecuteAsync();
        }

        private async void ShowVacations()
        {
            var vacations = await _dbService.GetItems();
            var filteredVacancies = vacations.Where(x => x.Type != OperationType.Delete).OrderBy(x => x.Id);

            Vacations.Clear();

            foreach (var vacation in filteredVacancies)
            {
                Vacations.Add(new VacationCellViewModel(vacation));
            }
        }

        private async Task UpdateOfflineVacations(IEnumerable<Vacation> serverVacations)
        {
            var offlineVacations = new List<Vacation>();
            var dbVacations = await _dbService.GetItems();

            var upToDateOfflineVacations = dbVacations.Where(x => x.Type == OperationType.UpToDate);

            foreach (var offlineVacation in upToDateOfflineVacations)
            {
                offlineVacations.Add(offlineVacation.ToVacation());
            }

            var vacationsToRemove = offlineVacations.Except(serverVacations);

            foreach (var vac in vacationsToRemove)
            {
                await _dbService.RemoveItem(vac.ToOffline());
            }

            foreach (var a in serverVacations)
            {
                var itemToUpdate = await _dbService.GetItem(Guid.Parse(a.Id));

                if (itemToUpdate == null || itemToUpdate.Type == OperationType.UpToDate)
                {
                    await _dbService.InsertOrReplace(a.ToOffline());
                }
            }
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
