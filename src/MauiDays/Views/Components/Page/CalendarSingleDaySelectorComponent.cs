using System.Windows.Input;

namespace MauiDays.Views.Components.Page;
public class CalendarSingleDaySelectorComponent : BaseCalendarDaysComponent
{
    public IList<int> DaysWithEvents { get; set; } = new List<int>();
    public Color HighlightColor { get; set; }
    public ICommand OnDaySelectedCommand { get; set; } = null;

    private static CalendarSingleDaySelectorComponent _instance;

    public static CalendarSingleDaySelectorComponent Instance()
    {
        _instance = new CalendarSingleDaySelectorComponent();

        return _instance;
    }

    public CalendarSingleDaySelectorComponent SetDaySelectedCallback(ICommand callbackCommand)
    {
        OnDaySelectedCommand = callbackCommand;

        return this;
    }

    public CalendarSingleDaySelectorComponent SetDaysWithEvents(IList<int> daysWithEvents, Color highlightColor)
    {
        DaysWithEvents = daysWithEvents;
        HighlightColor = highlightColor;

        return this;
    }

    protected override TapGestureRecognizer CreateTappedCommandForDay(DateOnly day, Border layout)
    {
        var tapGesture = base.CreateTappedCommandForDay(day, layout);

        tapGesture.Tapped += (s, e) =>
        {
            OnDaySelectedCommand?.Execute(CurrentDate.GetDate());
        };

        return tapGesture;
    }

    protected override Label CreateDayLabel(int day, Color color)
    {
        var label = base.CreateDayLabel(day, color);

        if (DaysWithEvents.Contains(day))
            label.TextColor = HighlightColor;

        return label;
    }
}
