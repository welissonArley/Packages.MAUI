using MauiDays.Services;
using System.Globalization;

namespace MauiDays.Views.Components;
public abstract class BaseCalendarDaysComponent
{
    protected Border CurrentLayoutSelected;

    private readonly Grid _view;

    protected DateOnlyService CurrentDate;
    protected CultureInfo Culture { get; set; }
    protected Color PrimaryColor { get; set; }
    protected string DaysOfWeekFontFamily { get; set; }
    protected string DaysFontFamily { get; set; }
    protected Color SelectedDayColor { get; set; }
    protected Color SelectedBackgroundColor { get; set; }

    protected DateOnly? MinimumDate { get; set; }
    protected DateOnly? MaximumDate { get; set; }

    public BaseCalendarDaysComponent SetDate(DateOnlyService date)
    {
        CurrentDate = date;

        return this;
    }

    public BaseCalendarDaysComponent SetCulture(CultureInfo culture)
    {
        Culture = culture;

        return this;
    }

    public BaseCalendarDaysComponent SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public BaseCalendarDaysComponent SetDaysOfWeekFontFamily(string fontFamily)
    {
        DaysOfWeekFontFamily = fontFamily;

        return this;
    }

    public BaseCalendarDaysComponent SetDaysFontFamily(string fontFamily)
    {
        DaysFontFamily = fontFamily;

        return this;
    }

    public BaseCalendarDaysComponent SetSelectedDayColor(Color color)
    {
        SelectedDayColor = color;

        return this;
    }

    public BaseCalendarDaysComponent SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;

        return this;
    }

    public BaseCalendarDaysComponent SetMinimumDate(DateOnly? date)
    {
        MinimumDate = date;

        return this;
    }
    public BaseCalendarDaysComponent SetMaximumDate(DateOnly? date)
    {
        MaximumDate = date;

        return this;
    }

    protected BaseCalendarDaysComponent()
    {
        if (_view is null)
        {
            _view = new Grid
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

            Culture = CultureInfo.CurrentCulture;
        }
        else
            _view.Clear();
    }

    public IView Build()
    {
        CurrentDate.OnChangedMonth(() =>
        {
            var currentDate = CurrentDate.GetDate();
            if (!AllowSelectDay(currentDate))
            {
                if (MinimumDate.HasValue && currentDate.Month == MinimumDate.Value.Month)
                    CurrentDate.SetDate(new DateOnly(currentDate.Year, currentDate.Month, MinimumDate.Value.Day));
                else if (MaximumDate.HasValue && currentDate.Month == MaximumDate.Value.Month)
                    CurrentDate.SetDate(new DateOnly(currentDate.Year, currentDate.Month, MaximumDate.Value.Day));
            }

            FillCalendar();
        });

        FillCalendar();

        return _view;
    }

    private void FillCalendar()
    {
        _view.Clear();

        SetDaysOfTheWeekHeader();

        SetDaysOnCalendar();
    }

    private void SetDaysOfTheWeekHeader()
    {
        var daysOfWeek = Culture.DateTimeFormat.AbbreviatedDayNames;

        if (Culture.DateTimeFormat.FirstDayOfWeek != DayOfWeek.Sunday)
            daysOfWeek = daysOfWeek.Skip(1).Append(daysOfWeek.ElementAt(0)).ToArray();

        for (var index = 0; index < daysOfWeek.Length; index++)
            _view.Add(CreateLabelForDayOfTheWeek(daysOfWeek.ElementAt(index)), column: index, row: 0);
    }

    private Label CreateLabelForDayOfTheWeek(string day)
    {
        return new()
        {
            Text = day,
            TextColor = PrimaryColor,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 12,
            FontFamily = DaysOfWeekFontFamily
        };
    }

    private void SetDaysOnCalendar()
    {
        var currentDate = CurrentDate.GetDate();
        var firstDayCurrentMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
        var lastDayCurrentMonth = firstDayCurrentMonth.AddMonths(1).AddDays(-1);

        var startColumnForCurrentMonth = CalculateStartColumn(firstDayCurrentMonth);

        (int lastRowSet, int lastColumnSet) = SetDaysCurrentMonth(lastDayCurrentMonth, startColumnForCurrentMonth);
        CompleteCalendarToAvoidResize(lastRowSet, lastColumnSet);
    }

    private int CalculateStartColumn(DateOnly date)
    {
        var dayofWeek = (int)date.DayOfWeek;

        var column = Culture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday ? dayofWeek : dayofWeek - 1;

        return column == -1 ? 6 : column;
    }

    private (int lastRowSet, int lastColumnSet) SetDaysCurrentMonth(DateOnly lastDayCurrentMonth, int startColumnForCurrentMonth)
    {
        var currentDate = CurrentDate.GetDate();
        var column = startColumnForCurrentMonth;
        var row = 1;

        for (var day = new DateOnly(currentDate.Year, currentDate.Month, 1); day <= lastDayCurrentMonth; day = day.AddDays(1))
        {
            var layout = CreateLayout();

            if (day.Day == currentDate.Day)
            {
                layout.Background = SelectedBackgroundColor;
                layout.Content = CreateDayLabel(day.Day, SelectedDayColor);
                CurrentLayoutSelected = layout;
            }
            else
                layout.Content = CreateDayLabel(day.Day, PrimaryColor);

            if (AllowSelectDay(day))
                layout.GestureRecognizers.Add(CreateTappedCommandForDay(day, layout));
            else
                layout.Content.Opacity = 0.2;

            _view.Add(layout, column, row);

            column++;
            if (column == 7)
            {
                column = 0;
                row++;
            }
        }

        return (row, column);
    }

    private void CompleteCalendarToAvoidResize(int row, int column)
    {
        while (row < 7)
        {
            for (var index = column; index < 7; index++)
            {
                var layout = CreateLayout();
                layout.Content = CreateDayLabel(10, Colors.Transparent);

                _view.Add(layout, index, row);
            }

            row++;
            column = 0;
        }
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

    protected virtual Label CreateDayLabel(int day, Color color)
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

    private bool AllowSelectDay(DateOnly date)
    {
        if (MinimumDate.HasValue && date < MinimumDate.Value)
            return false;

        if (MaximumDate.HasValue && date > MaximumDate.Value)
            return false;

        return true;
    }

    protected virtual TapGestureRecognizer CreateTappedCommandForDay(DateOnly day, Border layout)
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
                CurrentLayoutSelected.Content = CreateDayLabel(currentDate.Day, PrimaryColor);

                CurrentDate.SetDate(day);
                CurrentLayoutSelected = layout;
            }
        };

        return tapGestureRecognizer;
    }
}
