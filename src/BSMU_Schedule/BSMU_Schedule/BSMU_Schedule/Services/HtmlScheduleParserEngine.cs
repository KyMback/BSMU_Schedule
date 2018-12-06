using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Interfaces;

namespace BSMU_Schedule.Services
{
    public class HtmlScheduleParserEngine: IHtmlScheduleParserEngine
    {
        /// <summary>
        /// This shit works only for 1401 - 1439 groups in schedule https://www.bsmu.by/page/3/2874/
        /// I hope the managers of this university gonna simplify schedule for devs because now
        /// THE STRUCTURE OF SCHEDULE IS VERY BAD
        /// </summary>
        public Schedule ParseHtmlPageIntoSchedule(string rawPage, int groupNumber)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            var parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(rawPage);

            DaySchedule[] days = GetDays(document, groupNumber).OrderBy(s => s.Date).ToArray();

            var weeks = new Dictionary<int, WeekSchedule>();

            foreach (DaySchedule daySchedule in days)
            {
                int numberOfWeek = GetNumberOfWeek(daySchedule.Date);

                if (!weeks.ContainsKey(numberOfWeek))
                {
                    weeks.Add(numberOfWeek, new WeekSchedule(numberOfWeek, new List<DaySchedule>()));
                }
                
                weeks[numberOfWeek].DaySchedules.Add(daySchedule);
            }

            List<WeekSchedule> test = weeks.Values.ToList();
            return new Schedule(test, groupNumber);
        }

        private int GetNumberOfWeek(DateTime dateTime)
        {
            DateTime sept1 = DateTime.Parse("09/01/2018");
            int sept1Number = sept1.DayOfYear;

            DateTime dateOfMonday = GetMonday(dateTime);

            var number = (dateOfMonday.DayOfYear - sept1Number) / 7;
            if ((dateOfMonday.DayOfYear - sept1Number) % 7 > 0)
            {
                number++;
            }

            return number;
        }

        private DateTime GetMonday(DateTime date)
        {
            while(date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        public DaySchedule[] GetDays(IHtmlDocument document, int groupNumber)
        {
            IElement el = document.QuerySelectorAll("tbody")[GetTableNumberByGroup(groupNumber)];
            IHtmlCollection<IElement> columns = el.QuerySelectorAll("tr");

            Tuple<string, string[]>[] data = columns
                .Select(s =>
                    s.QuerySelectorAll("td")?
                        .Select(k => k.TextContent)
                        .Where(text => !string.IsNullOrWhiteSpace(text))
                        .ToArray())
                .SkipWhile(s => s.Length < 15)
                .TakeWhile(s => s.Length == 15)
                .Select(arr => new Tuple<string, string[]>(arr[0], arr.Skip(1).ToArray()))
                .ToArray();
            
            Dictionary<DateTime, DaySchedule> dictionary = new Dictionary<DateTime, DaySchedule>();

            Tuple<string, string[]> group = data.Single(gr => gr.Item1 == groupNumber.ToString());

            for (int i = 0; i < group.Item2.Length; i++)
            {
                string lesson = data[0].Item2[i];
                string time = data[1].Item2[i];
                string address = data[2].Item2[i];
                
                var ls = new Lesson(time, lesson, address);

                var dates = GetDates(group.Item2[i]);
                foreach (DateTime date in dates)
                {
                    if (!dictionary.ContainsKey(date))
                    {
                        dictionary.Add(date, new DaySchedule(date, new List<Lesson> {ls}));
                        continue;
                    }
                    
                    dictionary[date].Lessons.Add(ls);
                }
            }
            
            string[][] lects = columns
                .Select(s =>
                    s.QuerySelectorAll("td")?
                        .Select(k => k.TextContent)
                        .ToArray())
                .Skip(23)
                .Where(s => s.Length == 6)
                .ToArray();

            string lastTime = string.Empty;
            foreach (var lect in lects)
            {
                lastTime = !string.IsNullOrWhiteSpace(lect[0]) ? lect[0] : lastTime;
                string lesson = lect[1];
                Tuple<string, DateTime[] > res = ParseLectureDate(lect[2]);
                
                var ls = new Lesson(lastTime, lesson, res.Item1);

                foreach (var date in res.Item2)
                {
                    if (!dictionary.ContainsKey(date))
                    {
                        dictionary.Add(date, new DaySchedule(date, new List<Lesson> {ls}));
                        continue;
                    }
                    
                    dictionary[date].Lessons.Add(ls);
                }
            }
            
            return dictionary.Values.OrderBy(s => s.Date).ToArray();
        }

        private Tuple<string, DateTime[]> ParseLectureDate(string rawData)
        {
            var splittedRawData = rawData.Split('-');
            var address = splittedRawData[1];

            DateTime[] dates = splittedRawData[0].Split(',').Select(s => s.Trim())
                .Select(s => s
                .Split('.')
                .Reverse())
                .Select(s => GetDate(s.First(), s.Last()))
                .ToArray();
            
            return new Tuple<string, DateTime[]>(address, dates);
        }

        private List<DateTime> GetDates(string dateInterval)
        {
            string[] dateIntervals = dateInterval.Split('-').Select(s => s.Trim()).ToArray();

            IEnumerable<DateTime> dates = dateIntervals
                .Select(s => s
                    .Split('.')
                    .Reverse())
                .Select(s => GetDate(s.First(), s.Last())).ToArray();

            List<DateTime> resultDates = new List<DateTime>();

            for (DateTime startDate = dates.First(); startDate.DayOfYear != dates.Last().DayOfYear; startDate = startDate.AddDays(1))
            {
                resultDates.Add(startDate);
            }
            
            return resultDates;
        }

        private DateTime GetDate(string mount, string days)
        {
            DateTime sept1 = DateTime.Parse("09/01/2018");

            DateTime result = DateTime.Parse($"{mount}/{days}/2018");

            if (sept1.DayOfYear > result.DayOfYear)
            {
                result = result.AddYears(1);
            }

            return result;
        }

        private static int GetTableNumberByGroup(int groupNumber)
        {
            if (groupNumber < 1412)
            {
                return 0;
            }
            if (groupNumber < 1426)
            {
                return 1;
            }
            if (groupNumber < 1439)
            {
                return 2;
            }

            return 3;
        }
    }
}
