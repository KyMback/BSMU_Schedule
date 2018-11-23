using System;

namespace BSMU_Schedule.Interfaces
{
    public interface IStorageAdapter<T>: IDisposable
    {
        /// <summary>
        /// Gets value
        /// </summary>
        T Get();

        /// <summary>
        /// Saves value
        /// </summary>
        /// <param name="value"></param>
        T InsertOrUpdate(T value);

        /// <summary>
        /// Commits changes
        /// </summary>
        void Commit();
    }
}
