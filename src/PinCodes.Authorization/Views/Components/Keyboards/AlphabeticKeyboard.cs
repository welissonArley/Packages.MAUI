namespace PinCodes.Authorization.Views.Components.Keyboards;

public sealed class AlphabeticKeyboard : KeyBoardViewerBase
{
    protected override ushort LEFT_SIDE_BUTTON_COLUMN => 0;
    protected override ushort LEFT_SIDE_BUTTON_ROW => 3;

    protected override ushort BACKSPACE_SIDE_BUTTON_COLUMN => 6;
    protected override ushort BACKSPACE_SIDE_BUTTON_ROW => 3;

    protected override void CreateLayout()
    {
        var grid = new Grid()
        {
            HorizontalOptions = LayoutOptions.Center,
            ColumnSpacing = ColumnSpacing,
            RowSpacing = RowSpacing,
            ColumnDefinitions = [.. Enumerable.Range(0, 7).Select(_ => new ColumnDefinition { Width = ShapeViewer.WidthRequest })],
            RowDefinitions = [.. Enumerable.Range(0, 4).Select(_ => new RowDefinition { Height = ShapeViewer.HeightRequest })]
        };

        if (LeftSideButtonShapeViewer is not null)
        {
            LeftSideButtonShapeViewer.WidthRequest = ShapeViewer.WidthRequest;
            LeftSideButtonShapeViewer.HeightRequest = ShapeViewer.HeightRequest;

            grid.Add(view: LeftSideButtonShapeViewer, column: LEFT_SIDE_BUTTON_COLUMN, row: LEFT_SIDE_BUTTON_ROW);
        }

        grid.Add(view: AddButtonWithCommand("A"), column: 0, row: 0);
        grid.Add(view: AddButtonWithCommand("B"), column: 1, row: 0);
        grid.Add(view: AddButtonWithCommand("C"), column: 2, row: 0);
        grid.Add(view: AddButtonWithCommand("D"), column: 3, row: 0);
        grid.Add(view: AddButtonWithCommand("E"), column: 4, row: 0);
        grid.Add(view: AddButtonWithCommand("F"), column: 5, row: 0);
        grid.Add(view: AddButtonWithCommand("G"), column: 6, row: 0);

        grid.Add(view: AddButtonWithCommand("H"), column: 0, row: 1);
        grid.Add(view: AddButtonWithCommand("I"), column: 1, row: 1);
        grid.Add(view: AddButtonWithCommand("J"), column: 2, row: 1);
        grid.Add(view: AddButtonWithCommand("K"), column: 3, row: 1);
        grid.Add(view: AddButtonWithCommand("L"), column: 4, row: 1);
        grid.Add(view: AddButtonWithCommand("M"), column: 5, row: 1);
        grid.Add(view: AddButtonWithCommand("N"), column: 6, row: 1);

        grid.Add(view: AddButtonWithCommand("O"), column: 0, row: 2);
        grid.Add(view: AddButtonWithCommand("P"), column: 1, row: 2);
        grid.Add(view: AddButtonWithCommand("Q"), column: 2, row: 2);
        grid.Add(view: AddButtonWithCommand("R"), column: 3, row: 2);
        grid.Add(view: AddButtonWithCommand("S"), column: 4, row: 2);
        grid.Add(view: AddButtonWithCommand("T"), column: 5, row: 2);
        grid.Add(view: AddButtonWithCommand("U"), column: 6, row: 2);

        grid.Add(view: AddButtonWithCommand("V"), column: 1, row: 3);
        grid.Add(view: AddButtonWithCommand("W"), column: 2, row: 3);
        grid.Add(view: AddButtonWithCommand("X"), column: 3, row: 3);
        grid.Add(view: AddButtonWithCommand("Y"), column: 4, row: 3);
        grid.Add(view: AddButtonWithCommand("Z"), column: 5, row: 3);

        Content = grid;

        AddBackspaceButton();
    }
}
