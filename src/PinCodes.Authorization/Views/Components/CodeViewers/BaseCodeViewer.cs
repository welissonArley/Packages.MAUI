using Microsoft.Maui.Controls.Shapes;
using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;

public abstract class BaseCodeViewer : ContentView
{
    private const ushort CODE_LENGTH = 4;
    private const ushort COLUMN_SPACING = 20;

    protected string Code { get; private set; } = string.Empty;

    private readonly List<Shape> _codeViewerLayouts;

    public ushort CodeLength { get => (ushort)GetValue(CodeLengthProperty); set => SetValue(CodeLengthProperty, value); }
    public static readonly BindableProperty CodeLengthProperty = BindableProperty.Create(nameof(CodeLength), typeof(ushort), typeof(BaseCodeViewer), CODE_LENGTH, propertyChanged: OnCodeLengthPropertyChanged);

    public ushort Spacing { get => (ushort)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(ushort), typeof(BaseCodeViewer), COLUMN_SPACING, propertyChanged: OnSpacingPropertyChanged);

    public Shape ShapeViewer { get => (Shape)GetValue(ShapeProperty); set => SetValue(ShapeProperty, value); }
    public static readonly BindableProperty ShapeProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Shape), typeof(BaseCodeViewer), null, propertyChanged: OnShapePropertyChanged);

    public Color CodeColor { get => (Color)GetValue(CodeColorProperty); set => SetValue(CodeColorProperty, value); }
    public static readonly BindableProperty CodeColorProperty = BindableProperty.Create(nameof(CodeColor), typeof(Color), typeof(BaseCodeViewer), Colors.Red);

    public Color CodeStrokeColor { get => (Color)GetValue(CodeStrokeColorProperty); set => SetValue(CodeStrokeColorProperty, value); }
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

        Content = grid;
    }

    public virtual void SetCode(string code)
    {
        Code = code;

        if (code.IsEmpty())
        {
            foreach (var shape in _codeViewerLayouts)
            {
                shape.Fill = ShapeViewer.Fill;
                shape.Stroke = ShapeViewer.Stroke;
            }

            return;
        }

        var current = _codeViewerLayouts[code.Length - 1];
        current.Fill = new SolidColorBrush(CodeColor);
        current.Stroke = new SolidColorBrush(CodeStrokeColor);

        if (code.Length < CodeLength)
        {
            var next = _codeViewerLayouts[code.Length];
            next.Fill = ShapeViewer.Fill;
            next.Stroke = ShapeViewer.Stroke;
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

                _codeViewerLayouts.Add(viewer);

                grid.Add(view: viewer, column: _codeViewerLayouts.Count - 1);
            }
        }
    }

    private void CreateLayout()
    {
        var grid = Content as Grid;
        grid!.Clear();

        for (var index = 0; index < CodeLength; index++)
        {
            _codeViewerLayouts.Add(CreateCodeView());
            grid.Add(view: _codeViewerLayouts[index], column: index);
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

        return shape;
    }
}