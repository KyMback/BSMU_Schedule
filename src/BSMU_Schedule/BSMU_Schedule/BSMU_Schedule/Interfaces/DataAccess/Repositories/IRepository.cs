using System;
using System.Threading.Tasks;

namespace BSMU_Schedule.Interfaces.DataAccess.Repositories
{
    public interface IRepository<T>: IDisposable
    {
        /// <summary>
        /// Returns value
        /// </summary>
        Task<T> Get();

        /// <summary>
        /// Saves value
        /// </summary>
        /// <param name="value">Value for saving</param>
        Task<T> InsertOrUpdate(T value);

        /// <summary>
        /// Commit all changes
        /// </summary>
        void Commit();
    }
}
