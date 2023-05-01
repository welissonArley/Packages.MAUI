using Packages.MAUI.App.ViewModels;

namespace Packages.MAUI.App.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}

