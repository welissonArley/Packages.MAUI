using CommunityToolkit.Maui.Views;
using Packages.MAUI.App.ViewModels.Popups.Information;

namespace Packages.MAUI.App.Views.Popups;

public partial class InformationPopup : Popup
{
	public InformationPopup(InformationVieweModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}