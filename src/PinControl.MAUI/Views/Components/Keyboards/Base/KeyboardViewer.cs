using PinControl.MAUI.Helpers.Extensions;

namespace PinControl.MAUI.Views.Components.Keyboards.Base;

public abstract class KeyboardViewer : ContentView
{
    protected const uint SHAPE_SIZE = 80;
    protected const uint FONT_SIZE = 32;
    protected const uint CANCEL_TEXT_FONT_SIZE = 18;
    protected const string CANCEL_TEXT = "Cancel";

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

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(KeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CancelTextColorProperty = BindableProperty.Create(nameof(CancelTextColor), typeof(Color), typeof(KeyboardViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(KeyboardViewer), SHAPE_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(uint), typeof(KeyboardViewer), FONT_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CancelTextFontSizeProperty = BindableProperty.Create(nameof(CancelTextFontSize), typeof(uint), typeof(KeyboardViewer), FONT_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(KeyboardViewer), CANCEL_TEXT, propertyChanged: OnPropertyChanged);

    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).CreateContent();

    public KeyboardViewer() => CreateContent();

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

        grid.Add(view: CreateButton("1"), column: 0, row: 0);
        grid.Add(view: CreateButton("2"), column: 1, row: 0);
        grid.Add(view: CreateButton("3"), column: 2, row: 0);
        grid.Add(view: CreateButton("4"), column: 0, row: 1);
        grid.Add(view: CreateButton("5"), column: 1, row: 1);
        grid.Add(view: CreateButton("6"), column: 2, row: 1);
        grid.Add(view: CreateButton("7"), column: 0, row: 2);
        grid.Add(view: CreateButton("8"), column: 1, row: 2);
        grid.Add(view: CreateButton("9"), column: 2, row: 2);
        grid.Add(view: CreateCancelOption(), column: 0, row: 3);
        grid.Add(view: CreateButton("0"), column: 1, row: 3);
        grid.Add(view: CreateImageOption(), column: 2, row: 3);

        Content = grid;
    }

    public abstract IView CreateButton(string option);

    private Label CreateCancelOption()
    {
        return new Label
        {
            Text = CancelText,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = CancelTextFontSize,
            TextColor = CancelTextColor
        };
    }
    private VerticalStackLayout CreateImageOption()
    {
        return new VerticalStackLayout
        {
            HeightRequest = Size,
            WidthRequest = Size,
            Padding = Size * 0.32,
            Children =
            {
                new Image
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Source = ImageSource.FromFile("Resources/Images/icon_delete.png")
                }
            }
        };
    }
}