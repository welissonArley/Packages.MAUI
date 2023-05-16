using MauiCode.Views.Components.Keyboards.Base;

namespace MauiCode.Views.Components.Keyboards;
public class KeyboardWithoutShape : BaseKeyboardViewer
{
    public override Button CreateButton(int value)
    {
        return new Button
        {
            WidthRequest = Size,
            HeightRequest = Size,
            BackgroundColor = TextColor.WithAlpha(0),
            Text = $"{value}",
            FontSize = FontSize,
            TextColor = TextColor
        };
    }
}
