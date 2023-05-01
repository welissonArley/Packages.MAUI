using Packages.MAUI.App.ViewModels.PinCode;

namespace Packages.MAUI.App.Views.PinCode;

public partial class PinCodeDashboardPage : ContentPage
{
	public PinCodeDashboardPage(PinCodeDashboardViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}