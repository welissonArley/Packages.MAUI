using Packages.MAUI.App.Services.Screen;
using Packages.MAUI.App.ViewModels;

namespace Packages.MAUI.App.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DeviceOrientationService _orientationService;

    public DashboardPage(DashboardViewModel viewModel)
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

