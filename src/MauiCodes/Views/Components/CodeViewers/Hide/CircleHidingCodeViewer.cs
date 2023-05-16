using Microsoft.Maui.Controls.Shapes;

namespace MauiCode.Views.Components.CodeViewers.Hide;

public class CircleHidingCodeViewer : Base.BaseCodeViewer
{
    public override IView CreateCodeView(char? codeChar)
    {
        return new Ellipse
        {
            WidthRequest = Size,
            HeightRequest = Size,
            HorizontalOptions = LayoutOptions.Start,
            Fill = codeChar.HasValue ? new SolidColorBrush(Color) : new SolidColorBrush(Color.WithAlpha(0.2f)),
            StrokeThickness = Size * 0.1,
            Stroke = new SolidColorBrush(Color)
        };
    }
}