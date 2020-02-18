using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TunerLib
{
    public class TunerDatabase
    {
        // Lazy initializer, delays db initialization to when the db is needed, preventing it from delaying app launch
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        private static SQLiteAsyncConnection Database => lazyInitializer.Value;
        private static bool initialized = false;

        public TunerDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Tuner).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Tuner)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Tuner>> GetTunersAsync()
        {
            return Database.Table<Tuner>().ToListAsync();
        }

        public Task<Tuner> GetTunerAsync(string name)
        {
            return Database.Table<Tuner>().Where(t => t.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> SaveTunerAsync(Tuner tuner)
        {
            //Tuner prevTuner = Database.Table<Tuner>().Where(t => t.Name == tuner.Name).FirstAsync().Result;
            //if ( prevTuner != null)
            //{
            //    return Database.UpdateAsync(tuner);
            //}
            //else
            //{
            //    return Database.InsertAsync(tuner);
            //}
            return Database.InsertAsync(tuner);
        }

        public Task<int> DeleteItemAsync(Tuner tuner)
        {
            return Database.DeleteAsync(tuner);
        }
    }
}
