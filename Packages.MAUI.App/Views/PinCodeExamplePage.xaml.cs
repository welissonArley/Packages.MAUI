using MauiCode.Views.Pages;
using Packages.MAUI.App.ViewModels;

namespace Packages.MAUI.App.Views;

public partial class PinCodeExamplePage : CodePage
{
	public PinCodeExamplePage(PinCodeViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}