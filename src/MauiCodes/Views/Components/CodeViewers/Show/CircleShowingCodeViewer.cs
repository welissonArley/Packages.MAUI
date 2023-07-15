using MauiCodes.Views.Components.CodeViewers.Base;
using Microsoft.Maui.Controls.Shapes;

namespace MauiCodes.Views.Components.CodeViewers.Show;

public class CircleShowingCodeViewer : BaseShowingCodeViewer
{
    protected override IView CreateCodeView()
    {
        return new Border
        {
            WidthRequest = Size,
            HeightRequest = Size,
            Background = ColorWithAlpha(),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Size / 2.0)
            }
        };
    }

    protected override void ChangeColorCodeView(IView view)
    {
        var layout = (Border)view;
        layout.Background = ColorWithAlpha();
    }
}