using MauiCodes.Views.Components.Keyboards.Base;

namespace MauiCodes.Views.Components.Keyboards;
public class KeyboardSquare : BaseKeyboardShapeViewer
{
    protected override Button CreateButton(int value)
    {
        return new Button
        {
            WidthRequest = SizeForOrientation(),
            HeightRequest = SizeForOrientation(),
            BackgroundColor = ShapeColor,
            CornerRadius = Convert.ToInt32(SizeForOrientation() * 0.2),
            Text = $"{value}",
            FontSize = FontSize,
            TextColor = TextColor
        };
    }

    protected override void SetShapeColor(Button button) => button.BackgroundColor = ShapeColor;

    protected override void SetSize(Button button)
    {
        button.WidthRequest = SizeForOrientation();
        button.HeightRequest = SizeForOrientation();
        button.CornerRadius = Convert.ToInt32(SizeForOrientation() * 0.2);
    }
}
