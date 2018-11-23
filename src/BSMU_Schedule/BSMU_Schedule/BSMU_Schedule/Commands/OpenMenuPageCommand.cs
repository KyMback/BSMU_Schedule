using System;
using System.Windows.Input;
using BSMU_Schedule.ViewModels;

namespace BSMU_Schedule.Commands
{
    public class OpenMenuPageCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ScheduleViewModel ViewModel { get; set; }

        public OpenMenuPageCommand(ScheduleViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.OpenMenuPage();
        }
    }
}
