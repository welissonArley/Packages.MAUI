using Mopups.Pages;
using Mopups.Services;

namespace Packages.MAUI.App.Views.Popups;

public partial class InvalidCodePopup : PopupPage
{
	public InvalidCodePopup()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
}