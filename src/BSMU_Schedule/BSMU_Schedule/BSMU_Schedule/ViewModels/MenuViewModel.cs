using System.ComponentModel;
using System.Windows.Input;
using BSMU_Schedule.Commands;

namespace BSMU_Schedule.ViewModels
{
    public class MenuViewModel: INotifyPropertyChanged
    {
        public ICommand DownloadSchedulePreviewCommand { get; set; }

        public ICommand DownloadGroupCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MenuViewModel()
        {
            DownloadSchedulePreviewCommand = new DownloadSchedulePreviewCommand(this);
            DownloadGroupCommand = new DownloadGroupCommand(this);

            ChangeStateOfDownloading(false);
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

        public void DownloadSchedulePreviewOpen()
        {
            ChangeStateOfDownloading(true);
        }

        private void ChangeStateOfDownloading(bool isGroupUploading)
        {
            IsDownloadPreviewButtonVisible = !isGroupUploading;
            IsGroupFieldVisible = isGroupUploading;
        }

        public void DownloadGroup()
        {

        }
        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
