using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Packages.MAUI.App.ViewModels.Popups.UserCompletedCode;

public partial class UserCompletedCodeViewModel : ViewModelBaseForPopups, IQueryAttributable
{
    [ObservableProperty]
    public string code;

    private readonly IPopupService _popupService;

    public UserCompletedCodeViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    public async Task Close()
    {
        await _popupService.ClosePopupAsync(Shell.Current);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var code = (string)query[nameof(UserCompletedCodeViewModel.Code)];

        var characters = code.ToCharArray();

        Code = string.Join(" ", characters);
    }
}