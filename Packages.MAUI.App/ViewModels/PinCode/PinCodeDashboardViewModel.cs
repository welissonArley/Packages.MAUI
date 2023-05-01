using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packages.MAUI.App.Helpers;
using Packages.MAUI.App.Model.Enums;

namespace Packages.MAUI.App.ViewModels.PinCode;
public partial class PinCodeDashboardViewModel : ObservableObject
{
    [ObservableProperty]
    public string headline;

    [ObservableProperty]
    public string subheadline;

    [ObservableProperty]
    public KeyboardType keyboardType;

    [ObservableProperty]
    public IllustrationType illustrationType;

    [ObservableProperty]
    public bool showCode;

    [ObservableProperty]
    public bool squareViewer;

    [RelayCommand]
    public void KeyboardTypeChanged(KeyboardType keyboardType) => KeyboardType = keyboardType;

    [RelayCommand]
    public void IllustrationChanged(IllustrationType illustrationType) => IllustrationType = illustrationType;

    [RelayCommand]
    public async Task GeneratePage()
    {
        var parameters = new Dictionary<string, object>
        {
            { "headline", Headline },
            { "subHeadline", Subheadline },
            { "illustration", illustrationType },
            { "showCode", ShowCode },
            { "squareViewer", SquareViewer },
            { "keyboardType", KeyboardType }
        };

        await Shell.Current.GoToAsync(RoutePages.PINCODE_PAGE, parameters);
    }

    public PinCodeDashboardViewModel()
    {
        Headline = string.Empty;
        Subheadline = string.Empty;
    }
}
