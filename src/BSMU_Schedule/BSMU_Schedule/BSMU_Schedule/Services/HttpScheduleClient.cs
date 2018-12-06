using System;
using System.Net.Http;
using System.Threading.Tasks;
using BSMU_Schedule.Common.ActionResults;
using BSMU_Schedule.Exceptions;
using BSMU_Schedule.Interfaces;
using Plugin.Connectivity;

namespace BSMU_Schedule.Services
{
    public class HttpScheduleClient: IHttpScheduleClient
    {
        private const string schedulePage = "https://www.bsmu.by/page/3/2874/";

        public async Task<ActionResult<string>> GetHtmlSchedule()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new NetworkProblemException();
            }

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(schedulePage), Method = HttpMethod.Get
            };

            return await HandleResponse(await client.SendAsync(request));
        }

        private async Task<ActionResult<string>> HandleResponse(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                var result = new ActionResult<string>();
                result.AddError(new ActionError
                {
                    Code = message.StatusCode.ToString("G")
                });
                return result;
            }

            return new ActionResult<string>
            {
                Data = await message.Content.ReadAsStringAsync()
            };
        }
    }
}
