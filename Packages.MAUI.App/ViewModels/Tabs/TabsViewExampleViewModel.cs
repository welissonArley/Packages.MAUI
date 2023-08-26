using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packages.MAUI.App.Helpers;

namespace Packages.MAUI.App.ViewModels.Tabs;

public partial class TabsViewExampleViewModel : ObservableObject
{
    [RelayCommand]
    public static async Task RoundedStyle() => await Shell.Current.GoToAsync(RoutePages.ROUNDED_TABS_PAGE);

    [RelayCommand]
    public static async Task UnderlinedStyle() => await Shell.Current.GoToAsync(RoutePages.UNDERLINED_TABS_PAGE);

    [RelayCommand]
    public static async Task SymmetricStyle() => await Shell.Current.GoToAsync(RoutePages.SYMMETRIC_TABS_PAGE);
}
