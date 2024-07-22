using Packages.MAUI.App.ViewModels;

namespace Packages.MAUI.App;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}