using Microsoft.Maui.Controls.Shapes;

namespace PinControl.MAUI.Views.Components.CodeViewer.Square;
public class SquareHidingCode : Base.CodeViewer
{
    public override IView CreateCodeView(char? codeChar)
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
