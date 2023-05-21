using Packages.MAUI.App.Services.Screen;
using Packages.MAUI.App.ViewModels.Calendar;

namespace Packages.MAUI.App.Views.Calendar;

public partial class CalendarDashboardPage : ContentPage
{
    private readonly DeviceOrientationService _orientationService;

    public CalendarDashboardPage(CalendarDashboardViewModel viewModel)
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