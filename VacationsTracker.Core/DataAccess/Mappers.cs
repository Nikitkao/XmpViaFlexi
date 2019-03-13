using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    internal static class Mappers
    {
        public static Vacation ToVacation(this VacationDto vacationDto)
        {
            return new Vacation(
                vacationDto.Id,
                vacationDto.Start,
                vacationDto.End,
                vacationDto.VacationStatus,
                vacationDto.VacationType,
                vacationDto.Created,
                vacationDto.CreatedBy
            );
        }

        public static VacationDto ToVacationDto(this Vacation vacation)
        {
            return new VacationDto()
            {
                Id = vacation.Id,
                Start = vacation.Start,
                End = vacation.End,
                VacationStatus = vacation.VacationStatus,
                VacationType = vacation.VacationType,
                Created = vacation.Created,
                CreatedBy = vacation.CreatedBy,
            };
        }
    }
}
