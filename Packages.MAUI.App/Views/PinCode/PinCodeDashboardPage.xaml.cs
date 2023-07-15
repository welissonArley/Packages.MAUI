using Packages.MAUI.App.Services.Screen;
using Packages.MAUI.App.ViewModels.PinCode;

namespace Packages.MAUI.App.Views.PinCode;

public partial class PinCodeDashboardPage : ContentPage
{
    private readonly DeviceOrientationService _orientationService;

    public PinCodeDashboardPage(PinCodeDashboardViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

        _orientationService = new ();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _orientationService.SetPortraitOrientation();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _orientationService.ResetOrientation();
    }
}