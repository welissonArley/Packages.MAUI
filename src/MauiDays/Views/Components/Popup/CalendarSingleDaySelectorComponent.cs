namespace MauiDays.Views.Components.Popup;
public class CalendarSingleDaySelectorComponent : BaseCalendarDaysComponent
{
    private static CalendarSingleDaySelectorComponent _instance;

    public static BaseCalendarDaysComponent Instance()
    {
        _instance = new CalendarSingleDaySelectorComponent();

        return _instance;
    }
}