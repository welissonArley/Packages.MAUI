using PinCodes.Authorization.Extensions;
using System.Windows.Input;

namespace PinCodes.Authorization.Views.Components.Keyboards;

public abstract class KeyBoardViewerBase : ContentView
{
    protected abstract ushort LEFT_SIDE_BUTTON_COLUMN { get; }
    protected abstract ushort LEFT_SIDE_BUTTON_ROW { get; }

    protected abstract ushort BACKSPACE_SIDE_BUTTON_COLUMN { get; }
    protected abstract ushort BACKSPACE_SIDE_BUTTON_ROW { get; }

    protected const ushort SPACING = 30;

    private ICommand _callbackKeyboardCommand = default!;

    public Button ShapeViewer { get => (Button)GetValue(ShapeViewerProperty); set => SetValue(ShapeViewerProperty, value); }
    public static readonly BindableProperty ShapeViewerProperty = BindableProperty.Create(nameof(ShapeViewer), typeof(Button), typeof(KeyBoardViewerBase), propertyChanged: OnShapePropertyChanged);

    public View LeftSideButtonShapeViewer { get => (View)GetValue(LeftSideButtonShapeViewerProperty); set => SetValue(LeftSideButtonShapeViewerProperty, value); }
    public static readonly BindableProperty LeftSideButtonShapeViewerProperty = BindableProperty.Create(nameof(LeftSideButtonShapeViewer), typeof(View), typeof(KeyBoardViewerBase), null, propertyChanged: OnLeftSideButtonShapeViewerPropertyChanged, validateValue: ValidateButtonProperty);

    public View BackspaceViewer { get => (View)GetValue(BackspaceViewerProperty); set => SetValue(BackspaceViewerProperty, value); }
    public static readonly BindableProperty BackspaceViewerProperty = BindableProperty.Create(nameof(BackspaceViewer), typeof(View), typeof(KeyBoardViewerBase), null, propertyChanged: OnBackspaceViewerPropertyChanged, validateValue: ValidateButtonProperty);

    public ushort RowSpacing { get => (ushort)GetValue(RowSpacingProperty); set => SetValue(RowSpacingProperty, value); }
    public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(nameof(RowSpacing), typeof(ushort), typeof(KeyBoardViewerBase), SPACING, propertyChanged: OnRowSpacingPropertyChanged);

    public ushort ColumnSpacing { get => (ushort)GetValue(ColumnSpacingProperty); set => SetValue(ColumnSpacingProperty, value); }
    public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(nameof(ColumnSpacing), typeof(ushort), typeof(KeyBoardViewerBase), SPACING, propertyChanged: OnColumnSpacingPropertyChanged);

    public KeyDescriptionCollection KeyDescriptions { get => (KeyDescriptionCollection)GetValue(KeyDescriptionsProperty); set => SetValue(KeyDescriptionsProperty, value); }
    public static readonly BindableProperty KeyDescriptionsProperty = BindableProperty.Create(nameof(KeyDescriptions), typeof(KeyDescriptionCollection), typeof(KeyBoardViewerBase), propertyChanged: OnKeyDescriptionsPropertyChanged);

    private static void OnShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyBoardViewerBase)bindable).CreateLayout();
    private static void OnLeftSideButtonShapeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyBoardViewerBase)bindable).AddLeftSideButton();
    private static void OnBackspaceViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyBoardViewerBase)bindable).AddBackspaceButton();
    private static void OnRowSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyBoardViewerBase)bindable).SetRowColumnSpacing();
    private static void OnColumnSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((KeyBoardViewerBase)bindable).SetColumnSpacing();

    private static void OnKeyDescriptionsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var keyboard = (KeyBoardViewerBase)bindable;

        if (keyboard.ShapeViewer is not null)
            keyboard.CreateLayout();
    }

    public void SetCommandWhenUserPressButtonOnKeyboard(ICommand callbackCommand) => _callbackKeyboardCommand = callbackCommand;

    private void SetColumnSpacing()
    {
        if (ColumnSpacing <= 0)
            ColumnSpacing = SPACING;

        if (Content is Grid grid)
            grid.ColumnSpacing = ColumnSpacing;
    }

    private void SetRowColumnSpacing()
    {
        if (RowSpacing <= 0)
            RowSpacing = SPACING;

        if (Content is Grid grid)
            grid.RowSpacing = RowSpacing;
    }

    protected Button AddButtonWithCommand(string value)
    {
        var button = ShapeViewer.Clone();
        button.Text = value;
        button.Command = new Command(() => { _callbackKeyboardCommand?.Execute(value); });

        var description = KeyDescriptions?.FirstOrDefault(item => item.Key == value)?.Description;
        if (description.NotEmpty())
            SemanticProperties.SetDescription(button, description);

        return button;
    }

    private static bool ValidateButtonProperty(BindableObject bindable, object value) => value is Button or ImageButton;

    protected abstract void CreateLayout();

    protected virtual void AddLeftSideButton()
    {
        if (LeftSideButtonShapeViewer is not null && Content is not null)
        {
            var grid = Content as Grid;

            LeftSideButtonShapeViewer.WidthRequest = ShapeViewer.WidthRequest;
            LeftSideButtonShapeViewer.HeightRequest = ShapeViewer.HeightRequest;

            grid!.Insert(0, child: LeftSideButtonShapeViewer);
            grid.SetColumn(LeftSideButtonShapeViewer, LEFT_SIDE_BUTTON_COLUMN);
            grid.SetRow(LeftSideButtonShapeViewer, LEFT_SIDE_BUTTON_ROW);
        }
    }

    protected virtual void AddBackspaceButton()
    {
        if (BackspaceViewer is not null && Content is not null)
        {
            var grid = Content as Grid;

            BackspaceViewer.WidthRequest = ShapeViewer.WidthRequest;
            BackspaceViewer.HeightRequest = ShapeViewer.HeightRequest;

            WireBackspaceCommand(BackspaceViewer);

            grid!.Add(BackspaceViewer, column: BACKSPACE_SIDE_BUTTON_COLUMN, row: BACKSPACE_SIDE_BUTTON_ROW);
        }
    }

    protected void WireBackspaceCommand(View backspace)
    {
        var command = new Command(() => _callbackKeyboardCommand?.Execute("-1"));

        if (backspace is Button button)
            button.Command = command;
        else if (backspace is ImageButton imageButton)
            imageButton.Command = command;
    }
}
