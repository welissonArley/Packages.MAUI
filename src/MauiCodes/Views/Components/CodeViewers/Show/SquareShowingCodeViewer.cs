using MauiCode.Views.Components.CodeViewers.Base;
using Microsoft.Maui.Controls.Shapes;

namespace MauiCode.Views.Components.CodeViewers.Show;

public class SquareShowingCodeViewer : BaseShowingCodeViewer
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