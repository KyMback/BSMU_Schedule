using System;
using System.Collections.Generic;
using System.Text;

namespace BSMU_Schedule.Entities
{
    [Serializable]
    public class DaySchedule
    {
        public DaySchedule()
        {
        }

        public DayOfWeek DayOfWeek => Date.DayOfWeek;

        public DaySchedule(DateTime date, List<Lesson> lessons)
        {
            Date = date;
            Lessons = lessons;
        }

        public DateTime Date { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
