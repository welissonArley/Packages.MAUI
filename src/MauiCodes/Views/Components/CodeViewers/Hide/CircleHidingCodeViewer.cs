using Microsoft.Maui.Controls.Shapes;

namespace MauiCode.Views.Components.CodeViewers.Hide;

public class CircleHidingCodeViewer : Base.BaseCodeViewer
{
    protected override IView CreateCodeView()
    {
        return new Ellipse
        {
            WidthRequest = Size,
            HeightRequest = Size,
            HorizontalOptions = LayoutOptions.Start,
            Fill = ColorWithAlpha(),
            StrokeThickness = Size * 0.1,
            Stroke = new SolidColorBrush(Color)
        };
    }

    protected override void ChangeColorCodeView(IView view)
    {
        var layout = (Ellipse)view;

        layout.Stroke = new SolidColorBrush(Color);
        layout.Fill = ColorWithAlpha();
    }
}