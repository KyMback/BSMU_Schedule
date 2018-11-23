using System;

namespace BSMU_Schedule.Interfaces.DataAccess.Repositories
{
    public interface IRepository<T>: IDisposable
    {
        /// <summary>
        /// Returns value
        /// </summary>
        T Get();

        /// <summary>
        /// Saves value
        /// </summary>
        /// <param name="value">Value for saving</param>
        T InsertOrUpdate(T value);

        /// <summary>
        /// Commit all changes
        /// </summary>
        void Commit();
    }
}
