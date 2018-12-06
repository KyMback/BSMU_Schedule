using System;

namespace BSMU_Schedule.Entities
{
    [Serializable]
    public class Lesson
    {
        public Lesson()
        {
        }

        public Lesson(string timeInterval, string lessonName, string address)
        {
            TimeInterval = timeInterval;
            LessonsName = lessonName;
            Address = address;
        }

        public string LessonsName { get; set; }

        public string Address { get; set; }
        
        public string TimeInterval { get; set; }
    }
}
