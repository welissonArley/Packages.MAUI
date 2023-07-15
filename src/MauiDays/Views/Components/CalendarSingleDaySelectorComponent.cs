using MauiDays.Services;
using System.Globalization;
using System.Windows.Input;

namespace MauiDays.Views.Components;
public class CalendarSingleDaySelectorComponent : ContentView
{
    private const short AMOUNT_SLOTS_ON_CALENDAR = 42;
    private const short AMOUNT_DAYS_OF_WEEK_ON_CALENDAR = 7;
    private const double OPACITY_FOR_DISABLE_DAY = 0.2;

    private Border CurrentLayoutSelected;
    private readonly IList<Label> _daysOfTheWeek;
    private readonly IList<Border> _days;
    private readonly IList<(int day, Border layout)> _daysWithEvents;
    private readonly Grid _grid;

    private readonly DateOnlyService CurrentDate;
    private CultureInfo Culture { get; set; }
    private Color PrimaryColor { get; set; }
    private Color HighlightColor { get; set; }
    private string DaysFontFamily { get; set; }
    private Color SelectedBackgroundColor { get; set; }
    private Color SelectedDayColor { get; set; }
    private DateOnly? MinimumDate { get; set; }
    private DateOnly? MaximumDate { get; set; }
    private IList<int> DaysWithEvents { get; set; } = new List<int>();
    private ICommand CallbackCommand { get; set; }

    public CalendarSingleDaySelectorComponent(DateOnlyService currentDate)
    {
        CurrentDate = currentDate;
        _daysOfTheWeek = new List<Label>(AMOUNT_DAYS_OF_WEEK_ON_CALENDAR);
        _days = new List<Border>(AMOUNT_SLOTS_ON_CALENDAR);

        _daysWithEvents = new List<(int, Border)>();

        _grid = new Grid
        {
            ColumnSpacing = 0,
            RowSpacing = 0,
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
            },
            RowDefinitions =
            {
                new RowDefinition(height: 20),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
            }
        };

        Content = _grid;

        CurrentDate.OnHeaderChangeDate(MonthChanged);
    }

    public void SetDaysOfWeekFontFamily(string fontFamily)
    {
        foreach (var dayOfTheWeek in _daysOfTheWeek)
            dayOfTheWeek.FontFamily = fontFamily;
    }
    public void SetColor(Color color)
    {
        PrimaryColor = color;

        foreach (var dayOfTheWeek in _daysOfTheWeek)
            dayOfTheWeek.TextColor = color;

        foreach (var dayLabel in _days.Where(c => c.Content is not null).Select(c => c.Content as Label))
            dayLabel.TextColor = color;
    }
    public void SetDaysFontFamily(string fontFamily)
    {
        DaysFontFamily = fontFamily;

        foreach (var dayLabel in _days.Where(c => c.Content is not null).Select(c => c.Content as Label))
            dayLabel.FontFamily = fontFamily;
    }
    public void SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;
        if (CurrentLayoutSelected is not null)
            CurrentLayoutSelected.Background = color;
    }
    public void SetSelectedDayColor(Color color)
    {
        SelectedDayColor = color;
        if (CurrentLayoutSelected is not null)
            (CurrentLayoutSelected.Content as Label).TextColor = color;
    }
    public void SetMinimumDate(DateOnly date) => MinimumDate = date;
    public void SetMaximumDate(DateOnly date) => MaximumDate = date;
    public void SetCallback(ICommand callbackCommand) => CallbackCommand = callbackCommand;
    public void SetDaysWithEvents(IList<int> daysWithEvents)
    {
        DaysWithEvents = daysWithEvents;

        var currentDate = CurrentDate.GetDate();

        foreach (var (day, layout) in _daysWithEvents)
            (layout.Content as Label).TextColor = currentDate.Day == day ? SelectedDayColor : PrimaryColor;

        _daysWithEvents.Clear();

        var daysContent = _days.Where(c => c.Content is not null);
        if (!daysContent.Any())
            return;

        var daysLabel = daysContent.Select(c => c.Content as Label);

        foreach (var day in daysWithEvents)
        {
            daysLabel.ElementAt(day - 1).TextColor = currentDate.Day == day ? SelectedDayColor : HighlightColor;
            _daysWithEvents.Add((day, daysContent.ElementAt(day - 1)));
        }
    }
    public void SetHighlightColor(Color color)
    {
        HighlightColor = color;

        var currentDate = CurrentDate.GetDate();

        foreach (var (day, layout) in _daysWithEvents)
            (layout.Content as Label).TextColor = currentDate.Day == day ? SelectedDayColor : color;
    }

    private void MonthChanged()
    {
        var currentDate = CurrentDate.GetDate();
        var lastDayOfMonth = DateTime.DaysInMonth(year: currentDate.Year, month: currentDate.Month);
        var startColumnForCurrentMonth = CalculateStartColumn();
        var day = 1;
        var date = new DateOnly(year: currentDate.Year, month: currentDate.Month, 1);

        CurrentLayoutSelected.Background = Colors.Transparent;

        if (!AllowSelectDay(currentDate))
        {
            if (MinimumDate.HasValue && currentDate.Month == MinimumDate.Value.Month)
                CurrentDate.SetDate(new DateOnly(currentDate.Year, currentDate.Month, MinimumDate.Value.Day));
            else if (MaximumDate.HasValue && currentDate.Month == MaximumDate.Value.Month)
                CurrentDate.SetDate(new DateOnly(currentDate.Year, currentDate.Month, MaximumDate.Value.Day));

            currentDate = CurrentDate.GetDate();
        }

        Label dayLabel;
        for (var index = 0; index < AMOUNT_SLOTS_ON_CALENDAR; index++)
        {
            var layout = _days.ElementAt(index);
            layout.GestureRecognizers.Clear();
            layout.Opacity = 1;

            if (index >= startColumnForCurrentMonth && day <= lastDayOfMonth)
            {
                if (layout.Content is null)
                {
                    dayLabel = CreateDayLabel(day, PrimaryColor);
                    layout.Content = dayLabel;
                }
                else
                {
                    dayLabel = layout.Content as Label;
                    dayLabel.Text = $"{day}";
                }

                if (day == currentDate.Day)
                {
                    layout.Background = SelectedBackgroundColor;
                    dayLabel.TextColor = SelectedDayColor;

                    CurrentLayoutSelected = layout;
                }
                else
                {
                    layout.Background = Colors.Transparent;
                    dayLabel.TextColor = PrimaryColor;
                }

                if (AllowSelectDay(date))
                    layout.GestureRecognizers.Add(CreateTappedCommandForDay(date, layout));
                else
                    layout.Opacity = OPACITY_FOR_DISABLE_DAY;

                day++;
                date = date.AddDays(1);
            }
            else
                layout.Content = null;
        }

        CallbackCommand?.Execute(currentDate);
    }
    public void Build(CultureInfo culture)
    {
        Culture = culture;

        SetDaysOfTheWeek();
        CreateDaysOnCalendar();
    }

    private void SetDaysOfTheWeek()
    {
        var daysOfWeek = Culture.DateTimeFormat.AbbreviatedDayNames;

        if (Culture.DateTimeFormat.FirstDayOfWeek != DayOfWeek.Sunday)
            daysOfWeek = daysOfWeek.Skip(1).Append(daysOfWeek.ElementAt(0)).ToArray();

        for (var index = 0; index < daysOfWeek.Length; index++)
        {
            var label = CreateLabelForDayOfTheWeek(daysOfWeek.ElementAt(index));

            _daysOfTheWeek.Add(label);

            _grid.Add(label, column: index, row: 0);
        }
    }
    private void CreateDaysOnCalendar()
    {
        var currentDate = CurrentDate.GetDate();
        var row = 1;
        var column = 0;
        var lastDayOfMonth = DateTime.DaysInMonth(year: currentDate.Year, month: currentDate.Month);
        var startColumnForCurrentMonth = CalculateStartColumn();
        short day = 1;
        var date = new DateOnly(year: currentDate.Year, month: currentDate.Month, 1);

        for (var index = 0; index < AMOUNT_SLOTS_ON_CALENDAR; index++)
        {
            var layout = CreateLayout();

            if (index >= startColumnForCurrentMonth && day <= lastDayOfMonth)
            {
                if (day == currentDate.Day)
                {
                    layout.Background = SelectedBackgroundColor;
                    layout.Content = CreateDayLabel(day, SelectedDayColor);
                    CurrentLayoutSelected = layout;
                }
                else if (DaysWithEvents.Contains(day))
                {
                    layout.Content = CreateDayLabel(day, HighlightColor);
                    _daysWithEvents.Add((day, layout));
                }
                else
                    layout.Content = CreateDayLabel(day, PrimaryColor);

                if (AllowSelectDay(date))
                    layout.GestureRecognizers.Add(CreateTappedCommandForDay(date, layout));
                else
                    layout.Opacity = OPACITY_FOR_DISABLE_DAY;

                day++;
                date = date.AddDays(1);
            }

            _days.Add(layout);

            _grid.Add(layout, column, row);

            column++;
            if (column == AMOUNT_DAYS_OF_WEEK_ON_CALENDAR)
            {
                column = 0;
                row++;
            }
        }
    }

    private bool AllowSelectDay(DateOnly date)
    {
        if (MinimumDate.HasValue && date < MinimumDate.Value)
            return false;

        if (MaximumDate.HasValue && date > MaximumDate.Value)
            return false;

        return true;
    }
    private static Label CreateLabelForDayOfTheWeek(string day)
    {
        return new()
        {
            Text = day,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 12
        };
    }
    private int CalculateStartColumn()
    {
        var currentDate = CurrentDate.GetDate();
        var date = new DateOnly(year: currentDate.Year, month: currentDate.Month, 1);

        var dayofWeek = (int)date.DayOfWeek;

        var column = Culture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday ? dayofWeek : dayofWeek - 1;

        return column == -1 ? 0 : column;
    }
    private static Border CreateLayout()
    {
        return new()
        {
            Background = Colors.Transparent,
            Padding = 10,
            Margin = 0
        };
    }
    private Label CreateDayLabel(int day, Color color)
    {
        return new()
        {
            Text = $"{day}",
            TextColor = color,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontFamily = DaysFontFamily
        };
    }
    private Label CreateDayLabelForSelectedDay(int day)
    {
        var label = CreateDayLabel(day, SelectedDayColor);
        label.TextColor = SelectedDayColor;

        return label;
    }
    private TapGestureRecognizer CreateTappedCommandForDay(DateOnly day, Border layout)
    {
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (s, e) =>
        {
            var currentDate = CurrentDate.GetDate();

            if (day.Day != currentDate.Day)
            {
                layout.Background = SelectedBackgroundColor;
                layout.Content = CreateDayLabelForSelectedDay(day.Day);

                CurrentLayoutSelected.Background = Colors.Transparent;
                CurrentLayoutSelected.Content = CreateDayLabel(currentDate.Day, _daysWithEvents.Any(c => c.day == currentDate.Day) ? HighlightColor : PrimaryColor);

                CurrentDate.SetDate(day);
                CurrentLayoutSelected = layout;

                CallbackCommand?.Execute(day);
            }
        };

        return tapGestureRecognizer;
    }
}
