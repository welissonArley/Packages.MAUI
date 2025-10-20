using Packages.MAUI.App.ViewModels.Pages.Dashboard;

namespace Packages.MAUI.App.Views.Pages.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}