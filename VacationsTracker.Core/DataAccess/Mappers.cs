using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    internal static class Mappers
    {
        public static Vacation ToVacation(this VacationDto vacationDto)
        {
            return new Vacation()
            {
                Id = vacationDto.Id,
                Start = vacationDto.Start,
                End = vacationDto.End,
                VacationStatus = vacationDto.VacationStatus,
                VacationType = vacationDto.VacationType,
                Created = vacationDto.Created,
                CreatedBy = vacationDto.CreatedBy,
            };
        }
    }
}
