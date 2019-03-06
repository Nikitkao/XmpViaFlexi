using System.Threading.Tasks;

namespace VacationsTracker.Core.DataAccess
{
    public interface ISecureStorage
    {
        Task<string> GetAsync(string key);

        Task SetAsync(string key, string value);

        bool Remove(string key);

        void RemoveAll();
    }
}
