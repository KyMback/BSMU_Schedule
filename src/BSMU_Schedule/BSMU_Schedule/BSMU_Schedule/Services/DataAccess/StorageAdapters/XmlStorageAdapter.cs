using System;
using System.Collections.Generic;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Interfaces;
using BSMU_Schedule.Interfaces.DataAccess.StorageAdapters;

namespace BSMU_Schedule.Services.DataAccess.StorageAdapters
{
    public class XmlStorageAdapter<T>: IStorageAdapter<T> //where T: class 
    {
        public XmlStorageAdapter(IHasConnectionConfiguration configuration)
        {
        }

        public T Get()
        {
            if (typeof(T) == typeof(Schedule))
            {
                return (T)(object) new Schedule
                {
                    WeekSchedules = new[]
                    {
                        new WeekSchedule(1, new Dictionary<DayOfWeek, DaySchedule>
                        {
                            {
                                DayOfWeek.Monday, new DaySchedule(DayOfWeek.Monday, new[]
                                {
                                    new Lesson("8.00 - 9.00", "Ортопедическая стоматология", "РКСП,ул.Сухая,28"),
                                    new Lesson("9.20 - 13.25", "Ортопедическая стоматология", "РКСП,ул.Сухая,28"),
                                    new Lesson("14.25 - 16.50", "Хирургические болезни", "ул.Ленина,30")
                                })
                            },
                            {
                                DayOfWeek.Tuesday, new DaySchedule(DayOfWeek.Thursday, new[]
                                {
                                    new Lesson("9.45 - 13.50", "Терапевтическая стоматология", "РКСП,ул.Сухая,28"),
                                    new Lesson("14.10 - 15.10", "Хирургические болезни", "ул.Ленина,30"),
                                    new Lesson("15.20 - 16.20", "Терапевтическая стоматология", "РКСП,ул.Сухая,28"),
                                    new Lesson("17.20 - 18.50", "Пат. физиология", "ул.Ленина,30")
                                })
                            },
                            {
                                DayOfWeek.Wednesday, new DaySchedule(DayOfWeek.Wednesday, new[]
                                {
                                    new Lesson("8.55 - 10.25", "Пат. анатомия", "ул.Ленина,30"),
                                    new Lesson("10.55 - 12.25", "Лучевая диагностика", "ул.Ленина,30"),
                                    new Lesson("13.30 - 14.30", "Стоматология детского возраста", "ул.Ленина,30")
                                })
                            },
                            {
                                DayOfWeek.Thursday, new DaySchedule(DayOfWeek.Thursday, new[]
                                {
                                    new Lesson("8.00 - 12.05", "Челюстно-лицевая хирургия", "ул.Ленина,30"),
                                    new Lesson("12.45 - 14.15", "Физическая культура", "ул.Ленина,30"),
                                    new Lesson("15.45 - 16.45", "Лучевая диагностика", "ул.Ленина,30"),
                                    new Lesson("16.55 - 17.55", "Фармакология", "Ауд.№4,пр-тДзержинского,83")
                                })
                            },
                            {
                                DayOfWeek.Friday, new DaySchedule(DayOfWeek.Friday, new[]
                                {
                                    new Lesson("8.00 - 9.00", "Челюстно-лиц. хирургияихирург.стоматология",
                                        "ул.Ленина,30"),
                                    new Lesson("9.10 - 13.15", "Стоматология детскоговозраста", "ул.Ленина,30"),
                                    new Lesson("14.25 - 15.55", "Фармакология", "ул.Ленина,30")
                                })
                            }
                        })
                    }
                };
            }
            throw new NotImplementedException();
        }

        public T InsertOrUpdate(T value)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
