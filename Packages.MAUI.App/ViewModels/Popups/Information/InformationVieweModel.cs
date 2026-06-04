using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Packages.MAUI.App.ViewModels.Popups.Information;

public partial class InformationVieweModel : ViewModelBaseForPopups, IQueryAttributable
{
    [ObservableProperty]
    public string title;

    [ObservableProperty]
    public string message;

    private readonly IPopupService _popupService;

    public InformationVieweModel(IPopupService popupService)
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
        Title = (string)query[nameof(InformationVieweModel.Title)];
        Message = (string)query[nameof(InformationVieweModel.Message)];
    }
}