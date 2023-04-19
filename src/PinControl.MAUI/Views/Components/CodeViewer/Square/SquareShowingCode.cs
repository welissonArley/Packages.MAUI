using Microsoft.Maui.Controls.Shapes;
using PinControl.MAUI.Views.Components.CodeViewer.Base;

namespace PinControl.MAUI.Views.Components.CodeViewer.Square;

public class SquareShowingCode : ShowCodeViewer
{
    public override IView CreateCodeView(char? codeChar)
    {
        return new Border
        {
            WidthRequest = Size,
            HeightRequest = Size,
            Stroke = new SolidColorBrush(Color),
            Background = codeChar.HasValue ? new SolidColorBrush() : new SolidColorBrush(Color.WithAlpha(0.2f)),
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            },
            Content = CreateLabel(codeChar)
        };
    }
}