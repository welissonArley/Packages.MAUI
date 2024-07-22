using Mopups.Pages;
using Mopups.Services;

namespace Packages.MAUI.App.Views.Popups;

public partial class UserCompletedCodePopup : PopupPage
{
	public UserCompletedCodePopup(string code)
	{
		InitializeComponent();

        var characters = code.ToCharArray();

        CodeSubmitted.Text = string.Join(" ", characters);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		MopupService.Instance.PopAsync();
    }
}