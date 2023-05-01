using Mopups.Services;

namespace Packages.MAUI.App.Views.Popups;

public class ShowInformationPopup : Mopups.Pages.PopupPage
{
    public ShowInformationPopup(string message)
    {
        CloseWhenBackgroundIsClicked = false;
        BackgroundColor = Color.FromArgb("#F2A1A1A1");
        Content = new VerticalStackLayout
        {
            Spacing = 30,
            Margin = 30,
            Padding = 30,
            BackgroundColor = Colors.White,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Label { Text = "Result info", FontSize = 18, TextColor = Colors.Black, FontAttributes = FontAttributes.Bold },
                new Label { Text = message, FontSize = 18, TextColor = Colors.Black },
                new Button
                {
                    Text = "Confirm",
                    HorizontalOptions = LayoutOptions.End,
                    BackgroundColor = Color.FromArgb("#0066fe"),
                    FontAttributes = FontAttributes.Bold,
                    Command = new Command(async () =>
                    {
                        await MopupService.Instance.PopAsync();
                    })
                }
            }
        };
    }
}