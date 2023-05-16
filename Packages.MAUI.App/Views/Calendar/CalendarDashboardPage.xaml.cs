using Packages.MAUI.App.ViewModels.Calendar;

namespace Packages.MAUI.App.Views.Calendar;

public partial class CalendarDashboardPage : ContentPage
{
	public CalendarDashboardPage(CalendarDashboardViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}