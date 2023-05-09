using MauiCode.Views.Components.CodeViewers.Base;
using Microsoft.Maui.Controls.Shapes;

namespace MauiCode.Views.Components.CodeViewers.Show;

public class SquareShowingCodeViewer : BaseShowingCodeViewer
{
    protected override IView CreateCodeView()
    {
        return new Border
        {
            WidthRequest = Size,
            HeightRequest = Size,
            Stroke = new SolidColorBrush(Color),
            Background = ColorWithAlpha(),
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            }
        };
    }

    protected override void ChangeColorCodeView(IView view)
    {
        var layout = (Border)view;

        layout.Stroke = new SolidColorBrush(Color);
        layout.Background = ColorWithAlpha();
    }
}