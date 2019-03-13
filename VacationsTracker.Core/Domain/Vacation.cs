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

        public Vacation(string vacationid, DateTime startdate, DateTime enddate,
            VacationStatus status, VacationType type, DateTime created, string createdBy)
        {
            Id = vacationid;
            Start = startdate;
            End = enddate;
            VacationStatus = status;
            VacationType = type;
            Created = created;
            CreatedBy = createdBy;
        }

        public void Deconstruct(out string vacationid, out DateTime startdate, out DateTime enddate,
            out VacationStatus status, out VacationType type, out DateTime created, out string createdBy)
        {
            vacationid = Id;
            startdate = Start;
            enddate = End;
            status = VacationStatus;
            type = VacationType;
            created = Created;
            createdBy = CreatedBy;
        }
    }
}
