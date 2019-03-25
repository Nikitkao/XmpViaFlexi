using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    internal class DbService : IDbService
    {
        private readonly SQLiteAsyncConnection _db;

        private readonly Task _iniTask;

        public DbService()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MyData.db");
            _db = new SQLiteAsyncConnection(databasePath);

            _iniTask = InitializeDb();
        }

        private async Task InitializeDb()
        {
            try
            {
                await _db.CreateTableAsync<OfflineVacation>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task<List<OfflineVacation>> GetItems()
        {
            await _iniTask;

            return await _db.Table<OfflineVacation>().ToListAsync();
        }

        public async Task<OfflineVacation> GetItem(Guid id)
        {
            await _iniTask;

            return await _db.Table<OfflineVacation>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> InsertOrReplace(OfflineVacation vacation)
        {
            await _iniTask;

            return await _db.InsertOrReplaceAsync(vacation);
        }

        public async Task<OfflineVacation> GetFirstOrDefault()
        {
            await _iniTask;

            return await _db.Table<OfflineVacation>().FirstOrDefaultAsync();
        }

        public async Task<int> RemoveItem(OfflineVacation vacation)
        {
            await _iniTask;

            return await _db.DeleteAsync(vacation);
        }
    }
}
