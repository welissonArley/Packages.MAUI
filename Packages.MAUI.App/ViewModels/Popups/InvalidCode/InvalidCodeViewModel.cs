using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Input;

namespace Packages.MAUI.App.ViewModels.Popups.InvalidCode;

public partial class InvalidCodeViewModel : ViewModelBaseForPopups
{
    private readonly IPopupService _popupService;

    public InvalidCodeViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    public async Task Close()
    {
        await _popupService.ClosePopupAsync(Shell.Current);
    }
}