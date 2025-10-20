using Packages.MAUI.App.ViewModels.Pages.PinCodes;
using PinCodes.Authorization.Views.Pages;

namespace Packages.MAUI.App.Views.Pages.PinCodes;

public partial class PinCodePage : CodePage
{
	public PinCodePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}