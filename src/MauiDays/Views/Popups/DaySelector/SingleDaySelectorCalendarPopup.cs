using MauiDays.Services;
using MauiDays.Views.Components;
using Mopups.Pages;
using Mopups.Services;
using System.Globalization;

namespace MauiDays.Views.Popups.DaySelector;
public class SingleDaySelectorCalendarPopup
{
    private static SingleDaySelectorCalendarPopup _instance;

    private readonly PopupPage _popup;
    private readonly DateOnlyService CurrentDate = new ();

    private static Action<DateOnly> _callbackConfirm;

    private Color PrimaryColor { get; set; } = Colors.Black;
    private string HeaderFontFamily { get; set; } = "OpenSansSemibold";
    private string CancelFontFamily { get; set; } = "OpenSans";
    private CultureInfo Culture { get; set; }
    private string TextCancel { get; set; } = "CANCEL";
    private Color ConfirmButtonColor { get; set; } = Color.FromArgb("#47D19D");
    private string DaysOfWeekFontFamily { get; set; } = "OpenSans";
    private string DaysFontFamily { get; set; } = "OpenSans";
    private Color SelectedDayColor { get; set; } = Colors.White;
    private Color SelectedBackgroundColor { get; set; } = Colors.Black;

    private DateOnly? MinimumDate { get; set; } = null;
    private DateOnly? MaximumDate { get; set; } = null;

    private SingleDaySelectorCalendarPopup()
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

    public static SingleDaySelectorCalendarPopup Instance(Action<DateOnly> callbackConfirm)
    {
        _instance = new SingleDaySelectorCalendarPopup();

        _callbackConfirm = callbackConfirm;

        return _instance;
    }

    public SingleDaySelectorCalendarPopup CalendarBackgroundColor(Color color)
    {
        _popup.Content.BackgroundColor = color;

        return this;
    }

    public SingleDaySelectorCalendarPopup PopupBackgroundColor(Color color)
    {
        _popup.BackgroundColor = color;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetDate(DateOnly date)
    {
        CurrentDate.SetDate(date);

        return this;
    }

    public SingleDaySelectorCalendarPopup SetCulture(CultureInfo culture)
    {
        Culture = culture;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetHeaderFontFamily(string fontFamily)
    {
        HeaderFontFamily = fontFamily;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetTextCancel(string textCancel)
    {
        TextCancel = textCancel;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetCancelFontFamily(string fontFamily)
    {
        CancelFontFamily = fontFamily;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetConfirmButtonColor(Color color)
    {
        ConfirmButtonColor = color;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetDaysOfWeekFontFamily(string fontFamily)
    {
        DaysOfWeekFontFamily = fontFamily;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetDaysFontFamily(string fontFamily)
    {
        DaysFontFamily = fontFamily;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetSelectedDayColor(Color color)
    {
        SelectedDayColor = color;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;

        return this;
    }

    public SingleDaySelectorCalendarPopup SetMinimumDate(DateOnly date)
    {
        MinimumDate = date;

        return this;
    }
    public SingleDaySelectorCalendarPopup SetMaximumDate(DateOnly date)
    {
        MaximumDate = date;

        return this;
    }

    public PopupPage Build()
    {
        var header = CalendarHeader
            .Instance()
            .SetPrimaryColor(PrimaryColor)
            .SetBackgroundColor(_popup.Content.BackgroundColor)
            .SetFontFamily(HeaderFontFamily)
            .SetDate(CurrentDate)
            .SetCulture(Culture)
            .SetMinimumDate(MinimumDate)
            .SetMaximumDate(MaximumDate)
            .Build();

        var calendarContent = CalendarDaysContent
            .Instance()
            .SetPrimaryColor(PrimaryColor)
            .SetCulture(Culture)
            .SetDate(CurrentDate)
            .SetDaysOfWeekFontFamily(DaysOfWeekFontFamily)
            .SetDaysFontFamily(DaysFontFamily)
            .SetMinimumDate(MinimumDate)
            .SetMaximumDate(MaximumDate)
            .SetSelectedBackgroundColor(SelectedBackgroundColor)
            .SetSelectedDayColor(SelectedDayColor)
            .Build();

        var confirmationControls = CalendarConfirmationControls
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
