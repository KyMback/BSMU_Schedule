﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BSMU_Schedule.ViewModels;

namespace BSMU_Schedule.Commands
{
    public class DownloadGroupCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;

        public MenuViewModel ViewModel { get; set; }

        public DownloadGroupCommand(MenuViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.DownloadGroup();
        }
    }
}
