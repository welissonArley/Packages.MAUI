using MauiDays.Services;
using MauiDays.Views.Components;
using Mopups.Pages;
using System.Globalization;

namespace MauiDays.Views.Popups;
public abstract class BaseCalendarPopup : PopupPage
{
    private static Action<DateOnly> _callbackConfirm;
    private static Action _callbackCancel;
    private Color CalendarBackgroundColor;
    private Color ConfirmButtonColor;
    private Color ConfirmButtonTextColor;
    private string HeaderFontFamily;
    private string CancelText;
    private string CancelFontFamily;

    protected string LabelFontFamily;
    protected Color SelectedLabelColor;
    protected Color SelectedBackgroundColor;
    protected Color PrimaryColor;
    protected DateOnly? MinimumDate { get; set; }
    protected DateOnly? MaximumDate { get; set; }
    protected CultureInfo Culture;
    protected readonly DateOnlyService _currentDate;

    public BaseCalendarPopup(Action callbackCancel, Action<DateOnly> callbackConfirm)
    {
        _callbackConfirm = callbackConfirm;
        _callbackCancel = callbackCancel;

        CloseWhenBackgroundIsClicked = true;

        _currentDate = new DateOnlyService();
    }
    public BaseCalendarPopup SetCalendarBackgroundColor(Color color)
    {
        CalendarBackgroundColor = color;

        return this;
    }
    public BaseCalendarPopup SetPopupBackgroundColor(Color color)
    {
        BackgroundColor = color;

        return this;
    }
    public BaseCalendarPopup SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }
    public BaseCalendarPopup SetDate(DateOnly date)
    {
        _currentDate.SetDate(date);
        return this;
    }
    public BaseCalendarPopup SetHeaderFontFamily(string fontFamily)
    {
        HeaderFontFamily = fontFamily;
        return this;
    }
    public BaseCalendarPopup SetMinimumDate(DateOnly date)
    {
        MinimumDate = date;
        return this;
    }
    public BaseCalendarPopup SetMaximumDate(DateOnly date)
    {
        MaximumDate = date;
        return this;
    }
    public BaseCalendarPopup DontCloseWhenBackgroundIsClicked()
    {
        CloseWhenBackgroundIsClicked = false;
        return this;
    }
    public BaseCalendarPopup SetConfirmButtonColor(Color color)
    {
        ConfirmButtonColor = color;
        return this;
    }
    public BaseCalendarPopup SetConfirmButtonTextColor(Color color)
    {
        ConfirmButtonTextColor = color;
        return this;
    }
    public BaseCalendarPopup SetTextCancel(string text)
    {
        CancelText = text;
        return this;
    }
    public BaseCalendarPopup SetCancelFontFamily(string fontFamily)
    {
        CancelFontFamily = fontFamily;
        return this;
    }
    public BaseCalendarPopup SetCulture(CultureInfo culture)
    {
        Culture = culture;
        return this;
    }
    public BaseCalendarPopup SetSelectedBackgroundColor(Color color)
    {
        SelectedBackgroundColor = color;
        return this;
    }
    public BaseCalendarPopup SetSelectedLabelColor(Color color)
    {
        SelectedLabelColor = color;
        return this;
    }
    public BaseCalendarPopup SetLabelFontFamily(string fontFamily)
    {
        LabelFontFamily = fontFamily;
        return this;
    }

    public BaseCalendarPopup Build()
    {
        var headerComponent = new CalendarHeaderComponent(_currentDate, monthCalendar: IsMonthCalendar());
        headerComponent.SetColor(PrimaryColor);
        headerComponent.SetFontFamily(HeaderFontFamily);
        headerComponent.SetCulture(Culture);

        if (MinimumDate.HasValue)
            headerComponent.SetMinimumDate(MinimumDate.Value);

        if (MaximumDate.HasValue)
            headerComponent.SetMaximumDate(MaximumDate.Value);

        var confimationControl = new CalendarConfirmationControlsComponent(_currentDate);
        confimationControl.SetPrimaryColor(PrimaryColor);
        confimationControl.SetConfirmButtonColor(ConfirmButtonColor);
        confimationControl.SetConfirmButtonTextColor(ConfirmButtonTextColor);
        confimationControl.SetCallbacks(_callbackCancel, _callbackConfirm);
        confimationControl.SetCancelFontFamily(CancelFontFamily);

        if (!string.IsNullOrEmpty(CancelText))
            confimationControl.SetTextCancel(CancelText);

        Content = new VerticalStackLayout
        {
            Spacing = 30,
            Margin = 30,
            Padding = 30,
            BackgroundColor = CalendarBackgroundColor,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                headerComponent,
                CreateCalendar(),
                confimationControl
            }
        };

        return this;
    }

    protected abstract IView CreateCalendar();
    protected abstract bool IsMonthCalendar();
}
