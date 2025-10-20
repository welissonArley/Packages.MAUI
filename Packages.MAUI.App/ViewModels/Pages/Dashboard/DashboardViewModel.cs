using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Packages.MAUI.App.Navigation;

namespace Packages.MAUI.App.ViewModels.Pages.Dashboard;
public partial class DashboardViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public DashboardViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task PinCodesAuthorization() => await _navigationService.GoToAsync(RoutePages.PINCODE_PAGE);
}
