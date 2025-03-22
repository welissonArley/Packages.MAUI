using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Packages.MAUI.App.Views.Popups;
using PinCodes.Authorization.Helpers;

namespace Packages.MAUI.App.ViewModels.PinCodes;
public partial class MaskedPinCodeViewModel : ObservableObject
{
    [RelayCommand]
    public static async Task ResendCode()
    {
        await MopupService.Instance.PushAsync(new InformationPopup(title: "Code on the Go: It's Coming Back to You!", body: "Your code is on its way back, freshly debugged and ready for action! 😄"));
    }

    [RelayCommand]
    public static async Task UserCompletedCode(string code)
    {
        if(code.All(c => c == '0'))
        {
            await MopupService.Instance.PushAsync(new InvalidCodePopup());

            PinCodeAuthorizationCenter.ClearPinCode();
        }
        else
        {
            await MopupService.Instance.PushAsync(new UserCompletedCodePopup(code));

            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    public static async Task FaceId()
    {
        await MopupService.Instance.PushAsync(new InformationPopup(title: "Face ID Magic", body: "Your face is the key – get ready to unlock with a smile! 😄"));
    }
}
