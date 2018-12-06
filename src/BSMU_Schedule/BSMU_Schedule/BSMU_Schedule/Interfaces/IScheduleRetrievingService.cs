using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSMU_Schedule.Entities;

namespace BSMU_Schedule.Interfaces
{
    public interface IScheduleRetrievingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupNumber"></param>
        /// <returns></returns>
        Task<Schedule> GetScheduleForGroup(int groupNumber);
    }
}
