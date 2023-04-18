using Microsoft.Maui.Controls.Shapes;
using PinControl.MAUI.Views.Components.Base;

namespace PinControl.MAUI.Views.Components.Circle;

public class CircleHidingCode : CodeViewer
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

            grid.Add(view: CreateCircle(codeChar.HasValue), column: index);
        }

        return grid;
    }

    private Ellipse CreateCircle(bool hasCodeValue)
    {
        var ellipse = new Ellipse
        {
            WidthRequest = Size,
            HeightRequest = Size,
            HorizontalOptions = LayoutOptions.Start,
            Fill = hasCodeValue ? new SolidColorBrush(Color) : new SolidColorBrush(),
            StrokeThickness = 5,
            Stroke = new SolidColorBrush(Color)
        };

        return ellipse;
    }
}