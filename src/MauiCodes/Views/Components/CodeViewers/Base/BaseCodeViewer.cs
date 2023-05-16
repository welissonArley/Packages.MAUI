using MauiCode.Helpers.Extensions;

namespace MauiCode.Views.Components.CodeViewers.Base;

public abstract class BaseCodeViewer : ContentView
{
    protected const ushort CODE_LENGTH = 4;
    protected const uint CIRCLE_SIZE = 20;

    public string Code { get; private set; } = string.Empty;

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

    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(BaseCodeViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#000000" : "#FFFFFF"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(BaseCodeViewer), CIRCLE_SIZE, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(BaseCodeViewer), CODE_LENGTH, propertyChanged: OnPropertyChanged);

    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).CreateContent();

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

    public BaseCodeViewer() => CreateContent();

    public void SetCode(string code)
    {
        Code = code;
        CreateContent();
    }
}