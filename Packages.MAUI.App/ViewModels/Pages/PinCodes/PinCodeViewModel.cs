using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Packages.MAUI.App.Navigation;
using Packages.MAUI.App.Views.Popups;
using PinCodes.Authorization.Helpers;

namespace Packages.MAUI.App.ViewModels.Pages.PinCodes;
public partial class PinCodeViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public PinCodeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public static async Task ResendCode()
    {
        var popup = new InformationPopup(
            title: "Code on the Go: It's Coming Back to You!",
            body: "Your code is on its way back, freshly debugged and ready for action! 😄");

        await MopupService.Instance.PushAsync(popup);
    }

    [RelayCommand]
    public async Task UserCompletedCode(string code)
    {
        if (code.All(c => c == '0'))
        {
            await MopupService.Instance.PushAsync(new InvalidCodePopup());

            PinCodeAuthorizationCenter.ClearPinCode();
        }
        else
        {
            await MopupService.Instance.PushAsync(new UserCompletedCodePopup(code));

            await _navigationService.ClosePage();
        }
    }

    [RelayCommand]
    public static async Task FaceId()
    {
        var popup = new InformationPopup(title: "Face ID Magic", body: "Your face is the key – get ready to unlock with a smile! 😄");
        
        await MopupService.Instance.PushAsync(popup);
    }
}
