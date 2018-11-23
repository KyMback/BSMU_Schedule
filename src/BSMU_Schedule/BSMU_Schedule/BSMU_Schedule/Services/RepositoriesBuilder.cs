using System;
using System.Collections.Generic;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Interfaces.DataAccess.Repositories;
using BSMU_Schedule.Interfaces.Parameters;
using BSMU_Schedule.Services.DataAccess.Repositories;

namespace BSMU_Schedule.Services
{
    public static class RepositoriesBuilder
    {
        private static readonly IDictionary<Type, Func<object, object>> RepositoriesBuilders =
            new Dictionary<Type, Func<object, object>>
            {
                {typeof(Schedule), cfg => new Repository<Schedule>(cfg as RepositoryConfigurations<Schedule>)}
            };

        public static IRepository<T> GetRepository<T>(RepositoryConfigurations<T> configurations)
        {
            return RepositoriesBuilders[typeof(T)](configurations) as IRepository<T>;
        }
    }
}
