using System;
using System.Collections.Generic;
using System.Text;

namespace BSMU_Schedule.Entities
{
    public class DaySchedule
    {
        public DaySchedule(DayOfWeek dayOfWeek, IEnumerable<Lesson> lessons)
        {
            DayOfWeek = dayOfWeek;
            Lessons = lessons;
        }

        public DayOfWeek DayOfWeek { get; set; }

        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
