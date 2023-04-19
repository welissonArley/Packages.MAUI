using PinControl.MAUI.Views.Components.Keyboards.Base;

namespace PinControl.MAUI.Views.Components.Keyboards.Square;
public class KeyboardSquare : KeyboardShapeViewer
{
    public override Button CreateButton(string value)
    {
        return new Button
        {
            WidthRequest = Size,
            HeightRequest = Size,
            BackgroundColor = ShapeColor,
            CornerRadius = Convert.ToInt32(Size * 0.2),
            Text = value,
            FontSize = FontSize,
            TextColor = TextColor
        };
    }
}
