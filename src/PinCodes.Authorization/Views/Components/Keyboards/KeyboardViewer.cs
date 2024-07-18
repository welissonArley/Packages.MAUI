using PinCodes.Authorization.Extensions;
using System.Windows.Input;

namespace PinCodes.Authorization.Views.Components.Keyboards;

public sealed class KeyboardViewer : ContentPage
{
    private const ushort SPACING = 30;

    private readonly Grid _layout;
    private ICommand _callbackKeyboardCommand = default!;

    public Button ShapeViewer
    {
        get => (Button)GetValue(ShapeViewerProperty);
        set => SetValue(ShapeViewerProperty, value);
    }

    /*public Button CancelShapeViewer
    {
        get => (Button)GetValue(ShapeViewerProperty);
        set => SetValue(ShapeViewerProperty, value);
    }*/

    public Button BackspaceViewer
    {
        get => (Button)GetValue(BackspaceViewerProperty);
        set => SetValue(BackspaceViewerProperty, value);
    }

    public ushort RowSpacing
    {
        get => (ushort)GetValue(RowSpacingProperty);
        set => SetValue(RowSpacingProperty, value);
    }

    public ushort ColumnSpacing
    {
        get => (ushort)GetValue(ColumnSpacingProperty);
        set => SetValue(ColumnSpacingProperty, value);
    }

    public static readonly BindableProperty ShapeViewerProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Button), typeof(KeyboardViewer), GetDefaultKeyboardViewShape(), propertyChanged: OnShapePropertyChanged);
    public static readonly BindableProperty BackspaceViewerProperty = BindableProperty.Create(nameof(BackspaceViewer), typeof(Button), typeof(KeyboardViewer), GetDefaultBackspaceOptionViewShape(), propertyChanged: OnBackspaceViewerPropertyChanged);
    public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(ushort), typeof(KeyboardViewer), SPACING, propertyChanged: OnRowSpacingProperty);
    public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(ushort), typeof(KeyboardViewer), SPACING, propertyChanged: OnColumnSpacingProperty);

    private static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).CreateLayout();
    private static void OnBackspaceViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).CreateLayout();
    private static void OnRowSpacingProperty(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).SetRowColumnSpacing();
    private static void OnColumnSpacingProperty(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).SetColumnSpacing();

    public KeyboardViewer()
	{
        _layout = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = ColumnSpacing,
            RowSpacing = RowSpacing,
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition { Width = ShapeViewer.WidthRequest }, 3).ToArray()),
            RowDefinitions = new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition { Height = ShapeViewer.HeightRequest }, 4).ToArray())
        };

        CreateLayout();
    }

    public void SetCommandWhenUserPressButtonOnKeyboard(ICommand callbackCommand) => _callbackKeyboardCommand = callbackCommand;

    private void CreateLayout()
    {
        _layout.Clear();

        _layout.Add(view: AddButtonWithCommand(1), column: 0, row: 0);
        _layout.Add(view: AddButtonWithCommand(2), column: 1, row: 0);
        _layout.Add(view: AddButtonWithCommand(3), column: 2, row: 0);
        _layout.Add(view: AddButtonWithCommand(4), column: 0, row: 1);
        _layout.Add(view: AddButtonWithCommand(5), column: 1, row: 1);
        _layout.Add(view: AddButtonWithCommand(6), column: 2, row: 1);
        _layout.Add(view: AddButtonWithCommand(7), column: 0, row: 2);
        _layout.Add(view: AddButtonWithCommand(8), column: 1, row: 2);
        _layout.Add(view: AddButtonWithCommand(9), column: 2, row: 2);
        //_layout.Add(view: CreateCancelOption(), column: 0, row: 3);
        _layout.Add(view: AddButtonWithCommand(0), column: 1, row: 3);
        _layout.Add(view: CreateBackspaceOption(), column: 2, row: 3);

        Content = _layout;
    }

    private Button AddButtonWithCommand(int value)
    {
        var button = ShapeViewer.Clone();
        button.Text = value.ToString();
        button.Command = new Command(() => { _callbackKeyboardCommand?.Execute(value); });

        return button;
    }

    private void SetColumnSpacing()
    {
        if (ColumnSpacing <= 0)
            ColumnSpacing = SPACING;

        _layout.ColumnSpacing = ColumnSpacing;
    }

    private void SetRowColumnSpacing()
    {
        if (RowSpacing <= 0)
            RowSpacing = SPACING;

        _layout.RowSpacing = RowSpacing;
    }

    private Button CreateBackspaceOption()
    {
        var button = BackspaceViewer.Clone();
        button.Command = new Command(() => { _callbackKeyboardCommand?.Execute(-1); });

        return button;
    }

    private static Button GetDefaultKeyboardViewShape()
    {
        return new Button
        {
            FontSize = 32,
            FontAttributes = FontAttributes.Bold,
            WidthRequest = 80,
            HeightRequest = 80,
            CornerRadius = 15,
            TextColor = Colors.Black,
            BackgroundColor = Color.FromArgb("#BEBEBE"),
        };
    }

    private static Button GetDefaultBackspaceOptionViewShape()
    {
        return new Button
        {
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            WidthRequest = 80,
            HeightRequest = 80,
            TextColor = Colors.Black,
            Text = "⌫",
            BackgroundColor = Colors.Transparent,
        };
    }
}