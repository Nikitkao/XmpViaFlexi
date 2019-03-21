using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FlexiMvvm;
using FlexiMvvm.Collections;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Operations;

namespace VacationsTracker.Core.Presentation.ViewModels.PendingOperations
{
    public class PendingOperationsViewModel : ViewModelBase, IViewModelWithOperation
    {
        private readonly IDbService _dbService;

        public RangeObservableCollection<VacationCellViewModel> Vacations { get; } =
            new RangeObservableCollection<VacationCellViewModel>();

        public PendingOperationsViewModel(IDbService dbService,
            IOperationFactory operationFactory) : base(operationFactory)
        {
            _dbService = dbService;
        }

        private bool _busy;

        public bool Busy
        {
            get => _busy;
            set => Set(ref _busy, value);
        }

        public ICommand RefreshCommand => CommandProvider.GetForAsync(LoadVacations);

        public ICommand<VacationCellViewModel> VacationSelectedCommand => CommandProvider.Get<VacationCellViewModel>(VacationSelected);

        public Task LoadVacations()
        {
            return OperationFactory
                .CreateOperation(OperationContext)
                .WithLoadingNotification()
                .WithExpressionAsync(token => _dbService.GetItems())
                .OnSuccess(vacations =>
                {
                    Vacations.Clear();

                    foreach (var vacation in vacations)
                    {
                        Vacations.Add(new VacationCellViewModel(vacation.ToVacation()));
                    }
                })
                .OnError<Exception>(ex => Debug.WriteLine(ex))
                .ExecuteAsync();
        }

        private async void VacationSelected(VacationCellViewModel cellViewModel)
        {
            var response = await _dbService.RemoveItem(cellViewModel.ToOffline());
            await LoadVacations();
        }
    }
}
