using MauiCode.Helpers.Extensions;
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

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseKeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CancelTextColorProperty = BindableProperty.Create(nameof(CancelTextColor), typeof(Color), typeof(BaseKeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(BaseKeyboardViewer), SHAPE_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(uint), typeof(BaseKeyboardViewer), FONT_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CancelTextFontSizeProperty = BindableProperty.Create(nameof(CancelTextFontSize), typeof(uint), typeof(BaseKeyboardViewer), FONT_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(BaseKeyboardViewer), CANCEL_TEXT, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty BackspaceColorProperty = BindableProperty.Create(nameof(BackspaceColor), typeof(Color), typeof(BaseKeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);

    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardViewer)bindable).CreateContent();

    public BaseKeyboardViewer() => CreateContent();

    public void CreateContent()
    {
        Grid grid = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = Size * 0.25,
            RowSpacing = Size * 0.25,
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition { Width = new GridLength(Size, GridUnitType.Star) }, 3).ToArray()),
            RowDefinitions = new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition { Height = new GridLength(Size, GridUnitType.Star) }, 4).ToArray())
        };

        grid.Add(view: AddButtonWithCommand(1), column: 0, row: 0);
        grid.Add(view: AddButtonWithCommand(2), column: 1, row: 0);
        grid.Add(view: AddButtonWithCommand(3), column: 2, row: 0);
        grid.Add(view: AddButtonWithCommand(4), column: 0, row: 1);
        grid.Add(view: AddButtonWithCommand(5), column: 1, row: 1);
        grid.Add(view: AddButtonWithCommand(6), column: 2, row: 1);
        grid.Add(view: AddButtonWithCommand(7), column: 0, row: 2);
        grid.Add(view: AddButtonWithCommand(8), column: 1, row: 2);
        grid.Add(view: AddButtonWithCommand(9), column: 2, row: 2);
        grid.Add(view: CreateCancelOption(), column: 0, row: 3);
        grid.Add(view: AddButtonWithCommand(0), column: 1, row: 3);
        grid.Add(view: CreateDeleteOption(), column: 2, row: 3);

        Content = grid;
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
            Padding = new Thickness(0, Size * 0.32, 0, Size * 0.32),
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
    private IView CreateDeleteOption()
    {
        var verticalLayout = new VerticalStackLayout
        {
            HeightRequest = Size,
            WidthRequest = Size,
            Padding = Size * 0.32,
            Children =
            {
                new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "⌫",
                    TextColor = BackspaceColor,
                    FontSize = Size * 0.35
                }
            }
        };

        verticalLayout.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { _callbackKeyboardCommand?.Execute(-1); }) });

        return verticalLayout;
    }

    public abstract Button CreateButton(int option);
    public void SetCommand(ICommand callbackCommand) => _callbackKeyboardCommand = callbackCommand;
}