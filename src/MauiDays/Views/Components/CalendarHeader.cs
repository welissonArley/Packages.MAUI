using MauiDays.Services;
using System.Globalization;

namespace MauiDays.Views.Components;
public class CalendarHeader
{
    private static CalendarHeader _instance;

    private readonly Grid _view;

    private DateOnlyService CurrentDate;
    private Color PrimaryColor { get; set; }
    private Color BackgroundColor { get; set; }
    private string FontFamily { get; set; }
    private CultureInfo Culture { get; set; }
    private bool MonthCalendar { get; set; }

    private DateOnly? MinimumDate { get; set; }
    private DateOnly? MaximumDate { get; set; }

    private CalendarHeader()
    {
        if (_view is null)
        {
            _view = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(width: 36),
                    new ColumnDefinition(),
                    new ColumnDefinition(width: 36)
                }
            };

            Culture = CultureInfo.CurrentCulture;
        }
    }

    public static CalendarHeader Instance()
    {
        _instance = new CalendarHeader();

        return _instance;
    }

    public CalendarHeader SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public CalendarHeader SetBackgroundColor(Color color)
    {
        BackgroundColor = color;

        return this;
    }

    public CalendarHeader SetFontFamily(string fontFamily)
    {
        FontFamily = fontFamily;

        return this;
    }

    public CalendarHeader SetDate(DateOnlyService date)
    {
        CurrentDate = date;

        return this;
    }

    public CalendarHeader SetCulture(CultureInfo culture)
    {
        Culture = culture;

        return this;
    }

    public CalendarHeader SetMinimumDate(DateOnly? date)
    {
        MinimumDate = date;

        return this;
    }
    public CalendarHeader SetMaximumDate(DateOnly? date)
    {
        MaximumDate = date;

        return this;
    }
    public CalendarHeader IsMonthCalendar()
    {
        MonthCalendar = true;

        return this;
    }

    public IView Build()
    {
        var currentDate = CurrentDate.GetDate();

        var previousDateButton = CreateButton("◀", new Thickness(0, -5, 2, 0));
        var nextDateButton = CreateButton("▶", new Thickness(2, -5, 0, 0));

        var labelShowingDate = CreateLabelShowDate(currentDate);

        previousDateButton.Clicked += (_, _) =>
        {
            if (MonthCalendar)
                CurrentDate.AddYears(-1);
            else
                CurrentDate.AddMonths(-1);

            ButtonSelectedBase(labelShowingDate, previousDateButton, nextDateButton);
        };

        nextDateButton.Clicked += (_, _) =>
        {
            if (MonthCalendar)
                CurrentDate.AddYears(1);
            else
                CurrentDate.AddMonths(1);

            ButtonSelectedBase(labelShowingDate, previousDateButton, nextDateButton);
        };

        CurrentDate.OnChoseDayInNextOrPreviousMonth(() => labelShowingDate.UpdateText(CurrentDate.GetDate(), Culture, MonthCalendar));

        CheckMinDate(previousDateButton);
        CheckMaxDate(nextDateButton);

        _view.Add(previousDateButton, column: 0);
        _view.Add(labelShowingDate, column: 1);
        _view.Add(nextDateButton, column: 2);

        return _view;
    }

    private Button CreateButton(string symbol, Thickness thickness)
    {
        return new ()
        {
            Text = symbol,
            TextColor = PrimaryColor,
            FontSize = 15,
            CornerRadius = 18,
            HeightRequest = 36,
            WidthRequest = 36,
            BorderWidth = 2,
            BorderColor = PrimaryColor,
            BackgroundColor = BackgroundColor,
            Padding = thickness
        };
    }
    private void ButtonSelectedBase(Label labelShowingDate, Button previousDateButton, Button nextDateButton)
    {
        labelShowingDate.UpdateText(CurrentDate.GetDate(), Culture, MonthCalendar);
        
        CheckMinDate(previousDateButton);
        CheckMaxDate(nextDateButton);
    }

    private Label CreateLabelShowDate(DateOnly date)
    {
        var labelShowingDate = new Label
        {
            TextColor = PrimaryColor,
            FontSize = 20,
            FontFamily = FontFamily,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        labelShowingDate.UpdateText(date, Culture, MonthCalendar);

        return labelShowingDate;
    }

    private void CheckMinDate(Button button)
    {
        if (!MinimumDate.HasValue)
            return;

        if (MonthCalendar)
            SetVisibilityButtonComparingDateMonthsCalendar(MinimumDate.Value, button);
        else
        {
            var minMonth = new DateOnly(MinimumDate.Value.Year, MinimumDate.Value.Month, 1);

            SetVisibilityButtonComparingDateDaysCalendar(minMonth, button);
        }
    }
    private void CheckMaxDate(Button button)
    {
        if (!MaximumDate.HasValue)
            return;

        if (MonthCalendar)
            SetVisibilityButtonComparingDateMonthsCalendar(MaximumDate.Value, button);
        else
        {
            var maxMonth = new DateOnly(MaximumDate.Value.Year, MaximumDate.Value.Month, 1);

            SetVisibilityButtonComparingDateDaysCalendar(maxMonth, button);
        }
    }
    private void SetVisibilityButtonComparingDateDaysCalendar(DateOnly dateToCompare, Button button)
    {
        var currentDate = CurrentDate.GetDate();
        var currentMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);

        if (currentMonth == dateToCompare)
            button.IsVisible = false;
        else
            button.IsVisible = true;
    }
    private void SetVisibilityButtonComparingDateMonthsCalendar(DateOnly dateToCompare, Button button)
    {
        var currentDate = CurrentDate.GetDate();

        if (currentDate.Year == dateToCompare.Year)
            button.IsVisible = false;
        else
            button.IsVisible = true;
    }
}

public static class LabelExtention
{
    public static void UpdateText(this Label label, DateOnly date, CultureInfo culture, bool monthCalendar)
    {
        label.Text = date.ToString(monthCalendar ? "yyyy" : "MMMM yyyy", culture);
    }
}