using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlexiMvvm;
using FlexiMvvm.Collections;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Domain;
using VacationsTracker.Core.Exceptions;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Operations;

namespace VacationsTracker.Core.Presentation.ViewModels.Details
{
    public class DetailsViewModel : ViewModelBase<VacationDetailsParameters>, IViewModelWithOperation
    {
        private readonly IVacationRepository _vacationsRepository;
        private readonly INavigationService _navigationService;

        private string _vacationId;
        private string _createdBy;
        private DateTime _created;
        private bool _busy;
        private DateTime _startDate;
        private DateTime _endDate;
        private VacationType _type;
        private VacationStatus _status;

        public ICommand SaveCommand => CommandProvider.GetForAsync(OnSave, () => !Busy);

        private async Task OnSave()
        {
            if(StartDate > EndDate)
                return;
            await OperationFactory
                .CreateOperation(OperationContext)
                .WithInternetConnectionCondition()
                .WithLoadingNotification()
                .WithExpressionAsync(token =>
                {
                    var vac = new Vacation(_vacationId, StartDate, EndDate, Status, Type, _created, _createdBy);
                    return _vacationsRepository.AddOrUpdateAsync(vac, token);
                })
                .OnSuccess(_ =>
                {
                   
                })
                .OnError<InternetConnectionException>(_ => Debug.WriteLine("Connection Exception"))
                .OnError<Exception>(error => Debug.WriteLine(error.Exception))
                .ExecuteAsync();
        }

        public bool Busy
        {
            get => _busy;
            set => Set(ref _busy, value);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => Set(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => Set(ref _endDate, value);
        }

        public VacationType Type
        {
            get => _type;
            set => Set(ref _type, value);
        }
        
        public VacationStatus Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        public RangeObservableCollection<VacationTypeItemParameters> VacationTypes { get; }
            = new RangeObservableCollection<VacationTypeItemParameters>(
                Enum.GetValues(typeof(VacationType))
                    .Cast<VacationType>().Select(t => new VacationTypeItemParameters(t)));

        public DetailsViewModel(
            INavigationService navigationService,
            IVacationRepository vacationsRepository,
            IOperationFactory operationFactory)
            : base(operationFactory)
        {
            _navigationService = navigationService;
            _vacationsRepository = vacationsRepository;
        }

        protected override async Task InitializeAsync(VacationDetailsParameters parameters)
        {
            await base.InitializeAsync(parameters);

            await OperationFactory
                .CreateOperation(OperationContext)
                .WithInternetConnectionCondition()
                .WithLoadingNotification()
                .WithExpressionAsync(token =>
                {
                    var id = parameters.NotNull().VacationId;
                    return _vacationsRepository.GetVacationAsync(id, token);
                })
                .OnSuccess(vacation =>
                {
                    (_vacationId, StartDate, EndDate, Status, Type, _created, _createdBy) = vacation;
                })
                .OnError<InternetConnectionException>(_ => { })
                .OnError<Exception>(error => Debug.WriteLine(error.Exception))
                .ExecuteAsync();
        }
    }
}
