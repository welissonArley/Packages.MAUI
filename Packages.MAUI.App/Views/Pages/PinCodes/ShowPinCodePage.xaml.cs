using Packages.MAUI.App.ViewModels.Pages.PinCodes;
using PinCodes.Authorization.Views.Pages;

namespace Packages.MAUI.App.Views.Pages.PinCodes;

public partial class ShowPinCodePage : CodePage
{
	public ShowPinCodePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}