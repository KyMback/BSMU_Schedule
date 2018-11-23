using BSMU_Schedule.ViewModels;
using Xamarin.Forms;

namespace BSMU_Schedule.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ScheduleViewModel(Navigation);
        }
    }
}
