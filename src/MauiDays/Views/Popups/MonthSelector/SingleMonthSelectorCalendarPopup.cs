using MauiDays.Views.Components;

namespace MauiDays.Views.Popups.MonthSelector;
public class SingleMonthSelectorCalendarPopup : BaseCalendarPopup
{
    public SingleMonthSelectorCalendarPopup(Action callbackCancel, Action<DateOnly> callbackConfirm) : base(callbackCancel, callbackConfirm)
    {
    }

    protected override IView CreateCalendar()
    {
        var calendarComponent = new CalendarSingleMonthSelectorComponent(_currentDate);
        calendarComponent.SetColor(PrimaryColor);

        if (MinimumDate.HasValue)
            calendarComponent.SetMinimumDate(MinimumDate.Value);
        if (MaximumDate.HasValue)
            calendarComponent.SetMaximumDate(MaximumDate.Value);

        calendarComponent.SetSelectedBackgroundColor(SelectedBackgroundColor);
        calendarComponent.SetSelectedMonthColor(SelectedLabelColor);
        calendarComponent.SetMonthsFontFamily(LabelFontFamily);
        calendarComponent.Build(Culture);

        return calendarComponent;
    }

    protected override bool IsMonthCalendar() => true;
}
