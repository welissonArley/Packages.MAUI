using Packages.MAUI.App.ViewModels.PinCodes;
using PinCodes.Authorization.Views.Pages;

namespace Packages.MAUI.App.Views.PinCodes;

public partial class PinCodePage : CodePage
{
	public PinCodePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}