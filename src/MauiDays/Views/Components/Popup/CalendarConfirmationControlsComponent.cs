namespace MauiDays.Views.Components.Popup;
public class CalendarConfirmationControlsComponent
{
    private static CalendarConfirmationControlsComponent _instance;

    private readonly Grid _view;
    private Action _callbackCancel = null;
    private Action _callbackConfirm = null;

    private Color PrimaryColor { get; set; }
    private Color BackgroundColor { get; set; }
    private Color ConfirmButtonColor { get; set; }
    private string FontFamily { get; set; }
    private string TextCancel { get; set; }

    private CalendarConfirmationControlsComponent()
    {
        if (_view is null)
        {
            _view = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(width: 50)
                }
            };
        }
    }

    public static CalendarConfirmationControlsComponent Instance()
    {
        _instance = new CalendarConfirmationControlsComponent();

        return _instance;
    }

    public CalendarConfirmationControlsComponent SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public CalendarConfirmationControlsComponent SetBackgroundColor(Color color)
    {
        BackgroundColor = color;

        return this;
    }

    public CalendarConfirmationControlsComponent SetFontFamily(string fontFamily)
    {
        FontFamily = fontFamily;

        return this;
    }

    public CalendarConfirmationControlsComponent SetTextCancel(string textCancel)
    {
        TextCancel = textCancel;

        return this;
    }

    public CalendarConfirmationControlsComponent SetConfirmButtonColor(Color color)
    {
        ConfirmButtonColor = color;

        return this;
    }

    public CalendarConfirmationControlsComponent SetCallbacks(Action callbackCancel, Action callbackConfirm)
    {
        _callbackCancel = callbackCancel;
        _callbackConfirm = callbackConfirm;

        return this;
    }

    public IView Build()
    {
        var cancelButton = new Button
        {
            Text = $"✖ {TextCancel}",
            TextColor = PrimaryColor,
            FontSize = 16,
            FontFamily = FontFamily,
            BackgroundColor = BackgroundColor,
            HorizontalOptions = LayoutOptions.Start,
        };

        var okButton = new Button
        {
            Text = "✔",
            TextColor = BackgroundColor,
            FontSize = 20,
            CornerRadius = 25,
            HeightRequest = 50,
            WidthRequest = 50,
            BackgroundColor = ConfirmButtonColor
        };

        cancelButton.Clicked += (_, _) =>
        {
            if (_callbackCancel is not null)
                _callbackCancel();
        };

        okButton.Clicked += (_, _) =>
        {
            if (_callbackConfirm is not null)
                _callbackConfirm();
        };

        _view.Add(cancelButton, column: 0);
        _view.Add(okButton, column: 1);

        return _view;
    }
}
