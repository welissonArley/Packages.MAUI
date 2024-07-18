using Microsoft.Maui.Controls.Shapes;
using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;

public sealed class CodeViewer : ContentView
{
    private const ushort CODE_LENGTH = 4;
    private const ushort COLUMN_SPACING = 20;
    private readonly Grid _layout;

    public string Code { get; private set; } = string.Empty;

    public ushort CodeLength
    {
        get => (ushort)GetValue(CodeLengthProperty);
        set => SetValue(CodeLengthProperty, value);
    }

    public ushort Spacing
    {
        get => (ushort)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public Shape ShapeViewer
    {
        get => (Shape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(CodeViewer), CODE_LENGTH, propertyChanged: OnCodeLengthPropertyChanged);
    public static readonly BindableProperty ShapeProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Shape), typeof(CodeViewer), GetDefaultCodeViewShape(), propertyChanged: OnShapePropertyChanged);
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(ushort), typeof(CodeViewer), COLUMN_SPACING, propertyChanged: OnSpacingPropertyChanged);

    private static void OnCodeLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodeViewer)bindable).SetCodeLength();
    private static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodeViewer)bindable).CreateLayout();
    private static void OnSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodeViewer)bindable).SetColumnSpacing();

    public CodeViewer()
    {
        _layout = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = Spacing
        };

        CreateLayout();
        Content = _layout;
    }

    public void SetCode(string code)
    {
        Code = code;

        for (var index = 0; index < CodeLength; index++)
        {
            var item = (Shape)_layout.ElementAt(index);

            char? codeChar = code.Length > index ? code.ElementAt(index) : null;

            if (codeChar.HasValue)
            {
                item.Fill = ShapeViewer.Fill;
                _layout.Add(view: CreateLabel(codeChar.Value), column: index);
            }
            else
                item.Fill = ColorWithAlpha(ShapeViewer.Fill);
        }
    }

    private void SetCodeLength()
    {
        if (CodeLength <= 0)
            CodeLength = CODE_LENGTH;

        CreateLayout();
    }

    private void CreateLayout()
    {
        _layout.Clear();

        for (var index = 0; index < CodeLength; index++)
            _layout.Add(view: CreateCodeView(), column: index);
    }

    private void SetColumnSpacing()
    {
        if (Spacing <= 0)
            Spacing = COLUMN_SPACING;

        _layout.ColumnSpacing = Spacing;
    }

    private Shape CreateCodeView()
    {
        var shape = ShapeViewer.Clone();
        shape.Fill = ColorWithAlpha(shape.Fill);

        return shape;
    }

    private static Ellipse GetDefaultCodeViewShape()
    {
        var color = Colors.Red;

        return new Ellipse
        {
            WidthRequest = 30,
            HeightRequest = 30,
            HorizontalOptions = LayoutOptions.Start,
            Fill = color.WithAlpha(0.2f),
            StrokeThickness = 20 * 0.1,
            Stroke = new SolidColorBrush(color)
        };
    }

    private Label CreateLabel(char codeChar)
    {
        return new Label
        {
            TextColor = Colors.Red,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = $"{codeChar}"
        };
    }

    private static SolidColorBrush ColorWithAlpha(Brush brush)
    {
        var solidColorBrush = brush as SolidColorBrush;
        var color = solidColorBrush!.Color;

        return new (color.WithAlpha(0.2f));
    }
}