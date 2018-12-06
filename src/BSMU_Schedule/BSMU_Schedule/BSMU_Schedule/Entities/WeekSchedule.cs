using System;
using System.Collections.Generic;

namespace BSMU_Schedule.Entities
{
    [Serializable]
    public class WeekSchedule
    {
        public WeekSchedule()
        {
        }

        public WeekSchedule(int weekNumber, List<DaySchedule> daySchedules)
        {
            DaySchedules = daySchedules;
            WeekNumber = weekNumber;
        }

        public List<DaySchedule> DaySchedules { get; set; }

        public int WeekNumber { get; set; }
    }
}
