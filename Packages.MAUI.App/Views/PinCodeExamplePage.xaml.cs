using Packages.MAUI.App.ViewModels;
using PinControl.MAUI.Views.Pages;

namespace Packages.MAUI.App.Views;

public partial class PinCodeExamplePage : CodePage
{
	public PinCodeExamplePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}