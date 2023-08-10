using Packages.MAUI.App.ViewModels.Tabs;

namespace Packages.MAUI.App.Views.Tabs;

public partial class TabsViewExamplePage : ContentPage
{
	public TabsViewExamplePage(TabsViewExampleViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}