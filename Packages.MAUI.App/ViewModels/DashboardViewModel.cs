using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packages.MAUI.App.Helpers;

namespace Packages.MAUI.App.ViewModels;
public partial class DashboardViewModel : ObservableObject
{
    [RelayCommand]
    public static async Task MauiCodes() => await Shell.Current.GoToAsync(RoutePages.DASHBOARD_PINCODE_PAGE);

    [RelayCommand]
    public static async Task MauiDays() => await Shell.Current.GoToAsync(RoutePages.DASHBOARD_CALENDAR_PAGE);

    [RelayCommand]
    public static async Task MauiTabs() => await Shell.Current.GoToAsync(RoutePages.TABSVIEW_PAGE);
}
