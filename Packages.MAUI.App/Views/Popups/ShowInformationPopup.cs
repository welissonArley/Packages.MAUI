using Mopups.Services;
using Packages.MAUI.App.Helpers.Extensions;

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
            BackgroundColor = Application.Current.IsLightMode() ? Colors.White : Application.Current.GetDarkMode(),
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Label { Text = "Result info", FontSize = 18, TextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White, FontAttributes = FontAttributes.Bold },
                new Label { Text = message, FontSize = 18, TextColor = Application.Current.IsLightMode() ? Colors.Black : Colors.White },
                new Button
                {
                    Text = "Confirm",
                    HorizontalOptions = LayoutOptions.End,
                    TextColor = Application.Current.IsLightMode() ? Colors.White : Application.Current.GetDarkMode(),
                    BackgroundColor = Color.FromArgb(Application.Current.IsLightMode() ? "#3455DB" : "#19B5FE"),
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