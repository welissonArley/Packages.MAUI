using Microsoft.Maui.Controls.Shapes;
using PinControl.MAUI.Views.Components.CodeViewer.Base;

namespace PinControl.MAUI.Views.Components.CodeViewer.Circle;

public class CircleShowingCode : ShowCodeViewer
{
    public override IView CreateCodeView(char? codeChar)
    {
        return new Border
        {
            WidthRequest = Size,
            HeightRequest = Size,
            Background = codeChar.HasValue ? new SolidColorBrush(Color) : new SolidColorBrush(Color.WithAlpha(0.2f)),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Size / 2.0)
            },
            Content = CreateLabel(codeChar)
        };
    }
}