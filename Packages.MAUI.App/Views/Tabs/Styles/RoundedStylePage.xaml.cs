using Packages.MAUI.App.ViewModels.Tabs;

namespace Packages.MAUI.App.Views.Tabs.Styles;

public partial class RoundedStylePage : ContentPage
{
	public RoundedStylePage(StyleTabViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}