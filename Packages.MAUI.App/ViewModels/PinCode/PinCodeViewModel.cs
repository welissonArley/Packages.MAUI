using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using Packages.MAUI.App.Views.Popups;

namespace Packages.MAUI.App.ViewModels.PinCode;
public partial class PinCodeViewModel : ObservableObject
{
    [RelayCommand]
    public static async void CodeEnded(string code)
    {
        await Shell.Current.GoToAsync("..");

        var popup = new ShowInformationPopup($"Did you typed {code}?");

        await MopupService.Instance.PushAsync(popup, false);
    }
}
