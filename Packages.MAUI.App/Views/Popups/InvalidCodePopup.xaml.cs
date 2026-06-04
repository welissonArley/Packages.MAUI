using CommunityToolkit.Maui.Views;
using Packages.MAUI.App.ViewModels.Popups.InvalidCode;

namespace Packages.MAUI.App.Views.Popups;

public partial class InvalidCodePopup : Popup
{
	public InvalidCodePopup(InvalidCodeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}