using System;
using VacationsTracker.Core.Data;

namespace VacationsTracker.Core.Domain
{
    public class Vacation
    {
        public string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public VacationType VacationType { get; set; }

        public VacationStatus VacationStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Vacation(
            string vacationId,
            DateTime startDate,
            DateTime endDate,
            VacationStatus status,
            VacationType type,
            DateTime created,
            string createdBy)
        {
            Id = vacationId;
            Start = startDate;
            End = endDate;
            VacationStatus = status;
            VacationType = type;
            Created = created;
            CreatedBy = createdBy;
        }
    }
}
