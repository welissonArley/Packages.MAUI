using Shared.Helpers.Extensions;
using System.Windows.Input;

namespace MauiCode.Views.Components.Keyboards.Base;

public abstract class BaseKeyboardViewer : ContentView
{
    protected const uint SHAPE_SIZE = 80;
    protected const uint FONT_SIZE = 32;
    protected const uint CANCEL_TEXT_FONT_SIZE = 18;
    protected const string CANCEL_TEXT = "Cancel";

    private ICommand _callbackKeyboardCommand;

    public uint Size
    {
        get { return (uint)GetValue(SizeProperty); }
        set { SetValue(SizeProperty, value); }
    }
    public uint FontSize
    {
        get { return (uint)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }
    public uint CancelTextFontSize
    {
        get { return (uint)GetValue(CancelTextFontSizeProperty); }
        set { SetValue(CancelTextFontSizeProperty, value); }
    }
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }
    public Color CancelTextColor
    {
        get { return (Color)GetValue(CancelTextColorProperty); }
        set { SetValue(CancelTextColorProperty, value); }
    }
    public string CancelText
    {
        get { return (string)GetValue(CancelTextProperty); }
        set { SetValue(CancelTextProperty, value); }
    }
    public Color BackspaceColor
    {
        get { return (Color)GetValue(BackspaceColorProperty); }
        set { SetValue(BackspaceColorProperty, value); }
    }

    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(BaseKeyboardViewer), SHAPE_SIZE, propertyChanged: OnSizePropertyChanged);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseKeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnTextColorPropertyChanged);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(uint), typeof(BaseKeyboardViewer), FONT_SIZE, propertyChanged: OnFontSizePropertyChanged);
    public static readonly BindableProperty BackspaceColorProperty = BindableProperty.Create(nameof(BackspaceColor), typeof(Color), typeof(BaseKeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnBackspaceColorPropertyChanged);
    public static readonly BindableProperty CancelTextColorProperty = BindableProperty.Create(nameof(CancelTextColor), typeof(Color), typeof(BaseKeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnCancelTextColorPropertyChanged);
    public static readonly BindableProperty CancelTextFontSizeProperty = BindableProperty.Create(nameof(CancelTextFontSize), typeof(uint), typeof(BaseKeyboardViewer), FONT_SIZE, propertyChanged: OnCancelTextFontSizePropertyChanged);
    public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(BaseKeyboardViewer), CANCEL_TEXT, propertyChanged: OnCancelTextPropertyChanged);

    private static void OnBackspaceColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetBackspaceColor();
    private static void OnCancelTextPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetCancelText();
    private static void OnCancelTextFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetCancelTextFontSize();
    private static void OnCancelTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetCancelTextColor();
    private static void OnFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetFontSize();
    private static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetTextColor();
    private static void OnSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).SetSize();

    protected readonly Grid _layout;

    public BaseKeyboardViewer()
    {
        _layout = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = RowColumnSpacing(),
            RowSpacing = RowColumnSpacing(),
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition { Width = GridLength() }, 3).ToArray()),
            RowDefinitions = new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition { Height = GridLength() }, 4).ToArray())
        };

        CreateContent();
    }

    private void SetBackspaceColor()
    {
        var backspaceButton = (VerticalStackLayout)_layout.Children.Last();
        var labelBackspace = backspaceButton.Children.First() as Label;
        labelBackspace.TextColor = BackspaceColor;
    }
    private void SetCancelText()
    {
        var label = GetCancelLabel();
        label.Text = CancelText;
    }
    private void SetCancelTextFontSize()
    {
        var label = GetCancelLabel();
        label.FontSize = CancelTextFontSize;
    }
    private void SetCancelTextColor()
    {
        var label = GetCancelLabel();
        label.TextColor = CancelTextColor;
    }
    private void SetFontSize()
    {
        var button = _layout.ElementAt(10) as Button;
        SetFontSize(button);

        for (var index = 0; index < 9; index++)
        {
            button = _layout.ElementAt(index) as Button;
            SetFontSize(button);
        }
    }
    private void SetTextColor()
    {
        var button = _layout.ElementAt(10) as Button;
        SetTextColor(button);

        for (var index = 0; index < 9; index++)
        {
            button = _layout.ElementAt(index) as Button;
            SetTextColor(button);
        }
    }
    private void SetSize()
    {
        _layout.ColumnSpacing = RowColumnSpacing();
        _layout.RowSpacing = RowColumnSpacing();

        _layout.ColumnDefinitions.ElementAt(0).Width = GridLength();
        _layout.ColumnDefinitions.ElementAt(1).Width = GridLength();
        _layout.ColumnDefinitions.ElementAt(2).Width = GridLength();

        _layout.RowDefinitions.ElementAt(0).Height = GridLength();
        _layout.RowDefinitions.ElementAt(1).Height = GridLength();
        _layout.RowDefinitions.ElementAt(2).Height = GridLength();
        _layout.RowDefinitions.ElementAt(3).Height = GridLength();

        var cancelButton = (VerticalStackLayout)_layout.Children.ElementAt(9);
        var backspaceButton = (VerticalStackLayout)_layout.Children.Last();

        cancelButton.Padding = new Thickness(0, PaddingCancelAndBackspaceOption(), 0, PaddingCancelAndBackspaceOption());
        cancelButton.WidthRequest = Size;
        cancelButton.HeightRequest = Size;

        backspaceButton.WidthRequest = Size;
        backspaceButton.HeightRequest = Size;
        backspaceButton.Padding = PaddingCancelAndBackspaceOption();

        var labelBackspace = backspaceButton.Children.First() as Label;
        labelBackspace.FontSize = FontSizeBackspaceOption();

        var button = _layout.ElementAt(10) as Button;
        SetSize(button);

        for (var index = 0; index < 9; index++)
        {
            button = _layout.ElementAt(index) as Button;
            SetSize(button);
        }
    }

    private void SetFontSize(Button button) => button.FontSize = FontSize;
    private void SetTextColor(Button button) => button.TextColor = TextColor;
    private Label GetCancelLabel()
    {
        var cancelButton = (VerticalStackLayout)_layout.Children.ElementAt(9);
        return cancelButton.Children.First() as Label;
    }

    protected abstract void SetSize(Button button);

    private void CreateContent()
    {
        _layout.Add(view: AddButtonWithCommand(1), column: 0, row: 0);
        _layout.Add(view: AddButtonWithCommand(2), column: 1, row: 0);
        _layout.Add(view: AddButtonWithCommand(3), column: 2, row: 0);
        _layout.Add(view: AddButtonWithCommand(4), column: 0, row: 1);
        _layout.Add(view: AddButtonWithCommand(5), column: 1, row: 1);
        _layout.Add(view: AddButtonWithCommand(6), column: 2, row: 1);
        _layout.Add(view: AddButtonWithCommand(7), column: 0, row: 2);
        _layout.Add(view: AddButtonWithCommand(8), column: 1, row: 2);
        _layout.Add(view: AddButtonWithCommand(9), column: 2, row: 2);
        _layout.Add(view: CreateCancelOption(), column: 0, row: 3);
        _layout.Add(view: AddButtonWithCommand(0), column: 1, row: 3);
        _layout.Add(view: CreateBackspaceOption(), column: 2, row: 3);

        Content = _layout;
    }

    private IView AddButtonWithCommand(int value)
    {
        var button = CreateButton(value);
        button.Command = new Command(() => { _callbackKeyboardCommand?.Execute(value); });

        return button;
    }

    private VerticalStackLayout CreateCancelOption()
    {
        var verticalLayout = new VerticalStackLayout
        {
            HeightRequest = Size,
            WidthRequest = Size,
            Padding = new Thickness(0, PaddingCancelAndBackspaceOption(), 0, PaddingCancelAndBackspaceOption()),
            Children =
            {
                new Label
                {
                    Text = CancelText,
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = CancelTextFontSize,
                    TextColor = CancelTextColor
                }
            }
        };

        verticalLayout.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => { await Shell.Current.GoToAsync(".."); }) });

        return verticalLayout;
    }
    private IView CreateBackspaceOption()
    {
        var verticalLayout = new VerticalStackLayout
        {
            HeightRequest = Size,
            WidthRequest = Size,
            Padding = PaddingCancelAndBackspaceOption(),
            Children =
            {
                new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "⌫",
                    TextColor = BackspaceColor,
                    FontSize = FontSizeBackspaceOption()
                }
            }
        };

        verticalLayout.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { _callbackKeyboardCommand?.Execute(-1); }) });

        return verticalLayout;
    }

    private double RowColumnSpacing() => Size * 0.25;
    private double FontSizeBackspaceOption() => Size * 0.35;
    private double PaddingCancelAndBackspaceOption() => Size * 0.32;
    private GridLength GridLength() => new (Size, GridUnitType.Star);

    protected abstract Button CreateButton(int option);
    public void SetCommand(ICommand callbackCommand) => _callbackKeyboardCommand = callbackCommand;
}