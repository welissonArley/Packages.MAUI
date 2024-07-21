using Packages.MAUI.App.ViewModels.PinCodes;
using PinCodes.Authorization.Views.Pages;
using System.Runtime.CompilerServices;

namespace Packages.MAUI.App.Views.PinCodes;

public partial class PinCodePage : CodePage
{
	public PinCodePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}

public class myButton : Button
{
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
    }
}