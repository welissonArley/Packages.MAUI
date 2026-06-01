using Packages.MAUI.App.ViewModels.Popups;

namespace Packages.MAUI.App.Navigation;

public interface INavigationService
{
    Task GoToAsync(ShellNavigationState state);
    Task GoToAsync(ShellNavigationState route, Dictionary<string, object> parameters);
    Task ClosePage();
    Task ShowPopup<TViewModel>(Dictionary<string, object>? queryAttributes = null) where TViewModel : ViewModelBaseForPopups;
}
