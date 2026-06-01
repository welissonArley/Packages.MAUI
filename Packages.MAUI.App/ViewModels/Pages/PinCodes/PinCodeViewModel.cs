using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packages.MAUI.App.Navigation;
using Packages.MAUI.App.ViewModels.Popups.Information;
using Packages.MAUI.App.ViewModels.Popups.InvalidCode;
using Packages.MAUI.App.ViewModels.Popups.UserCompletedCode;
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
    public async Task ResendCode()
    {
        var queryAttributes = new Dictionary<string, object>
        {
            [nameof(InformationVieweModel.Title)] = "Code on the go: It's coming back to you!",
            [nameof(InformationVieweModel.Message)] = "Your code is on its way back, freshly debugged and ready for action! 😄"
        };
        
        await _navigationService.ShowPopup<InformationVieweModel>(queryAttributes);
    }

    [RelayCommand]
    public async Task UserCompletedCode(string code)
    {
        if (code.All(c => c == '0'))
        {
            await _navigationService.ShowPopup<InvalidCodeViewModel>();

            PinCodeAuthorizationCenter.ClearPinCode();
        }
        else
        {
            var queryAttributes = new Dictionary<string, object>
            {
                [nameof(UserCompletedCodeViewModel.Code)] = code
            };

            await _navigationService.ShowPopup<UserCompletedCodeViewModel>(queryAttributes);

            await _navigationService.ClosePage();
        }
    }

    [RelayCommand]
    public async Task FaceId()
    {
        var queryAttributes = new Dictionary<string, object>
        {
            [nameof(InformationVieweModel.Title)] = "Face ID Magic",
            [nameof(InformationVieweModel.Message)] = "Your face is the key – get ready to unlock with a smile! 😄"
        };

        await _navigationService.ShowPopup<InformationVieweModel>(queryAttributes);
    }
}
