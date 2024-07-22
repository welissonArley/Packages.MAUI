using Mopups.Pages;
using Mopups.Services;

namespace Packages.MAUI.App.Views.Popups;

public partial class InformationPopup : PopupPage
{
	public InformationPopup(string title, string body)
	{
		InitializeComponent();

        LabelTitle.Text = title;
        LabelBody.Text = body;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
}