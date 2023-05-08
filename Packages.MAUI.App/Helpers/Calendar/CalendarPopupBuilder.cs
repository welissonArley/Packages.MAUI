using MauiDays.Views.Popups.DaySelector;
using MauiDays.Views.Popups.MonthSelector;
using Shared.Helpers.Extensions;

namespace Packages.MAUI.App.Helpers.Calendar;
public static class CalendarPopupBuilder
{
    public static SingleMonthSelectorCalendarPopup SingleMonth(Action<DateOnly> callbackConfirm)
    {
        return SingleMonthSelectorCalendarPopup
            .Instance(callbackConfirm)
            .PopupBackgroundColor(PopupBackgroundColor())
            .SetCancelFontFamily(CancelFontFamily())
            .SetHeaderFontFamily(HeaderFontFamily())
            .SetMonthFontFamily(LabelFontFamily())
            .CalendarBackgroundColor(CalendarBackgroundColor())
            .SetPrimaryColor(PrimaryColor())
            .SetConfirmButtonColor(ColorForConfirmButton())
            .SetSelectedBackgroundColor(SelectedBackgroundColor())
            .SetSelectedMonthColor(SelectedLabelColor());
    }

    public static SingleDaySelectorCalendarPopup SingleDay(Action<DateOnly> callbackConfirm)
    {
        return SingleDaySelectorCalendarPopup
            .Instance(callbackConfirm)
            .PopupBackgroundColor(PopupBackgroundColor())
            .SetCancelFontFamily(CancelFontFamily())
            .SetHeaderFontFamily(HeaderFontFamily())
            .SetDaysFontFamily(LabelFontFamily())
            .CalendarBackgroundColor(CalendarBackgroundColor())
            .SetPrimaryColor(PrimaryColor())
            .SetConfirmButtonColor(ColorForConfirmButton())
            .SetSelectedBackgroundColor(SelectedBackgroundColor())
            .SetSelectedDayColor(SelectedLabelColor());
    }

    private static Color ColorForConfirmButton() => Color.FromArgb(Application.Current.IsLightMode() ? "#40806A" : "#00D46A");
    private static Color PrimaryColor() => Application.Current.IsLightMode() ? Colors.Black : Colors.White;
    private static Color SelectedBackgroundColor() => Application.Current.IsLightMode() ? Colors.Black : Colors.White;
    private static Color SelectedLabelColor() => Application.Current.IsLightMode() ? Colors.White : Colors.Black;
    private static Color CalendarBackgroundColor() => Application.Current.IsLightMode() ? Colors.White : Application.Current.GetDarkMode();
    private static Color PopupBackgroundColor() => Color.FromArgb("#80A1A1A1");
    private static string CancelFontFamily() => "OpenSansRegular";
    private static string HeaderFontFamily() => "OpenSansSemibold";
    private static string LabelFontFamily() => "OpenSansRegular";
}
