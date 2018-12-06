using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BSMU_Schedule.Commands;
using BSMU_Schedule.Entities;
using BSMU_Schedule.Exceptions;
using BSMU_Schedule.Interfaces;
using BSMU_Schedule.Services;
using Xamarin.Forms;

namespace BSMU_Schedule.ViewModels
{
    public class MenuViewModel: INotifyPropertyChanged
    {
        public ICommand DownloadSchedulePreviewCommand { get; set; }

        public ICommand DownloadGroupCommand { get; set; }

        public INavigation Navigation { get; set; }

        public Page MenuPage { get; set; }

        public ScheduleViewModel ScheduleViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IScheduleRetrievingService scheduleRetrievingService;

        public MenuViewModel(INavigation navigation, ScheduleViewModel viewModel, Page menuPage)
        {
            MenuPage = menuPage;
            ScheduleViewModel = viewModel;
            Navigation = navigation;

            DownloadSchedulePreviewCommand = new DownloadSchedulePreviewCommand(this);
            DownloadGroupCommand = new DownloadGroupCommand(this);

            scheduleRetrievingService = new ScheduleRetrievingService();
            ChangeStateOfDownloading(false);
        }
        
        private bool _isThinking;
        public bool IsThinking
        {
            get => _isThinking;
            set
            {
                _isThinking = value;
                OnPropertyChanged(nameof(IsThinking));
            }
        }

        private string _groupNumber;
        public string GroupNumber
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

        private bool _isGroupFieldVisible;
        public bool IsGroupFieldVisible
        {
            get => _isGroupFieldVisible;
            set
            {
                if (_isGroupFieldVisible == value)
                {
                    return;
                }

                _isGroupFieldVisible = value;
                OnPropertyChanged(nameof(IsGroupFieldVisible));
            }
        }

        private bool _isDownloadPreviewButtonVisible;
        public bool IsDownloadPreviewButtonVisible
        {
            get => _isDownloadPreviewButtonVisible;
            set
            {
                if (_isDownloadPreviewButtonVisible == value)
                {
                    return;
                }

                _isDownloadPreviewButtonVisible = value;
                OnPropertyChanged(nameof(IsDownloadPreviewButtonVisible));
            }
        }

        private bool _isValidationErrorHappened;
        public bool IsValidationErrorHappened
        {
            get => _isValidationErrorHappened;
            set
            {
                if (_isValidationErrorHappened == value)
                {
                    return;
                }

                _isValidationErrorHappened = value;
                OnPropertyChanged(nameof(IsValidationErrorHappened));
            }
        }

        public void DownloadSchedulePreviewOpen()
        {
            ChangeStateOfDownloading(true);
        }

        private void ChangeStateOfDownloading(bool isGroupUploading)
        {
            IsDownloadPreviewButtonVisible = !isGroupUploading;
            IsGroupFieldVisible = isGroupUploading;
        }

        public async Task DownloadGroup()
        {
            if (!int.TryParse(GroupNumber, out int result))
            {
                IsValidationErrorHappened = true;
                return;
            }

            IsValidationErrorHappened = false;
            Schedule schedule;
            try
            {
                IsThinking = true;
                schedule = await scheduleRetrievingService.GetScheduleForGroup(result);
            }
            catch (NetworkProblemException)
            {
                IsThinking = false;
                await MenuPage.DisplayAlert("Error", "There are problems with network", "Ok", "Cancel");
                return;
            }
            catch (InvalidOperationException)
            {
                IsThinking = false;
                await MenuPage.DisplayAlert("Error", "Cannot find this group number", "Ok", "Cancel");
                return;
            }

            await ScheduleViewModel.UpdateSchedule(schedule);
            IsThinking = false;
            await Navigation.PopModalAsync(false);
        }
        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
