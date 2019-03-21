using System.Threading.Tasks;

namespace VacationsTracker.Core.DataAccess
{
    public interface ISynchronizationService
    {
        Task TrySynchronize();
    }
}
