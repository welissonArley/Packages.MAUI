using MauiCode.Views.Components.Keyboards.Base;

namespace MauiCode.Views.Components.Keyboards;
public class KeyboardSquare : BaseKeyboardShapeViewer
{
    protected override Button CreateButton(int value)
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

    protected override void SetShapeColor(Button button) => button.BackgroundColor = ShapeColor;

    protected override void SetSize(Button button)
    {
        button.WidthRequest = Size;
        button.HeightRequest = Size;
        button.CornerRadius = Convert.ToInt32(Size * 0.2);
    }
}
