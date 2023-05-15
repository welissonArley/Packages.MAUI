using MauiCodes.Views.Components.Keyboards.Base;

namespace MauiCodes.Views.Components.Keyboards;
public class KeyboardWithoutShape : BaseKeyboardViewer
{
    protected override Button CreateButton(int value)
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

    protected override void SetSize(Button button)
    {
        button.WidthRequest = Size;
        button.HeightRequest = Size;
    }
}
