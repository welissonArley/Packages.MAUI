using PinControl.MAUI.Views.Components.Keyboards.Base;

namespace PinControl.MAUI.Views.Components.Keyboards.Circle;
public class KeyboardCircle : KeyboardShapeViewer
{
    public override Button CreateButton(string value)
    {
        return new Button
        {
            WidthRequest = Size,
            HeightRequest = Size,
            BorderWidth = Size * 0.03,
            BorderColor = ShapeColor,
            BackgroundColor = ShapeColor.WithAlpha(0),
            CornerRadius = Convert.ToInt32(Size / 2.0),
            Text = value,
            FontSize = FontSize,
            TextColor = TextColor
        };
    }
}