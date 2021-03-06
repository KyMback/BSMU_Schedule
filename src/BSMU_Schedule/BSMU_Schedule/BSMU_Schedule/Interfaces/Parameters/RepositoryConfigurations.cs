﻿using BSMU_Schedule.Enums;
using BSMU_Schedule.Interfaces.DataAccess.StorageAdapters;

namespace BSMU_Schedule.Interfaces.Parameters
{
    public class RepositoryConfigurations<T>: IHasConnectionConfiguration
    {
        public string ConnectionConfiguration { get; set; }

        public RepositoryConfigurations()
        {
        }

        public RepositoryConfigurations(StorageType storageType, string connectionConfiguration)
        {
            StorageType = storageType;
            ConnectionConfiguration = connectionConfiguration;
        }

        public StorageType StorageType { get; set; }

    }
}
