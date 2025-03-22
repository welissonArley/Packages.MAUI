using Microsoft.Maui.Controls.Shapes;
using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;

public abstract class BaseCodeViewer : ContentView
{
    private const ushort CODE_LENGTH = 4;
    private const ushort COLUMN_SPACING = 20;

    protected string Code { get; private set; } = string.Empty;

    private readonly List<Shape> _codeViewerLayouts;
    private readonly List<AbsoluteLayout> _codeViewerContainers;

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

    public Color CodeColor
    {
        get => (Color)GetValue(CodeColorProperty);
        set => SetValue(CodeColorProperty, value);
    }

    public Color CodeStrokeColor
    {
        get => (Color)GetValue(CodeStrokeColorProperty);
        set => SetValue(CodeStrokeColorProperty, value);
    }

    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(BaseCodeViewer), CODE_LENGTH, propertyChanged: OnCodeLengthPropertyChanged);
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(ushort), typeof(BaseCodeViewer), COLUMN_SPACING, propertyChanged: OnSpacingPropertyChanged);
    public static readonly BindableProperty ShapeProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Shape), typeof(BaseCodeViewer), null, propertyChanged: OnShapePropertyChanged);
    public static readonly BindableProperty CodeColorProperty = BindableProperty.Create(nameof(CodeColor), typeof(Color), typeof(BaseCodeViewer), Colors.Red);
    public static readonly BindableProperty CodeStrokeColorProperty = BindableProperty.Create(nameof(CodeStrokeColor), typeof(Color), typeof(BaseCodeViewer), Colors.Yellow);

    private static void OnCodeLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetCodeLength();
    private static void OnSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).SetColumnSpacing();
    private static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseCodeViewer)bindable).CreateLayout();

    public BaseCodeViewer()
    {
        var grid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = Spacing
        };

        _codeViewerLayouts = [];
        _codeViewerContainers = [];

        Content = grid;
    }

    public virtual void SetCode(string code)
    {
        Code = code;

        for (var index = 0; index < CodeLength; index++)
        {
            var item = _codeViewerLayouts[index];

            if (Code.Length > index)
            {
                item.Fill = new SolidColorBrush(CodeColor);
                item.Stroke = new SolidColorBrush(CodeStrokeColor);
            }
            else
            {
                item.Fill = ShapeViewer.Fill;
                item.Stroke = ShapeViewer.Stroke;
            }
        }
    }

    private void SetCodeLength()
    {
        if (CodeLength <= 0)
            CodeLength = CODE_LENGTH;

        if (_codeViewerLayouts.Count == 0)
            return;
        
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

                _codeViewerContainers.Add(viewer.Container);
                _codeViewerLayouts.Add(viewer.Shape);

                grid.Add(view: viewer.Container, column: _codeViewerLayouts.Count - 1);
            }
        }
    }

    private void CreateLayout()
    {
        var grid = Content as Grid;
        grid!.Clear();

        for (var index = 0; index < CodeLength; index++)
        {
            var viewer = CreateCodeView();

            _codeViewerContainers.Add(viewer.Container);
            _codeViewerLayouts.Add(viewer.Shape);

            grid.Add(view: viewer.Container, column: index);
        }
    }

    private void SetColumnSpacing()
    {
        if (Spacing <= 0)
            Spacing = COLUMN_SPACING;

        var grid = Content as Grid;
        grid!.ColumnSpacing = Spacing;
    }

    private (AbsoluteLayout Container, Shape Shape) CreateCodeView()
    {
        var shape = ShapeViewer.Clone();
        var container = new AbsoluteLayout
        {
            HeightRequest = shape.HeightRequest,
            WidthRequest = shape.WidthRequest,
            Background = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,

            Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, shape.WidthRequest, shape.HeightRequest)
            }
        };

        container.Add(shape);

        return (container, shape);
    }

    protected AbsoluteLayout this[int index] => _codeViewerContainers[index];
}