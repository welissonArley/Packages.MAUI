using PinControl.MAUI.Helpers.Extensions;

namespace PinControl.MAUI.Views.Components.CodeViewer.Base;

public abstract class CodeViewer : ContentView
{
    protected const ushort CODE_LENGTH = 4;
    protected const uint CIRCLE_SIZE = 20;

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

    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(CodeViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(CodeViewer), CIRCLE_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeProperty = BindableProperty.Create(nameof(Code), typeof(string), typeof(CodeViewer), string.Empty, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(CodeViewer), CODE_LENGTH, propertyChanged: OnPropertyChanged);

    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodeViewer)bindable).CreateContent();

    public void CreateContent()
    {
        Grid grid = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = Size * 0.5,
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition { Width = Size }, CodeLength).ToArray())
        };

        for (var index = 0; index < CodeLength; index++)
        {
            char? codeChar = Code.Length > index ? Code.ElementAt(index) : null;

            grid.Add(view: CreateCodeView(codeChar), column: index);
        }

        Content = grid;
    }

    public abstract IView CreateCodeView(char? codeChar);

    public CodeViewer() => CreateContent();
}