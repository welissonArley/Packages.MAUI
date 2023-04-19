using Packages.MAUI.App.Helpers;

namespace Packages.MAUI.App.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(RoutePages.PINCODE_PAGE);
    }
}

