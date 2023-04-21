using MauiCode.Views.Components.Keyboards.Base;

namespace MauiCode.Views.Components.Keyboards;
public class KeyboardSquare : BaseKeyboardShapeViewer
{
    public override Button CreateButton(int value)
    {
        return new Button
        {
            WidthRequest = Size,
            HeightRequest = Size,
            BackgroundColor = ShapeColor,
            CornerRadius = Convert.ToInt32(Size * 0.2),
            Text = $"{value}",
            FontSize = FontSize,
            TextColor = TextColor
        };
    }
}
