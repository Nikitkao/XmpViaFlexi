using System;
using FlexiMvvm;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.Presentation.ViewModels
{
    public class Duration
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }

    public class VacationCellViewModel : ObservableObjectBase
    {
        private VacationType _type;
        private VacationStatus _status;
        private Duration _duration;

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

        public Duration Duration
        {
            get => _duration;
            set => Set(ref _duration, value);
        }

        public string Id { get; set; }

        public VacationCellViewModel(Vacation vac)
        {
            Id = vac.Id;
            Duration = new Duration() {End = vac.End, Start = vac.Start};
            Status = vac.VacationStatus;
            Type = vac.VacationType;
        }

        public VacationCellViewModel(OfflineVacation vac)
        {
            Id = vac.Id.ToString();
            Duration = new Duration() { End = vac.End, Start = vac.Start };
            Status = vac.VacationStatus;
            Type = vac.VacationType;
        }
    }
}
