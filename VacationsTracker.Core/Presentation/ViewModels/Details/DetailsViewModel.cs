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

        private string _id;
        private string _createdBy;
        private DateTime _created;
        private bool _busy;
        private DateTime _start;
        private DateTime _end;
        private VacationType _vacationType;
        private VacationStatus _vacationStatus;

        public DetailsViewModel(
            INavigationService navigationService,
            IVacationRepository vacationsRepository,
            IOperationFactory operationFactory)
            : base(operationFactory)
        {
            _navigationService = navigationService;
            _vacationsRepository = vacationsRepository;
        }

        public ICommand SaveCommand => CommandProvider.GetForAsync(OnSave, () => !Busy);

        public bool Busy
        {
            get => _busy;
            set => Set(ref _busy, value);
        }

        public DateTime Start
        {
            get => _start;
            set => Set(ref _start, value);
        }

        public DateTime End
        {
            get => _end;
            set => Set(ref _end, value);
        }

        public VacationType VacationType
        {
            get => _vacationType;
            set => Set(ref _vacationType, value);
        }
        
        public VacationStatus VacationStatus
        {
            get => _vacationStatus;
            set => Set(ref _vacationStatus, value);
        }

        public RangeObservableCollection<VacationTypeItemParameters> VacationTypes { get; }
            = new RangeObservableCollection<VacationTypeItemParameters>(
                Enum.GetValues(typeof(VacationType))
                    .Cast<VacationType>().Select(t => new VacationTypeItemParameters(t)));

        private async Task OnSave()
        {
            if (Start > End)
                return;
            await OperationFactory
                .CreateOperation(OperationContext)
                .WithInternetConnectionCondition()
                .WithLoadingNotification()
                .WithExpressionAsync(token =>
                {
                    var vac = new Vacation(
                        _id,
                        Start,
                        End,
                        VacationStatus,
                        VacationType,
                        _created,
                        _createdBy);
                    return _vacationsRepository.AddOrUpdateAsync(vac, token);
                })
                .OnSuccess(_ => {})
                .OnError<InternetConnectionException>(_ => Debug.WriteLine("Connection Exception"))
                .OnError<Exception>(error => Debug.WriteLine(error.Exception))
                .ExecuteAsync();
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
                    _id = vacation.Id;
                    Start = vacation.Start;
                    End = vacation.End;
                    VacationStatus = vacation.VacationStatus;
                    VacationType = vacation.VacationType;
                    _created = vacation.Created;
                    _createdBy = vacation.CreatedBy;
                })
                .OnError<InternetConnectionException>(_ => { })
                .OnError<Exception>(error => Debug.WriteLine(error.Exception))
                .ExecuteAsync();
        }
    }
}
