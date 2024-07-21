using Mopups.Pages;
using Mopups.Services;

namespace Packages.MAUI.App.Views.Popups;

public partial class UserCompletedCodePopup : PopupPage
{
	public UserCompletedCodePopup(string code)
	{
		InitializeComponent();

		CodeSubmitted.Text = code;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		MopupService.Instance.PopAsync();
    }
}