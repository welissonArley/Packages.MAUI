namespace PinCodes.Authorization.Views.Components.Keyboards;

public sealed class AlphanumericKeyboard : KeyBoardViewerBase
{
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    private static readonly int[] RowCounts = [6, 5, 6, 5, 4, 6, 4];

    protected override ushort LEFT_SIDE_BUTTON_COLUMN => 0;
    protected override ushort LEFT_SIDE_BUTTON_ROW => 0;
    protected override ushort BACKSPACE_SIDE_BUTTON_COLUMN => 0;
    protected override ushort BACKSPACE_SIDE_BUTTON_ROW => 0;

    protected override void CreateLayout() => BuildKeyboard();
    protected override void AddLeftSideButton() => BuildKeyboard();
    protected override void AddBackspaceButton() => BuildKeyboard();

    private void BuildKeyboard()
    {
        if (ShapeViewer is null)
            return;

        var stack = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = RowSpacing
        };

        var index = 0;
        for (var rowIndex = 0; rowIndex < RowCounts.Length; rowIndex++)
        {
            var isLastRow = rowIndex == RowCounts.Length - 1;

            var row = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Spacing = ColumnSpacing
            };

            if (isLastRow && LeftSideButtonShapeViewer is not null)
            {
                Detach(LeftSideButtonShapeViewer);
                SizeLikeKey(LeftSideButtonShapeViewer);
                row.Add(LeftSideButtonShapeViewer);
            }

            for (var column = 0; column < RowCounts[rowIndex] && index < Characters.Length; column++)
                row.Add(AddButtonWithCommand(Characters[index++].ToString()));

            if (isLastRow && BackspaceViewer is not null)
            {
                Detach(BackspaceViewer);
                SizeLikeKey(BackspaceViewer);
                WireBackspaceCommand(BackspaceViewer);
                row.Add(BackspaceViewer);
            }

            stack.Add(row);
        }

        Content = stack;
    }

    private void SizeLikeKey(View view)
    {
        view.WidthRequest = ShapeViewer.WidthRequest;
        view.HeightRequest = ShapeViewer.HeightRequest;
    }

    private static void Detach(View view)
    {
        if (view.Parent is Layout layout)
            layout.Remove(view);
    }
}
