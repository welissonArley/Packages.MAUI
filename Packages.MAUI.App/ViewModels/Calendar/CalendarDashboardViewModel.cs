using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiDays.Views.Popups.DaySelector;
using MauiDays.Views.Popups.MonthSelector;
using Mopups.Services;
using Packages.MAUI.App.Views.Popups;

namespace Packages.MAUI.App.ViewModels.Calendar;
public partial class CalendarDashboardViewModel : ObservableObject
{
    [RelayCommand]
    public static async void SingleMonth()
    {
        var today = DateTime.Today;

        var popup = SingleMonthSelectorCalendarPopup
            .Instance(async (date) =>
            {
                await Callback(date, true);
            })
            .SetMinimumDate(new DateOnly(today.Year - 1, today.Month, 7))
            .SetMaximumDate(new DateOnly(today.Year + 1, today.Month, 7))
            .Build();

        await MopupService.Instance.PushAsync(popup);
    }

    [RelayCommand]
    public static async void SingleDay()
    {
        var today = DateTime.Today;

        var popup = SingleDaySelectorCalendarPopup
            .Instance(async (date) =>
            {
                await Callback(date, false);
            })
            .SetMinimumDate(new DateOnly(today.Year, today.Month - 1, 2))
            .SetMaximumDate(new DateOnly(today.Year, today.Month + 1, 2))
            .Build();

        await MopupService.Instance.PushAsync(popup);
    }

    private static async Task Callback(DateOnly date, bool isMonth)
    {
        var datestring = date.ToString(isMonth ? "MMMM/yyyy" : "dd-MMMM-yyyy");

        var popup = new ShowInformationPopup($"Did you choose {datestring}?");

        await MopupService.Instance.PushAsync(popup, false);
    }
}
