using System.Threading.Tasks;
using BSMU_Schedule.Common.ActionResults;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Interfaces;

namespace BSMU_Schedule.Services
{
    public class ScheduleRetrievingService: IScheduleRetrievingService
    {
        private readonly IHttpScheduleClient httpClient;
        private readonly IHtmlScheduleParserEngine htmlScheduleParserEngine;

        public ScheduleRetrievingService()
        {
            httpClient = new HttpScheduleClient();
            htmlScheduleParserEngine = new HtmlScheduleParserEngine();

        }

        public async Task<Schedule> GetScheduleForGroup(int groupNumber)
        {
            ActionResult<string> result = await httpClient.GetHtmlSchedule();

            if (!result.Succeeded)
            {
                return null;
            }

            return htmlScheduleParserEngine.ParseHtmlPageIntoSchedule(result.Data, groupNumber);
        }
    }
}
