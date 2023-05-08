namespace MauiDays.Views.Components.Popup;
public class CalendarHeaderComponent : BaseCalendarHeader
{
    private static CalendarHeaderComponent _instance;

    public static CalendarHeaderComponent Instance()
    {
        _instance = new CalendarHeaderComponent();

        return _instance;
    }
}
