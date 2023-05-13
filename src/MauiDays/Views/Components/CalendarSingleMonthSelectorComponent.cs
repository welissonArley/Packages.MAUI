using MauiDays.Services;
using System.Globalization;

namespace MauiDays.Views.Components;
public class CalendarSingleMonthSelectorComponent : ContentView
{
    private const short AMOUNT_MONTH_ON_CALENDAR = 12;
    private const double OPACITY_FOR_DISABLE_MONTH = 0.2;

    private readonly DateOnlyService CurrentDate;

    private readonly IList<Border> _month;
    private Border CurrentLayoutSelected;
    private readonly Grid _grid;

    private CultureInfo Culture { get; set; }
    private Color PrimaryColor { get; set; }
    private Color SelectedBackgroundColor { get; set; }
    private Color SelectedMonthColor { get; set; }
    private DateOnly? MinimumDate { get; set; }
    private DateOnly? MaximumDate { get; set; }
    private string MonthsFontFamily { get; set; }

    public CalendarSingleMonthSelectorComponent(DateOnlyService currentDate)
    {
        CurrentDate = currentDate;
        _month = new List<Border>(AMOUNT_MONTH_ON_CALENDAR);

        _grid = new Grid
        {
            ColumnSpacing = 0,
            RowSpacing = 0,
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            },
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
            }
        };

        Content = _grid;

        CurrentDate.OnHeaderChangeDate(YearChanged);
    }

    public void SetMinimumDate(DateOnly date) => MinimumDate = new DateOnly(date.Year, date.Month, 1);
    public void SetMaximumDate(DateOnly date) => MaximumDate = new DateOnly(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
    public void SetColor(Color color)
    {
        PrimaryColor = color;

        foreach (var label in _month.Select(c => c.Content as Label))
            label.TextColor = color;
    }
    public void SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;
        if (CurrentLayoutSelected is not null)
            CurrentLayoutSelected.Background = color;
    }
    public void SetSelectedMonthColor(Color color)
    {
        SelectedMonthColor = color;
        if (CurrentLayoutSelected is not null)
            (CurrentLayoutSelected.Content as Label).TextColor = color;
    }
    public void SetMonthsFontFamily(string fontFamily)
    {
        MonthsFontFamily = fontFamily;

        foreach (var label in _month.Select(c => c.Content as Label))
            label.FontFamily = fontFamily;
    }

    public void Build(CultureInfo culture)
    {
        Culture = culture;

        CreateMonthsOnCalendar();
    }

    private void CreateMonthsOnCalendar()
    {
        var currentDate = CurrentDate.GetDate();

        string[] months = Culture.DateTimeFormat.AbbreviatedMonthNames;

        var row = 0;
        var column = 0;

        for (var month = 1; month <= AMOUNT_MONTH_ON_CALENDAR; month++)
        {
            var layout = CreateLayout();

            if (month == currentDate.Month)
            {
                layout.Background = SelectedBackgroundColor;
                layout.Content = CreateMonthLabel(months.ElementAt(month - 1), SelectedMonthColor);
                CurrentLayoutSelected = layout;
            }
            else
                layout.Content = CreateMonthLabel(months.ElementAt(month - 1), PrimaryColor);

            if (AllowSelectMonth(month))
                layout.GestureRecognizers.Add(CreateTappedCommandForMonth(month, layout));
            else
                layout.Content.Opacity = OPACITY_FOR_DISABLE_MONTH;

            _month.Add(layout);

            _grid.Add(layout, column, row);

            column++;
            if (column == 4)
            {
                column = 0;
                row++;
            }
        }
    }

    private static Border CreateLayout()
    {
        return new()
        {
            Background = Colors.Transparent,
            Padding = new Thickness(15, 20, 15, 20),
            Margin = 0
        };
    }
    private Label CreateMonthLabel(string month, Color color)
    {
        return new()
        {
            Text = month,
            TextColor = color,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontFamily = MonthsFontFamily
        };
    }
    private bool AllowSelectMonth(int month)
    {
        var currentDate = CurrentDate.GetDate();
        var date = new DateOnly(currentDate.Year, month, 1);

        if (MinimumDate.HasValue && date < MinimumDate.Value)
            return false;

        if (MaximumDate.HasValue && date > MaximumDate.Value)
            return false;

        return true;
    }
    private TapGestureRecognizer CreateTappedCommandForMonth(int month, Border layout)
    {
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (s, e) =>
        {
            var currentDate = CurrentDate.GetDate();

            if (month != currentDate.Month)
            {
                var newMonth = new DateOnly(currentDate.Year, month, 1);

                layout.Background = SelectedBackgroundColor;
                layout.Content = CreateMonthLabel(newMonth.ToString("MMM", Culture), SelectedMonthColor);

                CurrentLayoutSelected.Background = Colors.Transparent;
                CurrentLayoutSelected.Content = CreateMonthLabel(currentDate.ToString("MMM", Culture), PrimaryColor);

                CurrentDate.SetDate(newMonth);
                CurrentLayoutSelected = layout;
            }
        };

        return tapGestureRecognizer;
    }

    private void YearChanged()
    {
        var currentDate = CurrentDate.GetDate();

        CurrentLayoutSelected.Background = Colors.Transparent;

        if (!AllowSelectMonth(currentDate.Month))
        {
            if (MinimumDate.HasValue && currentDate.Year == MinimumDate.Value.Year)
                CurrentDate.SetDate(new DateOnly(currentDate.Year, MinimumDate.Value.Month, 1));
            else if (MaximumDate.HasValue && currentDate.Year == MaximumDate.Value.Year)
                CurrentDate.SetDate(new DateOnly(currentDate.Year, MinimumDate.Value.Month, 1));

            currentDate = CurrentDate.GetDate();
        }

        Label label;
        for (var month = 1; month <= AMOUNT_MONTH_ON_CALENDAR; month++)
        {
            var layout = _month.ElementAt(month - 1);
            layout.GestureRecognizers.Clear();
            layout.Opacity = 1;

            label = layout.Content as Label;

            if (month == currentDate.Month)
            {
                layout.Background = SelectedBackgroundColor;
                label.TextColor = SelectedMonthColor;

                CurrentLayoutSelected = layout;
            }
            else
            {
                layout.Background = Colors.Transparent;
                label.TextColor = PrimaryColor;
            }

            if (AllowSelectMonth(month))
                layout.GestureRecognizers.Add(CreateTappedCommandForMonth(month, layout));
            else
                layout.Opacity = OPACITY_FOR_DISABLE_MONTH;
        }
    }
}
