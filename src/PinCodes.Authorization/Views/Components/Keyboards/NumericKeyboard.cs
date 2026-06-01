namespace PinCodes.Authorization.Views.Components.Keyboards;

public sealed class NumericKeyboard : KeyBoardViewerBase
{
    protected override ushort LEFT_SIDE_BUTTON_COLUMN => 0;
    protected override ushort LEFT_SIDE_BUTTON_ROW => 3;

    protected override ushort BACKSPACE_SIDE_BUTTON_COLUMN => 2;
    protected override ushort BACKSPACE_SIDE_BUTTON_ROW => 3;

    protected override void CreateLayout()
    {
        var grid = new Grid()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = ColumnSpacing,
            RowSpacing = RowSpacing,
            ColumnDefinitions = [.. Enumerable.Range(0, 3).Select(_ => new ColumnDefinition { Width = ShapeViewer.WidthRequest })],
            RowDefinitions = [.. Enumerable.Range(0, 4).Select(_ => new RowDefinition { Height = ShapeViewer.HeightRequest })]
        };

        if (LeftSideButtonShapeViewer is not null)
        {
            LeftSideButtonShapeViewer.WidthRequest = ShapeViewer.WidthRequest;
            LeftSideButtonShapeViewer.HeightRequest = ShapeViewer.HeightRequest;

            grid.Add(view: LeftSideButtonShapeViewer, column: LEFT_SIDE_BUTTON_COLUMN, row: LEFT_SIDE_BUTTON_ROW);
        }

        grid.Add(view: AddButtonWithCommand("1"), column: 0, row: 0);
        grid.Add(view: AddButtonWithCommand("2"), column: 1, row: 0);
        grid.Add(view: AddButtonWithCommand("3"), column: 2, row: 0);
        grid.Add(view: AddButtonWithCommand("4"), column: 0, row: 1);
        grid.Add(view: AddButtonWithCommand("5"), column: 1, row: 1);
        grid.Add(view: AddButtonWithCommand("6"), column: 2, row: 1);
        grid.Add(view: AddButtonWithCommand("7"), column: 0, row: 2);
        grid.Add(view: AddButtonWithCommand("8"), column: 1, row: 2);
        grid.Add(view: AddButtonWithCommand("9"), column: 2, row: 2);
        grid.Add(view: AddButtonWithCommand("0"), column: 1, row: 3);

        Content = grid;

        AddBackspaceButton();
    }
}
