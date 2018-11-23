using System;
using System.Collections.Generic;

namespace BSMU_Schedule.Entities
{
    public class WeekSchedule
    {
        public WeekSchedule()
        {
        }

        public WeekSchedule(int weekNumber, IDictionary<DayOfWeek, DaySchedule> daySchedules)
        {
            DaySchedules = daySchedules;
            WeekNumber = weekNumber;
        }

        public IDictionary<DayOfWeek, DaySchedule> DaySchedules { get; set; }

        public int WeekNumber { get; set; }
    }
}
