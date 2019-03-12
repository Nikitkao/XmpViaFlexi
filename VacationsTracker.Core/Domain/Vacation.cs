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

        public void Deconstruct(out string _vacationid, out DateTime startdate, out DateTime enddate, out VacationStatus status, out VacationType type)
        {
            _vacationid = Id;
            startdate = Start;
            enddate = End;
            status = VacationStatus;
            type = VacationType;
        }
    }
}
