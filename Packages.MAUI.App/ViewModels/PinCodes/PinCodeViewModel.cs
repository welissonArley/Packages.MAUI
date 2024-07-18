using CommunityToolkit.Mvvm.ComponentModel;

namespace Packages.MAUI.App.ViewModels.PinCodes;
public partial class PinCodeViewModel : ObservableObject
{
    [ObservableProperty]
    public string headline;

    [ObservableProperty]
    public string subheadline;

    public PinCodeViewModel()
    {
        Headline = "Confirm your PIN Code";
        Subheadline = "Enter the code to login";
    }
}
