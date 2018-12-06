using System;
using System.Threading.Tasks;

namespace BSMU_Schedule.Interfaces
{
    public interface IStorageAdapter<T>: IDisposable
    {
        /// <summary>
        /// Gets value
        /// </summary>
        Task<T> Get();

        /// <summary>
        /// Saves value
        /// </summary>
        /// <param name="value"></param>
        Task<T> InsertOrUpdate(T value);

        /// <summary>
        /// Commits changes
        /// </summary>
        void Commit();
    }
}
