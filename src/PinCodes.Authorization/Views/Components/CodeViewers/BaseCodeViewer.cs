using Microsoft.Maui.Controls.Shapes;
using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;

public abstract class BaseCodeViewer : ContentView
{
    private const ushort CODE_LENGTH = 4;
    private const ushort COLUMN_SPACING = 20;
    private readonly List<Shape> _codeViewerLayouts;

    protected string Code { get; private set; } = string.Empty;

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

    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(BaseCodeViewer), CODE_LENGTH, propertyChanged: OnCodeLengthPropertyChanged);
    public static readonly BindableProperty ShapeProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Shape), typeof(BaseCodeViewer), GetDefaultCodeViewShape(), propertyChanged: OnShapePropertyChanged);
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(ushort), typeof(BaseCodeViewer), COLUMN_SPACING, propertyChanged: OnSpacingPropertyChanged);

    private static void OnCodeLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetCodeLength();
    private static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).UpdateLayout();
    private static void OnSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetColumnSpacing();

    public BaseCodeViewer()
    {
        _codeViewerLayouts = Enumerable.Range(0, CodeLength).Select(_ => CreateCodeView()).ToList();

        var grid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = Spacing
        };

        for (var index = 0; index < CodeLength; index++)
            grid.Add(view: _codeViewerLayouts[index], column: index);

        Content = grid;
    }

    public virtual void SetCode(string code)
    {
        Code = code;

        for (var index = 0; index < CodeLength; index++)
        {
            var item = _codeViewerLayouts[index];

            if (Code.Length > index)
                item.Fill = ShapeViewer.Fill;
            else
                item.Fill = ColorWithAlpha(ShapeViewer.Fill);
        }
    }

    private void SetCodeLength()
    {
        if (CodeLength <= 0)
            CodeLength = CODE_LENGTH;
        
        var grid = Content as Grid;
        var currentCodeLength = _codeViewerLayouts.Count;
        if (CodeLength < currentCodeLength)
        {
            for (var index = 0; index < currentCodeLength - CodeLength; index++)
            {
                _codeViewerLayouts.RemoveAt(_codeViewerLayouts.Count);
                grid!.RemoveAt(_codeViewerLayouts.Count);
            }
        }
        else if (CodeLength > currentCodeLength)
        {
            for (var index = 0; index < CodeLength - currentCodeLength; index++)
            {
                var viewer = CreateCodeView();

                _codeViewerLayouts.Add(viewer);

                grid.Add(view: viewer, column: _codeViewerLayouts.Count - 1);
            }
        }
    }

    private void UpdateLayout()
    {
        var grid = Content as Grid;

        for (var index = 0; index < CodeLength; index++)
        {
            _codeViewerLayouts[index] = CreateCodeView();
            grid![index] = _codeViewerLayouts[index];

            Grid.SetColumn(_codeViewerLayouts[index], index);
        }
    }

    private void SetColumnSpacing()
    {
        if (Spacing <= 0)
            Spacing = COLUMN_SPACING;

        var grid = Content as Grid;
        grid!.ColumnSpacing = Spacing;
    }

    private Shape CreateCodeView()
    {
        var shape = ShapeViewer.Clone();
        shape.Fill = ColorWithAlpha(shape.Fill);

        return shape;
    }

    private static SolidColorBrush ColorWithAlpha(Brush brush)
    {
        var solidColorBrush = brush as SolidColorBrush;
        var color = solidColorBrush!.Color;

        return new (color.WithAlpha(0.2f));
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
}