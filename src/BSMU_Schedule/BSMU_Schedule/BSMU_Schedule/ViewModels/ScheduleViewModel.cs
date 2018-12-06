using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BSMU_Schedule.Commands;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Enums;
using BSMU_Schedule.Interfaces.DataAccess.Repositories;
using BSMU_Schedule.Interfaces.Parameters;
using BSMU_Schedule.Services.DataAccess;
using BSMU_Schedule.Views;
using Xamarin.Forms;

namespace BSMU_Schedule.ViewModels
{
    public class ScheduleViewModel: INotifyPropertyChanged
    {
        private static string scheduleFile => "Schedule.xml";

        public WeekSchedule DaySchedules { get; set; }

        public DaySchedule CurrentDaySchedule { get; set; }

        public ICommand ChangeDayOfWeekCommand { get; }

        public ICommand OpenMenuPageCommand { get; }

        public INavigation Navigation { get; set; }

        public MenuPage MenuPage { get; set; }

        public Schedule  Schedule { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ScheduleViewModel(INavigation navigation)
        {
            ChangeDayOfWeekCommand = new ChangeDayOfWeekCommand(this);
            OpenMenuPageCommand = new OpenMenuPageCommand(this);
            Navigation = navigation;

            Lessons = new ObservableCollection<Lesson>();
            Schedule = RepositoriesBuilder
                .GetRepository(new RepositoryConfigurations<Schedule>(StorageType.XmlFileStorageType, scheduleFile))
                .Get().Result;
            if (Schedule != null)
            {
                DaySchedules = Schedule.WeekSchedules[4];
                GroupNumber = Schedule.GroupNumber;
            }

            ChangeCurrentDayOfWeek(DateTime.Now.DayOfWeek);
            IsMenuButtonEnabled = true;
        }

        private bool _isMenuButtonEnabled;
        public bool IsMenuButtonEnabled
        {
            get => _isMenuButtonEnabled;
            set
            {
                if (_isMenuButtonEnabled == value)
                {
                    return;
                }

                _isMenuButtonEnabled = value;
                OnPropertyChanged(nameof(IsMenuButtonEnabled));
            }
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

            if (DaySchedules?.DaySchedules == null)
            {
                return;
            }

            DaySchedule daySchedule;
            if ((daySchedule = DaySchedules.DaySchedules.FirstOrDefault(d => d.DayOfWeek == dayOfWeek)) == null)
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

        public async Task OpenMenuPage()
        {
            IsMenuButtonEnabled = false;
            if (MenuPage == null)
            {
                MenuPage = new MenuPage();
                MenuPage.BindingContext = new MenuViewModel(Navigation, this, MenuPage);
            }
            await Navigation.PushModalAsync(MenuPage, false);
            IsMenuButtonEnabled = true;
        }

        public async Task UpdateSchedule(Schedule schedule)
        {
            IRepository<Schedule> rep =
                RepositoriesBuilder.GetRepository(
                    new RepositoryConfigurations<Schedule>(StorageType.XmlFileStorageType, scheduleFile));

            await rep.InsertOrUpdate(schedule);
            rep.Commit();

            Schedule = schedule;
            DaySchedules = Schedule.WeekSchedules[4];
            ChangeCurrentDayOfWeek(DateTime.Now.DayOfWeek);
            GroupNumber = schedule.GroupNumber;
        }
    }
}
