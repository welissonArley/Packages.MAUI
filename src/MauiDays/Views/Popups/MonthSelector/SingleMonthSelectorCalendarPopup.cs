using MauiDays.Services;
using MauiDays.Views.Components.Popup;
using Mopups.Pages;
using Mopups.Services;
using System.Globalization;

namespace MauiDays.Views.Popups.MonthSelector;
public class SingleMonthSelectorCalendarPopup
{
    private static SingleMonthSelectorCalendarPopup _instance;

    private readonly PopupPage _popup;
    private readonly DateOnlyService CurrentDate = new();

    private static Action<DateOnly> _callbackConfirm;

    private Color PrimaryColor { get; set; } = Colors.Black;
    private string HeaderFontFamily { get; set; } = "OpenSansSemibold";
    private string CancelFontFamily { get; set; } = "OpenSans";
    private CultureInfo Culture { get; set; }
    private string TextCancel { get; set; } = "CANCEL";
    private Color ConfirmButtonColor { get; set; } = Color.FromArgb("#47D19D");
    private string MonthFontFamily { get; set; } = "OpenSans";
    private Color SelectedMonthColor { get; set; } = Colors.White;
    private Color SelectedBackgroundColor { get; set; } = Colors.Black;

    private DateOnly? MinimumDate { get; set; } = null;
    private DateOnly? MaximumDate { get; set; } = null;

    private SingleMonthSelectorCalendarPopup()
    {
        if (_popup is null)
        {
            _popup = new PopupPage
            {
                CloseWhenBackgroundIsClicked = true,
                BackgroundColor = Color.FromArgb("#F2A1A1A1"),
                Content = new VerticalStackLayout
                {
                    Spacing = 30,
                    Margin = 30,
                    Padding = 30,
                    BackgroundColor = Colors.White,
                    VerticalOptions = LayoutOptions.Center
                }
            };

            Culture = CultureInfo.CurrentCulture;
        }
    }

    public static SingleMonthSelectorCalendarPopup Instance(Action<DateOnly> callbackConfirm)
    {
        _instance = new SingleMonthSelectorCalendarPopup();

        _callbackConfirm = callbackConfirm;

        return _instance;
    }

    public SingleMonthSelectorCalendarPopup CalendarBackgroundColor(Color color)
    {
        _popup.Content.BackgroundColor = color;

        return this;
    }

    public SingleMonthSelectorCalendarPopup PopupBackgroundColor(Color color)
    {
        _popup.BackgroundColor = color;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetDate(DateOnly date)
    {
        CurrentDate.SetDate(date);

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetCulture(CultureInfo culture)
    {
        Culture = culture;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetHeaderFontFamily(string fontFamily)
    {
        HeaderFontFamily = fontFamily;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetTextCancel(string textCancel)
    {
        TextCancel = textCancel;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetCancelFontFamily(string fontFamily)
    {
        CancelFontFamily = fontFamily;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetConfirmButtonColor(Color color)
    {
        ConfirmButtonColor = color;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetMonthFontFamily(string fontFamily)
    {
        MonthFontFamily = fontFamily;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetSelectedMonthColor(Color color)
    {
        SelectedMonthColor = color;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;

        return this;
    }

    public SingleMonthSelectorCalendarPopup SetMinimumDate(DateOnly date)
    {
        MinimumDate = date;

        return this;
    }
    public SingleMonthSelectorCalendarPopup SetMaximumDate(DateOnly date)
    {
        MaximumDate = date;

        return this;
    }

    public PopupPage Build()
    {
        var header = CalendarHeaderComponent
            .Instance()
            .SetPrimaryColor(PrimaryColor)
            .SetBackgroundColor(_popup.Content.BackgroundColor)
            .SetFontFamily(HeaderFontFamily)
            .SetDate(CurrentDate)
            .SetCulture(Culture)
            .SetMinimumDate(MinimumDate)
            .SetMaximumDate(MaximumDate)
            .IsMonthCalendar()
            .Build();

        var calendarContent = CalendarSingleMonthSelectorComponent
            .Instance()
            .SetPrimaryColor(PrimaryColor)
            .SetCulture(Culture)
            .SetDate(CurrentDate)
            .SetMonthsFontFamily(MonthFontFamily)
            .SetMinimumDate(MinimumDate)
            .SetMaximumDate(MaximumDate)
            .SetSelectedBackgroundColor(SelectedBackgroundColor)
            .SetSelectedMonthColor(SelectedMonthColor)
            .Build();

        var confirmationControls = CalendarConfirmationControlsComponent
            .Instance()
            .SetPrimaryColor(PrimaryColor)
            .SetBackgroundColor(_popup.Content.BackgroundColor)
            .SetFontFamily(CancelFontFamily)
            .SetTextCancel(TextCancel)
            .SetConfirmButtonColor(ConfirmButtonColor)
            .SetCallbacks(callbackCancel: async () => { await MopupService.Instance.PopAsync(); }, callbackConfirm: async () => { await MopupService.Instance.PopAsync(); _callbackConfirm(CurrentDate.GetDate()); })
            .Build();

        var stackLayout = (VerticalStackLayout)_popup.Content;
        stackLayout.Children.Clear();

        stackLayout.Children.Add(header);
        stackLayout.Children.Add(calendarContent);
        stackLayout.Children.Add(confirmationControls);

        return _popup;
    }
}
