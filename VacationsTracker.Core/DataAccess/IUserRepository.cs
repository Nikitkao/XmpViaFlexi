using System.Threading;
using System.Threading.Tasks;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public interface IUserRepository
    {
        Task AuthorizeAsync(User user, CancellationToken token = default);
    }
}
