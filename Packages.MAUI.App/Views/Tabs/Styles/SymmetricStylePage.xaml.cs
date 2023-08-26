using Packages.MAUI.App.ViewModels.Tabs;

namespace Packages.MAUI.App.Views.Tabs.Styles;

public partial class SymmetricStylePage : ContentPage
{
    public SymmetricStylePage(StyleTabViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}