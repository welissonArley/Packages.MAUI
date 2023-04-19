using PinControl.MAUI.Views.Components.Keyboards.Base;

namespace PinControl.MAUI.Views.Components.Keyboards.NoShape;
public class KeyboardWithoutShape : KeyboardViewer
{
    public override Button CreateButton(string value)
    {
        return new Button
        {
            WidthRequest = Size,
            HeightRequest = Size,
            BackgroundColor = TextColor.WithAlpha(0),
            Text = value,
            FontSize = FontSize,
            TextColor = TextColor
        };
    }
}
