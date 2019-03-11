using System;
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

        private DateTime _refreshedDateTime;

        private bool _busy;

        public RangeObservableCollection<VacationCellViewModel> Vacations { get; } =
            new RangeObservableCollection<VacationCellViewModel>();

        public DateTime RefreshedDateTime
        {
            get => _refreshedDateTime;
            set => Set(ref _refreshedDateTime, value);
        }

        public bool Busy
        {
            get => _busy;
            set => Set(ref _busy, value);
        }

        public ICommand RefreshCommand => CommandProvider.GetForAsync(LoadVacations);

        public ICommand<VacationCellViewModel> VacationSelectedCommand => CommandProvider.Get<VacationCellViewModel>(VacationSelected);

        public HomeViewModel(INavigationService navigationService, IVacationRepository vacationsRepository,
            IOperationFactory operationFactory) : base(operationFactory)
        {
            _navigationService = navigationService;
            _vacationsRepository = vacationsRepository;
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await LoadVacations();
        }

        private Task LoadVacations()
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

                    RefreshedDateTime = DateTime.Now;
                    Busy = false;
                })
                .OnError<InternetConnectionException>(_ => { })
                .OnError<AuthenticationException>(ex => Debug.WriteLine(ex))
                .ExecuteAsync();
        }

        private void VacationSelected(VacationCellViewModel cellViewModel)
        {
            _navigationService.NavigateToDetails(this, cellViewModel.Id);
        }
    }
}
