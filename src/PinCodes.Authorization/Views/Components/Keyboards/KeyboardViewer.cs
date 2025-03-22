using PinCodes.Authorization.Extensions;
using System.Windows.Input;

namespace PinCodes.Authorization.Views.Components.Keyboards;

public sealed partial class KeyboardViewer : ContentView
{
    private const ushort SPACING = 30;

    private ICommand _callbackKeyboardCommand = default!;

    public Button ShapeViewer
    {
        get => (Button)GetValue(ShapeViewerProperty);
        set => SetValue(ShapeViewerProperty, value);
    }

    public View LeftSideButtonShapeViewer
    {
        get => (View)GetValue(LeftSideButtonShapeViewerProperty);
        set => SetValue(LeftSideButtonShapeViewerProperty, value);
    }

    public View BackspaceViewer
    {
        get => (View)GetValue(BackspaceViewerProperty);
        set => SetValue(BackspaceViewerProperty, value);
    }

    public ushort RowSpacing
    {
        get => (ushort)GetValue(RowSpacingProperty);
        set => SetValue(RowSpacingProperty, value);
    }

    public ushort ColumnSpacing
    {
        get => (ushort)GetValue(ColumnSpacingProperty);
        set => SetValue(ColumnSpacingProperty, value);
    }

    public static readonly BindableProperty ShapeViewerProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Button), typeof(KeyboardViewer), null, propertyChanged: OnShapePropertyChanged);
    public static readonly BindableProperty LeftSideButtonShapeViewerProperty = BindableProperty.Create(nameof(LeftSideButtonShapeViewer), typeof(View), typeof(KeyboardViewer), null, propertyChanged: OnLeftSideButtonShapeViewerPropertyChanged, validateValue: ValidateButtonProperty);
    public static readonly BindableProperty BackspaceViewerProperty = BindableProperty.Create(nameof(BackspaceViewer), typeof(View), typeof(KeyboardViewer), null, propertyChanged: OnBackspaceViewerPropertyChanged, validateValue: ValidateButtonProperty);
    public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(nameof(RowSpacing), typeof(ushort), typeof(KeyboardViewer), SPACING, propertyChanged: OnRowSpacingPropertyChanged);
    public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(nameof(ColumnSpacing), typeof(ushort), typeof(KeyboardViewer), SPACING, propertyChanged: OnColumnSpacingPropertyChanged);

    private static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).CreateLayout();
    private static void OnBackspaceViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).UpdateBackSpaceLayout();
    private static void OnRowSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).SetRowColumnSpacing();
    private static void OnColumnSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).SetColumnSpacing();
    private static void OnLeftSideButtonShapeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyboardViewer)bindable).AddLeftSideButton();

    public void SetCommandWhenUserPressButtonOnKeyboard(ICommand callbackCommand) => _callbackKeyboardCommand = callbackCommand;

    private void CreateLayout()
    {
        var grid = new Grid()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = ColumnSpacing,
            RowSpacing = RowSpacing,
            ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition { Width = ShapeViewer.WidthRequest }, 3).ToArray()),
            RowDefinitions = new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition { Height = ShapeViewer.HeightRequest }, 4).ToArray())
        };

        if (LeftSideButtonShapeViewer is not null)
        {
            LeftSideButtonShapeViewer.WidthRequest = ShapeViewer.WidthRequest;
            LeftSideButtonShapeViewer.HeightRequest = ShapeViewer.HeightRequest;

            grid.Add(view: LeftSideButtonShapeViewer, column: 0, row: 3);
        }

        grid.Add(view: AddButtonWithCommand(1), column: 0, row: 0);
        grid.Add(view: AddButtonWithCommand(2), column: 1, row: 0);
        grid.Add(view: AddButtonWithCommand(3), column: 2, row: 0);
        grid.Add(view: AddButtonWithCommand(4), column: 0, row: 1);
        grid.Add(view: AddButtonWithCommand(5), column: 1, row: 1);
        grid.Add(view: AddButtonWithCommand(6), column: 2, row: 1);
        grid.Add(view: AddButtonWithCommand(7), column: 0, row: 2);
        grid.Add(view: AddButtonWithCommand(8), column: 1, row: 2);
        grid.Add(view: AddButtonWithCommand(9), column: 2, row: 2);
        grid.Add(view: AddButtonWithCommand(0), column: 1, row: 3);

        if (BackspaceViewer is not null)
            grid.Add(view: CreateBackspaceOption(), column: 2, row: 3);

        Content = grid;
    }

    private void UpdateBackSpaceLayout()
    {
        if (Content is not null)
        {
            var grid = Content as Grid;
            grid.Add(view: CreateBackspaceOption(), column: 2, row: 3);
        }
    }

    private void AddLeftSideButton()
    {
        if (LeftSideButtonShapeViewer is not null && Content is not null)
        {
            var grid = Content as Grid;

            LeftSideButtonShapeViewer.WidthRequest = ShapeViewer.WidthRequest;
            LeftSideButtonShapeViewer.HeightRequest = ShapeViewer.HeightRequest;

            grid!.Insert(0, child: LeftSideButtonShapeViewer);
            grid.SetColumn(LeftSideButtonShapeViewer, 0);
            grid.SetRow(LeftSideButtonShapeViewer, 3);
        }
    }

    private void SetColumnSpacing()
    {
        if (Content is not null)
        {
            if (ColumnSpacing <= 0)
                ColumnSpacing = SPACING;

            var grid = Content as Grid;
            grid!.ColumnSpacing = ColumnSpacing;
        }
    }

    private void SetRowColumnSpacing()
    {
        if (Content is not null)
        {
            if (RowSpacing <= 0)
                RowSpacing = SPACING;

            var grid = Content as Grid;
            grid!.RowSpacing = RowSpacing;
        }
    }

    private Button AddButtonWithCommand(int value)
    {
        var button = ShapeViewer.Clone();
        button.Text = $"{value}";
        button.Command = new Command(() => { _callbackKeyboardCommand?.Execute(value); });

        return button;
    }

    private View CreateBackspaceOption()
    {
        var viewer = BackspaceViewer.Clone();

        viewer.WidthRequest = ShapeViewer.WidthRequest;
        viewer.HeightRequest = ShapeViewer.HeightRequest;

        var command = new Command(() => { _callbackKeyboardCommand?.Execute(-1); });
        if (viewer is Button button)
            button.Command = command;
        else
            ((ImageButton)viewer).Command = command;

        return viewer;
    }

    private static bool ValidateButtonProperty(BindableObject bindable, object value) => value is Button or ImageButton;
}