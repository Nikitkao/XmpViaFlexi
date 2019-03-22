using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public interface IDbService
    {
        Task<List<OfflineVacation>> GetItems();

        Task<OfflineVacation> GetItem(Guid id);

        Task<int> InsertOrReplace(OfflineVacation vacation);

        Task<int> RemoveItem(OfflineVacation vacation);

        Task<OfflineVacation> GetFirstOrDefault();
    }
}
