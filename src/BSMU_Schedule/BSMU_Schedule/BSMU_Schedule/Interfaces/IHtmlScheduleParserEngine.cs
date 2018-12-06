using System;
using System.Collections.Generic;
using System.Text;
using BSMU_Schedule.Entities;

namespace BSMU_Schedule.Interfaces
{
    public interface IHtmlScheduleParserEngine
    {
        Schedule ParseHtmlPageIntoSchedule(string rawPage, int groupNumber);
    }
}
