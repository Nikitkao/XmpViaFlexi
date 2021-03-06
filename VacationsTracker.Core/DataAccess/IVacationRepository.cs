﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public interface  IVacationRepository
    {
        Task<IEnumerable<Vacation>> GetVacationsAsync(CancellationToken token = default);

        Task<Vacation> GetVacationAsync(string id, CancellationToken token = default);

        Task AddOrUpdateAsync(Vacation vacationViewModel, CancellationToken token = default);
        
        Task DeleteVacationAsync(string id, CancellationToken token = default);
    }
}
