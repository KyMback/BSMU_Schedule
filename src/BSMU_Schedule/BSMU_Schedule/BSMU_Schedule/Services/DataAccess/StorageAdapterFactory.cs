using System;
using BSMU_Schedule.Enums;
using BSMU_Schedule.Interfaces;
using BSMU_Schedule.Interfaces.DataAccess.StorageAdapters;
using BSMU_Schedule.Services.DataAccess.StorageAdapters;

namespace BSMU_Schedule.Services.DataAccess
{
    public static class StorageAdapterFactory
    {
        public static IStorageAdapter<T> BuildAdapter<T>(StorageType storageType, IHasConnectionConfiguration configuration)
        {
            switch (storageType)
            {
                    case StorageType.XmlFileStorageType:
                        return new XmlStorageAdapter<T>(configuration);
            }

            throw new NotImplementedException($"Adapter of type {storageType:G} not supported");
        }
    }
}
