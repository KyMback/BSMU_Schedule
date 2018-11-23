using System;
using System.Windows.Input;
using BSMU_Schedule.ViewModels;

namespace BSMU_Schedule.Commands
{
    public class ChangeDayOfWeekCommand: ICommand
    {
        public ScheduleViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public ChangeDayOfWeekCommand(ScheduleViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.ChangeCurrentDayOfWeek((DayOfWeek)parameter);
        }
    }
}
