using Microsoft.Maui.Controls.Shapes;

namespace MauiCodes.Views.Components.CodeViewers.Hide;
public class SquareHidingCodeViewer : Base.BaseCodeViewer
{
    protected override IView CreateCodeView()
    {
        return new Border
        {
            WidthRequest = Size,
            HeightRequest = Size,
            Stroke = new SolidColorBrush(Color),
            Background = ColorWithAlpha(),
            StrokeThickness = Size * 0.1,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Size * 0.2)
            }
        };
    }

    protected override void ChangeColorCodeView(IView view)
    {
        var layout = (Border)view;

        layout.Stroke = new SolidColorBrush(Color);
        layout.Background = ColorWithAlpha();
    }

    public override void SetCode(string code)
    {
        base.SetCode(code);

        for (var index = 0; index < CodeLength; index++)
        {
            var item = (Border)_layout.ElementAt(index);

            char? codeChar = code.Length > index ? code.ElementAt(index) : null;

            if (codeChar.HasValue)
                item.Background = new SolidColorBrush(Color);
            else
                item.Background = ColorWithAlpha();
        }
    }
}
