namespace MauiCodes.Views.Components.CodeViewers.Base;

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

    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(BaseCodeViewer), Colors.Red, propertyChanged: OnColorPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(uint), typeof(BaseCodeViewer), CIRCLE_SIZE, propertyChanged: OnSizePropertyChanged);
    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(BaseCodeViewer), CODE_LENGTH, propertyChanged: OnCodeLengthPropertyChanged);

    protected static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetColor();
    private static void OnCodeLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetCodeLength();
    private static void OnSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetSize();
    
    protected readonly Grid _layout;

    public BaseCodeViewer()
    {
        _layout = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = CreateColumnSpacing(),
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(CreateColumnDefinition(), CodeLength).ToArray())
        };

        for (var index = 0; index < CodeLength; index++)
            _layout.Add(view: CreateCodeView(), column: index);

        Content = _layout;
    }

    private void SetCodeLength()
    {
        if (CodeLength <= 0)
            CodeLength = CODE_LENGTH;

        _layout.ColumnDefinitions.Clear();
        _layout.Clear();

        _layout.ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(CreateColumnDefinition(), CodeLength).ToArray());

        for (var index = 0; index < CodeLength; index++)
            _layout.Add(view: CreateCodeView(), column: index);
    }
    private void SetSize()
    {
        for (var index = 0; index < CodeLength; index++)
        {
            var collumnDefinition = _layout.ColumnDefinitions.ElementAt(index);
            collumnDefinition.Width = Size;

            var item = (VisualElement)_layout.ElementAt(index);
            item.WidthRequest = Size;
            item.HeightRequest = Size;
        }
    }
    private void SetColor()
    {
        for (var index = 0; index < CodeLength; index++)
        {
            var item = _layout.ElementAt(index);
            ChangeColorCodeView(item);
        }
    }

    private ColumnDefinition CreateColumnDefinition() => new () { Width = Size };
    private double CreateColumnSpacing() => Size * 0.5;

    protected abstract IView CreateCodeView();
    protected abstract void ChangeColorCodeView(IView view);

    protected SolidColorBrush ColorWithAlpha() => new (Color.WithAlpha(0.2f));

    public virtual void SetCode(string code) => Code = code;
}