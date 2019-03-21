using System;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;
using VacationsTracker.Core.Presentation.ViewModels;

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

        public static OfflineVacation ToOffline(this Vacation vacation)
        {
            return new OfflineVacation()
            {
                Id = Guid.Parse(vacation.Id),
                Start = vacation.Start,
                End = vacation.End,
                VacationStatus = vacation.VacationStatus,
                VacationType = vacation.VacationType,
                Created = vacation.Created,
                CreatedBy = vacation.CreatedBy,
            };
        }

        public static Vacation ToVacation(this OfflineVacation vacation)
        {
            return new Vacation
                (
                    vacation.Id.ToString(),
                    vacation.Start,
                    vacation.End,
                    vacation.VacationStatus,
                    vacation.VacationType,
                    vacation.Created,
                    vacation.CreatedBy)
                { };
        }

        public static OfflineVacation ToOffline(this VacationCellViewModel vacation)
        {
            return new OfflineVacation()
            {
                Id = Guid.Parse(vacation.Id)
            };
        }
    }
}
