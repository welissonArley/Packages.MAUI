using Packages.MAUI.App.ViewModels.Tabs;

namespace Packages.MAUI.App.Views.Tabs.Styles;

public partial class UnderlinedStylePage : ContentPage
{
    public UnderlinedStylePage(StyleTabViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}