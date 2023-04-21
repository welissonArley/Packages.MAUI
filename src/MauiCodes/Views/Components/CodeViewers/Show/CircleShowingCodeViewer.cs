using MauiCode.Views.Components.CodeViewers.Base;
using Microsoft.Maui.Controls.Shapes;

namespace MauiCode.Views.Components.CodeViewers.Show;

public class CircleShowingCodeViewer : BaseShowingCodeViewer
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