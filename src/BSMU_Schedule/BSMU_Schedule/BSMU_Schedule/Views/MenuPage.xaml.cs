using BSMU_Schedule.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BSMU_Schedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			InitializeComponent ();
		    BindingContext = new MenuViewModel();
		}
	}
}