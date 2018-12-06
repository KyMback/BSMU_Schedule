using System.Threading.Tasks;
using BSMU_Schedule.Interfaces;
using BSMU_Schedule.Interfaces.DataAccess.Repositories;
using BSMU_Schedule.Interfaces.Parameters;

namespace BSMU_Schedule.Services.DataAccess.Repositories
{
    public class Repository<T>: IRepository<T>
    {
        protected readonly IStorageAdapter<T> storageAdapter;

        public Repository(RepositoryConfigurations<T> configurations)
        {
            storageAdapter = StorageAdapterBuilder.BuildAdapter<T>(configurations.StorageType, configurations);
        }

        public Task<T> Get()
        {
            return storageAdapter.Get();
        }

        public Task<T> InsertOrUpdate(T value)
        {
            return storageAdapter.InsertOrUpdate(value);
        }

        public void Commit()
        {
            storageAdapter.Commit();
        }

        public void Dispose()
        {
            storageAdapter?.Dispose();
        }
    }
}
