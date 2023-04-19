using Microsoft.Maui.Controls.Shapes;
using PinControl.MAUI.Views.Components.Base;

namespace PinControl.MAUI.Views.Components.Square;
public class SquareHidingCode : CodeViewer
{
    public override void CreateContent()
    {
        Content = CreateGridContent();
    }

    private Grid CreateGridContent()
    {
        var grid = CreateGridToShowContent();
        for (var index = 0; index < CodeLength; index++)
        {
            char? codeChar = Code.Length > index ? Code.ElementAt(index) : null;

            grid.Add(view: CreateSquare(codeChar), column: index);
        }

        return grid;
    }

    private Border CreateSquare(char? codeChar)
    {
        return new Border
        {
            WidthRequest = Size,
            HeightRequest = Size,
            Stroke = new SolidColorBrush(Color),
            Background = codeChar.HasValue ? new SolidColorBrush(Color) : new SolidColorBrush(Color.WithAlpha(0.2f)),
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Size * 0.2)
            }
        };
    }
}
