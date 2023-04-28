using MauiDays.Services;
using System.Globalization;

namespace MauiDays.Views.Components;
public class CalendarDaysContent
{
    private static CalendarDaysContent _instance;

    private readonly Grid _view;

    private Border CurrentLayoutSelected;

    private DateOnlyService CurrentDate;
    private CultureInfo Culture { get; set; }
    private Color PrimaryColor { get; set; }
    private string DaysOfWeekFontFamily { get; set; }
    private string DaysFontFamily { get; set; }
    private Color SelectedDayColor { get; set; }
    private Color SelectedBackgroundColor { get; set; }

    private DateOnly? MinimumDate { get; set; }
    private DateOnly? MaximumDate { get; set; }

    private CalendarDaysContent()
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
    }

    public static CalendarDaysContent Instance()
    {
        _instance = new CalendarDaysContent();

        return _instance;
    }

    public CalendarDaysContent SetDate(DateOnlyService date)
    {
        CurrentDate = date;

        return this;
    }

    public CalendarDaysContent SetCulture(CultureInfo culture)
    {
        Culture = culture;

        return this;
    }

    public CalendarDaysContent SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public CalendarDaysContent SetDaysOfWeekFontFamily(string fontFamily)
    {
        DaysOfWeekFontFamily = fontFamily;

        return this;
    }

    public CalendarDaysContent SetDaysFontFamily(string fontFamily)
    {
        DaysFontFamily = fontFamily;

        return this;
    }

    public CalendarDaysContent SetMinimumDate(DateOnly? date)
    {
        MinimumDate = date;

        return this;
    }
    public CalendarDaysContent SetMaximumDate(DateOnly? date)
    {
        MaximumDate = date;

        return this;
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

    private void SetDaysOnCalendar()
    {
        var currentDate = CurrentDate.GetDate();
        var firstDayCurrentMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
        var lastDayCurrentMonth = firstDayCurrentMonth.AddMonths(1).AddDays(-1);

        var startColumnForCurrentMonth = CalculateStartColumn(firstDayCurrentMonth);

        (int lastRowSet, int lastColumnSet) = SetDaysCurrentMonth(lastDayCurrentMonth, startColumnForCurrentMonth);
        CompleteCalendarToAvoidResize(lastRowSet, lastColumnSet);
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

    private void SetDaysOfTheWeekHeader()
    {
        string[] daysOfWeek = Culture.DateTimeFormat.AbbreviatedDayNames;

        for (var index = 0; index < daysOfWeek.Length; index++)
            _view.Add(CreateLabelForDayOfTheWeek(daysOfWeek.ElementAt(index)), column: index, row: 0);
    }

    private Label CreateLabelForDayOfTheWeek(string day)
    {
        return new ()
        {
            Text = day,
            TextColor = PrimaryColor,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 12,
            FontFamily = DaysOfWeekFontFamily
        };
    }

    private int CalculateStartColumn(DateOnly date)
    {
        var dayofWeek = (int)date.DayOfWeek;

        var column = Culture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday ? dayofWeek : dayofWeek - 1;

        return column == -1 ? 6 : column;
    }

    private static Border CreateLayout()
    {
        return new ()
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

    private bool AllowSelectDay(DateOnly date)
    {
        if (MinimumDate.HasValue && date < MinimumDate.Value)
            return false;

        if (MaximumDate.HasValue && date > MaximumDate.Value)
            return false;

        return true;
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
                layout.Content = CreateDayLabel(day.Day, SelectedDayColor);

                CurrentLayoutSelected.Background = Colors.Transparent;
                CurrentLayoutSelected.Content = CreateDayLabel(currentDate.Day, PrimaryColor);

                CurrentDate.SetDate(day);
                CurrentLayoutSelected = layout;
            }
        };

        return tapGestureRecognizer;
    }
}