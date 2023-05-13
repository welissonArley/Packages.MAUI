using MauiDays.Services;
using System.Globalization;

namespace MauiDays.Views.Components;
public class CalendarHeaderComponent : ContentView
{
    private readonly Button _previousButton;
    private readonly Button _nextButton;
    private readonly Label _headerLabel;

    private readonly DateOnlyService CurrentDate;
    private readonly bool MonthCalendar;

    private CultureInfo Culture { get; set; }
    private DateOnly? MinimumDate { get; set; }
    private DateOnly? MaximumDate { get; set; }

    public CalendarHeaderComponent(DateOnlyService currentDate, bool monthCalendar)
    {
        CurrentDate = currentDate;
        MonthCalendar = monthCalendar;
        Culture = CultureInfo.CurrentCulture;

        _previousButton = CreateButton("◀", new Thickness(0, -5, 2, 0));
        _nextButton = CreateButton("▶", new Thickness(2, -5, 0, 0));
        _headerLabel = CreateLabelShowDate();

        _previousButton.Clicked += (_, _) =>
        {
            HeaderButtonNextPreviousCommand(-1);

            ButtonSelectedBase();

            _headerLabel.Text = GetTextForHeader();
        };

        _nextButton.Clicked += (_, _) =>
        {
            HeaderButtonNextPreviousCommand(1);

            ButtonSelectedBase();

            _headerLabel.Text = GetTextForHeader();
        };

        var gridContent = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(width: 36),
                new ColumnDefinition(),
                new ColumnDefinition(width: 36)
            }
        };

        gridContent.Add(_previousButton, column: 0);
        gridContent.Add(_headerLabel, column: 1);
        gridContent.Add(_nextButton, column: 2);

        Content = gridContent;
    }

    public void SetCulture(CultureInfo culture)
    {
        Culture = culture;

        _headerLabel.Text = GetTextForHeader();
    }

    public void UpdateDate() => _headerLabel.Text = GetTextForHeader();
    public void SetFontFamily(string fontFamily) => _headerLabel.FontFamily = fontFamily;
    public void SetColor(Color color)
    {
        _headerLabel.TextColor = color;

        _previousButton.TextColor = color;
        _previousButton.BorderColor = color;

        _nextButton.TextColor = color;
        _nextButton.BorderColor = color;
    }
    public void SetMinimumDate(DateOnly date)
    {
        MinimumDate = date;
        CheckMinDate();
    }
    public void SetMaximumDate(DateOnly date)
    {
        MaximumDate = date;
        CheckMaxDate();
    }

    private Label CreateLabelShowDate()
    {
        var labelShowingDate = new Label
        {
            FontSize = 20,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Text = GetTextForHeader()
        };

        return labelShowingDate;
    }

    private static Button CreateButton(string symbol, Thickness thickness)
    {
        return new()
        {
            Text = symbol,
            FontSize = 15,
            CornerRadius = 18,
            HeightRequest = 36,
            WidthRequest = 36,
            BorderWidth = 2,
            BackgroundColor = Colors.Transparent,
            Padding = thickness
        };
    }

    private string GetTextForHeader() => CurrentDate.GetDate().ToString(MonthCalendar ? "yyyy" : "MMMM yyyy", Culture);

    private void HeaderButtonNextPreviousCommand(int timeToAdd)
    {
        if (MonthCalendar)
            CurrentDate.AddYears(timeToAdd);
        else
            CurrentDate.AddMonths(timeToAdd);
    }

    private void ButtonSelectedBase()
    {
        _headerLabel.Text = GetTextForHeader();

        CheckMinDate();
        CheckMaxDate();
    }

    private void CheckMinDate()
    {
        if (!MinimumDate.HasValue)
            return;

        if (MonthCalendar)
            SetVisibilityButtonComparingDateMonthsCalendar(MinimumDate.Value, _previousButton);
        else
        {
            var minMonth = new DateOnly(MinimumDate.Value.Year, MinimumDate.Value.Month, 1);

            SetVisibilityButtonComparingDateDaysCalendar(minMonth, _previousButton);
        }
    }
    private void CheckMaxDate()
    {
        if (!MaximumDate.HasValue)
            return;

        if (MonthCalendar)
            SetVisibilityButtonComparingDateMonthsCalendar(MaximumDate.Value, _nextButton);
        else
        {
            var maxMonth = new DateOnly(MaximumDate.Value.Year, MaximumDate.Value.Month, 1);

            SetVisibilityButtonComparingDateDaysCalendar(maxMonth, _nextButton);
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