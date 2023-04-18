namespace PinControl.MAUI.Views.Components.Base;

public abstract class CodeViewer : ContentView
{
    protected const ushort CODE_LENGTH = 4;

    public string Code
    {
        get { return (string)GetValue(CodeProperty); }
        set { SetValue(CodeProperty, value); }
    }

    public ushort CodeLength
    {
        get { return (ushort)GetValue(CodeLengthProperty); }
        set { SetValue(CodeLengthProperty, value); }
    }

    protected const uint CIRCLE_SIZE = 20;

    public Color Color
    {
        get { return (Color)GetValue(ColorProperty); }
        set { SetValue(ColorProperty, value); }
    }

    public uint Size
    {
        get { return (uint)GetValue(SizeProperty); }
        set { SetValue(SizeProperty, value); }
    }

    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(CodeViewer), Color.FromArgb("#000000"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(CodeViewer), CIRCLE_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeProperty = BindableProperty.Create(nameof(Code), typeof(string), typeof(CodeViewer), string.Empty, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(CodeViewer), CODE_LENGTH, propertyChanged: OnPropertyChanged);

    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodeViewer)bindable).CreateContent();

    protected Grid CreateGridToShowContent()
    {
        return new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = Size * 0.5,
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition { Width = Size }, CodeLength).ToArray())
        };
    }

    public abstract void CreateContent();

    public CodeViewer() => CreateContent();
}