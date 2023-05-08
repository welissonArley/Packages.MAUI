using System.Windows.Input;

namespace MauiDays.Views.Components.Page;
public class CalendarHeaderComponent : BaseCalendarHeader
{
    public ICommand OnDaySelectedCommand { get; set; } = null;

    private static CalendarHeaderComponent _instance;

    public static CalendarHeaderComponent Instance()
    {
        _instance = new CalendarHeaderComponent();

        return _instance;
    }

    public CalendarHeaderComponent SetDaySelectedCallback(ICommand callbackCommand)
    {
        OnDaySelectedCommand = callbackCommand;

        return this;
    }

    protected override void HeaderButtonNextPreviousCommand(int timeToAdd)
    {
        base.HeaderButtonNextPreviousCommand(timeToAdd);

        OnDaySelectedCommand?.Execute(CurrentDate.GetDate());
    }
}
