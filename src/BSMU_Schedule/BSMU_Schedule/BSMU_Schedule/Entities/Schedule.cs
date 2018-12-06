using System;
using System.Collections.Generic;

namespace BSMU_Schedule.Entities
{
    [Serializable]
    public class Schedule
    {
        public Schedule()
        {
        }
        
        public Schedule(List<WeekSchedule> weeks, int groupNumber)
        {
            WeekSchedules = weeks;
            GroupNumber = groupNumber;
        }

        public List<WeekSchedule> WeekSchedules { get; set; }

        public int GroupNumber { get; set; }
    }
}
