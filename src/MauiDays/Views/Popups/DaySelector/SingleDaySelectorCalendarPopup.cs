using MauiDays.Views.Components;

namespace MauiDays.Views.Popups.DaySelector;
public class SingleDaySelectorCalendarPopup : BaseCalendarPopup
{
    private string DaysOfWeekFontFamily { get; set; }

    public SingleDaySelectorCalendarPopup(Action callbackCancel, Action<DateOnly> callbackConfirm) : base(callbackCancel, callbackConfirm)
    {
    }

    protected override IView CreateCalendar()
    {
        var calendarComponent = new CalendarSingleDaySelectorComponent(_currentDate);
        calendarComponent.SetColor(PrimaryColor);
        
        if (MinimumDate.HasValue)
            calendarComponent.SetMinimumDate(MinimumDate.Value);
        if (MaximumDate.HasValue)
            calendarComponent.SetMaximumDate(MaximumDate.Value);

        calendarComponent.SetSelectedBackgroundColor(SelectedBackgroundColor);
        calendarComponent.SetSelectedDayColor(SelectedLabelColor);
        calendarComponent.SetDaysFontFamily(LabelFontFamily);
        calendarComponent.SetDaysOfWeekFontFamily(DaysOfWeekFontFamily);
        calendarComponent.Build(Culture);

        return calendarComponent;
    }

    public BaseCalendarPopup SetDaysOfWeekFontFamily(string fontFamily)
    {
        DaysOfWeekFontFamily = fontFamily;
        return this;
    }

    protected override bool IsMonthCalendar() => false;
}