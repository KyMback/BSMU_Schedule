using System;
using System.Collections.Generic;
using System.Text;

namespace BSMU_Schedule.Entities
{
    public class Schedule
    {
        public IEnumerable<WeekSchedule> WeekSchedules { get; set; }
    }
}
