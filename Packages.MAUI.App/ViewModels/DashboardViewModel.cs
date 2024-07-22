using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packages.MAUI.App.Helpers;

namespace Packages.MAUI.App.ViewModels;
public partial class DashboardViewModel : ObservableObject
{
    [RelayCommand]
    public static async Task PinCodesAuthorization() => await Shell.Current.GoToAsync(RoutePages.PINCODE_PAGE);
}
