namespace MauiDays.Views.Components;
public class CalendarConfirmationControls
{
    private static CalendarConfirmationControls _instance;

    private readonly Grid _view;
    private Action _callbackCancel = null;
    private Action _callbackConfirm = null;

    private Color PrimaryColor { get; set; } = Colors.Black;
    private Color BackgroundColor { get; set; } = Colors.White;
    private Color ConfirmButtonColor { get; set; } = Color.FromArgb("#47D19D");
    private string FontFamily { get; set; } = "OpenSansSemibold";
    private string TextCancel { get; set; } = "CANCEL";

    private CalendarConfirmationControls()
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

    public static CalendarConfirmationControls Instance()
    {
        _instance = new CalendarConfirmationControls();

        return _instance;
    }

    public CalendarConfirmationControls SetPrimaryColor(Color color)
    {
        PrimaryColor = color;

        return this;
    }

    public CalendarConfirmationControls SetBackgroundColor(Color color)
    {
        BackgroundColor = color;

        return this;
    }

    public CalendarConfirmationControls SetFontFamily(string fontFamily)
    {
        FontFamily = fontFamily;

        return this;
    }

    public CalendarConfirmationControls SetTextCancel(string textCancel)
    {
        TextCancel = textCancel;

        return this;
    }

    public CalendarConfirmationControls SetConfirmButtonColor(Color color)
    {
        ConfirmButtonColor = color;

        return this;
    }

    public CalendarConfirmationControls SetCallbacks(Action callbackCancel, Action callbackConfirm)
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
            TextColor = PrimaryColor,
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
