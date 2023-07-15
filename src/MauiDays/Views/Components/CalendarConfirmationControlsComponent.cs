using MauiDays.Services;

namespace MauiDays.Views.Components;
public class CalendarConfirmationControlsComponent : ContentView
{
    private const string TEXT_CANCEL = "Cancel";

    private readonly DateOnlyService CurrentDate;

    private readonly Button _cancelButton;
    private readonly Button _confirmButton;

    private Action _callbackCancel;
    private Action<DateOnly> _callbackConfirm;

    public CalendarConfirmationControlsComponent(DateOnlyService currentDate)
    {
        CurrentDate = currentDate;

        var view = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(width: 50)
            }
        };

        _cancelButton = CreateCancelButton();
        _confirmButton = CreateConfirmButton();

        _cancelButton.Clicked += (_, _) =>
        {
            _callbackCancel?.Invoke();
        };

        _confirmButton.Clicked += (_, _) =>
        {
            _callbackConfirm?.Invoke(CurrentDate.GetDate());
        };

        view.Add(_cancelButton, column: 0);
        view.Add(_confirmButton, column: 1);

        Content = view;
    }

    public void SetPrimaryColor(Color color) => _cancelButton.TextColor = color;
    public void SetConfirmButtonColor(Color color) => _confirmButton.BackgroundColor = color;
    public void SetConfirmButtonTextColor(Color color) => _confirmButton.TextColor = color;
    public void SetTextCancel(string text) => _cancelButton.Text = $"{SymbolCancel()} {text}";
    public void SetCallbacks(Action cancel, Action<DateOnly> confirm)
    {
        _callbackCancel = cancel;
        _callbackConfirm = confirm;
    }
    public void SetCancelFontFamily(string fontFamily) => _cancelButton.FontFamily = fontFamily;

    private static Button CreateCancelButton()
    {
        return new Button
        {
            Text = $"{SymbolCancel()} {TEXT_CANCEL}",
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Start,
            BackgroundColor = Colors.Transparent
        };
    }
    private static Button CreateConfirmButton()
    {
        return new Button
        {
            Text = $"{SymbolConfirm()}",
            FontSize = 20,
            CornerRadius = 25,
            HeightRequest = 50,
            WidthRequest = 50
        };
    }

    private static string SymbolCancel() => "✖";
    private static string SymbolConfirm() => "✔";
}
