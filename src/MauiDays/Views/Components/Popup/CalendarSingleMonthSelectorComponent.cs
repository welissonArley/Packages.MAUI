using MauiDays.Services;
using System.Globalization;

namespace MauiDays.Views.Components.Popup;
public class CalendarSingleMonthSelectorComponent
{
    private static CalendarSingleMonthSelectorComponent _instance;

    private readonly Grid _view;

    private Border CurrentLayoutSelected;

    private DateOnlyService CurrentDate;
    private CultureInfo Culture { get; set; }
    private Color PrimaryColor { get; set; }
    private string MonthsFontFamily { get; set; }
    private Color SelectedMonthColor { get; set; }
    private Color SelectedBackgroundColor { get; set; }

    private DateOnly? MinimumDate { get; set; }
    private DateOnly? MaximumDate { get; set; }

    private CalendarSingleMonthSelectorComponent()
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
                    new ColumnDefinition()
                },
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                }
            };

            Culture = CultureInfo.CurrentCulture;
        }
    }

    public static CalendarSingleMonthSelectorComponent Instance()
    {
        _instance = new CalendarSingleMonthSelectorComponent();

        return _instance;
    }

    public CalendarSingleMonthSelectorComponent SetDate(DateOnlyService date)
    {
        CurrentDate = date;

        return this;
    }

    public CalendarSingleMonthSelectorComponent SetCulture(CultureInfo culture)
    {
        Culture = culture;

        return this;
    }

    public CalendarSingleMonthSelectorComponent SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public CalendarSingleMonthSelectorComponent SetSelectedMonthColor(Color color)
    {
        SelectedMonthColor = color;

        return this;
    }

    public CalendarSingleMonthSelectorComponent SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;

        return this;
    }

    public CalendarSingleMonthSelectorComponent SetMonthsFontFamily(string fontFamily)
    {
        MonthsFontFamily = fontFamily;

        return this;
    }

    public CalendarSingleMonthSelectorComponent SetMinimumDate(DateOnly? date)
    {
        if (date.HasValue)
            MinimumDate = new DateOnly(date.Value.Year, date.Value.Month, 1);

        return this;
    }
    public CalendarSingleMonthSelectorComponent SetMaximumDate(DateOnly? date)
    {
        if (date.HasValue)
            MaximumDate = new DateOnly(date.Value.Year, date.Value.Month, 1);

        return this;
    }

    public IView Build()
    {
        CurrentDate.OnChangedYear(() =>
        {
            var currentDate = CurrentDate.GetDate();
            if (!AllowSelectMonth(currentDate.Month))
            {
                if (MinimumDate.HasValue && currentDate.Year == MinimumDate.Value.Year)
                    CurrentDate.SetDate(new DateOnly(currentDate.Year, MinimumDate.Value.Month, 1));
                else if (MaximumDate.HasValue && currentDate.Year == MaximumDate.Value.Year)
                    CurrentDate.SetDate(new DateOnly(currentDate.Year, MinimumDate.Value.Month, 1));
            }

            FillCalendar();
        });

        FillCalendar();

        return _view;
    }

    private void FillCalendar()
    {
        _view.Clear();

        var currentDate = CurrentDate.GetDate();

        string[] months = Culture.DateTimeFormat.AbbreviatedMonthNames;

        var row = 0;
        var column = 0;

        for (var month = 1; month <= 12; month++)
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
                layout.Content.Opacity = 0.2;

            _view.Add(layout, column, row);

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
}
