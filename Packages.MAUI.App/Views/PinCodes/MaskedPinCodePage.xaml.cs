using Packages.MAUI.App.ViewModels.PinCodes;
using PinCodes.Authorization.Views.Pages;

namespace Packages.MAUI.App.Views.PinCodes;

public partial class MaskedPinCodePage : CodePage
{
	public MaskedPinCodePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}