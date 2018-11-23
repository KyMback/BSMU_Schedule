using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BSMU_Schedule.Commands;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Enums;
using BSMU_Schedule.Interfaces.Parameters;
using BSMU_Schedule.Services.DataAccess;

namespace BSMU_Schedule.ViewModels
{
    public class ScheduleViewModel: INotifyPropertyChanged
    {
        public WeekSchedule DaySchedules { get; set; }

        public DaySchedule CurrentDaySchedule { get; set; }

        public ICommand ChangeDayOfWeekCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ScheduleViewModel()
        {
            ChangeDayOfWeekCommand = new ChangeDayOfWeekCommand(this);
            Lessons = new ObservableCollection<Lesson>();
            GroupNumber = 7301;
            DaySchedules = RepositoriesBuilder
                .GetRepository(new RepositoryConfigurations<Schedule>(StorageType.XmlFileStorageType))
                .Get()
                .WeekSchedules
                .First();

            ChangeCurrentDayOfWeek(DateTime.Now.DayOfWeek);
        }

        private string _dayOfWeekRepresentation;
        public string DayOfWeekRepresentation
        {
            get => _dayOfWeekRepresentation;
            set
            {
                if (_dayOfWeekRepresentation == value)
                {
                    return;
                }

                _dayOfWeekRepresentation = value;
                OnPropertyChanged(nameof(DayOfWeekRepresentation));
            }
        }

        private int _groupNumber;
        public int GroupNumber
        {
            get => _groupNumber;
            set
            {
                if (_groupNumber == value)
                {
                    return;
                }

                _groupNumber = value;
                OnPropertyChanged(nameof(GroupNumber));
            }
        }

        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons
        {
            get => _lessons;
            set
            {
                if (_lessons == value)
                {
                    return;
                }

                _lessons = value;
                OnPropertyChanged(nameof(Lessons));
            }
        }
        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChangeCurrentDayOfWeek(DayOfWeek dayOfWeek)
        {
            DayOfWeekRepresentation = dayOfWeek.ToString("G");

            if (!DaySchedules.DaySchedules.TryGetValue(dayOfWeek, out DaySchedule daySchedule))
            {
                Lessons.Clear();
                return;
            }

            ChangeDaySchedule(daySchedule);
        }

        public void ChangeDaySchedule(DaySchedule daySchedule)
        {
            Lessons.Clear();
            CurrentDaySchedule = daySchedule;
            foreach (var day in CurrentDaySchedule.Lessons)
            {
                Lessons.Add(day);
            }
        }
    }
}
