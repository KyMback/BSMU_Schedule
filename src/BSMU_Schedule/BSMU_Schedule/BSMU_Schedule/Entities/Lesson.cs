using System;

namespace BSMU_Schedule.Entities
{
    public class Lesson
    {
        public Lesson()
        {
        }

//        public Lesson(DateTime startTime, DateTime endTime, string lessonName, string address)
//        {
//            StartTime = startTime;
//            EndTime = endTime;
//            LessonsName = lessonName;
//            Address = address;
//        }

        public Lesson(string timeInterval, string lessonName, string address)
        {
            TimeInterval = timeInterval;
            LessonsName = lessonName;
            Address = address;
        }

//        public DateTime StartTime { get; set; }
//
//        public DateTime EndTime { get; set; }

        public string LessonsName { get; set; }

        public string Address { get; set; }

        //public string TimeInterval => $"{StartTime:t} - {EndTime:t}";
        public string TimeInterval { get; set; }
    }
}
