using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Shapes;
using Packages.MAUI.App.ViewModels.Popups;

namespace Packages.MAUI.App.Navigation;

public class NavigationService : INavigationService
{
    private readonly IPopupService _popupService;

    public NavigationService(IPopupService popupService)
    {
        _popupService = popupService;
    }

    public async Task GoToAsync(ShellNavigationState state) => await Shell.Current.GoToAsync(state);
    public async Task GoToAsync(ShellNavigationState route, Dictionary<string, object> parameters) => await Shell.Current.GoToAsync(route, parameters);

    public async Task ClosePage() => await GoToAsync("..");

    public async Task ShowPopup<TViewModel>(Dictionary<string, object>? queryAttributes = null) where TViewModel : ViewModelBaseForPopups
    {
        var popupOptions = new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = false,
            Shadow = null,
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10),
                StrokeThickness = 0
            }
        };

        await _popupService.ShowPopupAsync<TViewModel>(Shell.Current, popupOptions, queryAttributes);
    }
}
