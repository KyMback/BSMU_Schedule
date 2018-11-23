using BSMU_Schedule.Enums;
using BSMU_Schedule.Interfaces.DataAccess.StorageAdapters;

namespace BSMU_Schedule.Interfaces.Parameters
{
    public class RepositoryConfigurations<T>: IHasConnectionConfiguration
    {
        public StorageType StorageType { get; set; }

        public string ConnectionConfiguration { get; set; }
    }
}
