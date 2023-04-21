using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Packages.MAUI.App.ViewModels;
public partial class PinCodeViewModel : ObservableObject
{
    [RelayCommand]
    public static async void CodeEnded(string code)
    {
        await Application.Current.MainPage.DisplayAlert("I got the return", $"Did you typed the pinCode {code}?", "OK");
        await Shell.Current.GoToAsync("..");
    }
}
