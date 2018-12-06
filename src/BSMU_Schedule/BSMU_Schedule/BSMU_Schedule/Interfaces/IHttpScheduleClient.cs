using System.Threading.Tasks;
using BSMU_Schedule.Common.ActionResults;

namespace BSMU_Schedule.Interfaces
{
    public interface IHttpScheduleClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ActionResult<string>> GetHtmlSchedule();
    }
}
