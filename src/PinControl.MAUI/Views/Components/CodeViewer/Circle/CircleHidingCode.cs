using Microsoft.Maui.Controls.Shapes;

namespace PinControl.MAUI.Views.Components.CodeViewer.Circle;

public class CircleHidingCode : Base.CodeViewer
{
    public override IView CreateCodeView(char? codeChar)
    {
        return new Ellipse
        {
            WidthRequest = Size,
            HeightRequest = Size,
            HorizontalOptions = LayoutOptions.Start,
            Fill = codeChar.HasValue ? new SolidColorBrush(Color) : new SolidColorBrush(),
            StrokeThickness = 5,
            Stroke = new SolidColorBrush(Color)
        };
    }
}