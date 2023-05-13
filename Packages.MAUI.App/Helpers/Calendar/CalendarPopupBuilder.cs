using MauiDays.Views.Popups;
using MauiDays.Views.Popups.DaySelector;
using MauiDays.Views.Popups.MonthSelector;
using Mopups.Services;
using Shared.Helpers.Extensions;
using System.Globalization;

namespace Packages.MAUI.App.Helpers.Calendar;
public static class CalendarPopupBuilder
{
    public static BaseCalendarPopup SingleMonth(Action<DateOnly> callbackConfirm)
    {
        return new SingleMonthSelectorCalendarPopup(async () => { await MopupService.Instance.PopAsync(); }, callbackConfirm)
            .SetPopupBackgroundColor(PopupBackgroundColor())
            .SetCalendarBackgroundColor(CalendarBackgroundColor())
            .SetPrimaryColor(PrimaryColor())
            .SetHeaderFontFamily(HeaderFontFamily())
            .SetConfirmButtonColor(ColorForConfirmButton())
            .SetTextCancel("CANCEL")
            .SetConfirmButtonTextColor(SelectedConfirmButtonTextColor())
            .SetCancelFontFamily(CancelFontFamily())
            .SetCulture(Culture())
            .SetSelectedBackgroundColor(SelectedBackgroundColor())
            .SetSelectedLabelColor(SelectedLabelColor())
            .SetLabelFontFamily(LabelFontFamily());
    }

    public static BaseCalendarPopup SingleDay(Action<DateOnly> callbackConfirm)
    {
        return new SingleDaySelectorCalendarPopup(async () => { await MopupService.Instance.PopAsync(); }, callbackConfirm)
            .SetDaysOfWeekFontFamily(LabelDaysOfWeekFontFamily())
            .SetPopupBackgroundColor(PopupBackgroundColor())
            .SetCalendarBackgroundColor(CalendarBackgroundColor())
            .SetPrimaryColor(PrimaryColor())
            .SetHeaderFontFamily(HeaderFontFamily())
            .SetConfirmButtonColor(ColorForConfirmButton())
            .SetTextCancel("CANCEL")
            .SetConfirmButtonTextColor(SelectedConfirmButtonTextColor())
            .SetCancelFontFamily(CancelFontFamily())
            .SetCulture(Culture())
            .SetSelectedBackgroundColor(SelectedBackgroundColor())
            .SetSelectedLabelColor(SelectedLabelColor())
            .SetLabelFontFamily(LabelFontFamily());
    }

    private static Color ColorForConfirmButton() => Color.FromArgb(Application.Current.IsLightMode() ? "#40806A" : "#00D46A");
    private static Color PrimaryColor() => Application.Current.IsLightMode() ? Colors.Black : Colors.White;
    private static Color SelectedBackgroundColor() => Application.Current.IsLightMode() ? Colors.Black : Colors.White;
    private static Color SelectedLabelColor() => Application.Current.IsLightMode() ? Colors.White : Colors.Black;
    private static Color SelectedConfirmButtonTextColor() => Application.Current.IsLightMode() ? Colors.White : Colors.Black;
    private static Color CalendarBackgroundColor() => Application.Current.IsLightMode() ? Colors.White : Application.Current.GetDarkMode();
    private static Color PopupBackgroundColor() => Color.FromArgb("#80A1A1A1");
    private static string CancelFontFamily() => "OpenSansRegular";
    private static string HeaderFontFamily() => "OpenSansSemibold";
    private static string LabelFontFamily() => "OpenSansRegular";
    private static string LabelDaysOfWeekFontFamily() => "OpenSansRegular";
    private static CultureInfo Culture() => CultureInfo.CurrentCulture;
}
