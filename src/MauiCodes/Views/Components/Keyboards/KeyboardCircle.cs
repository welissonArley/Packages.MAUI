using MauiCodes.Views.Components.Keyboards.Base;

namespace MauiCodes.Views.Components.Keyboards;
public class KeyboardCircle : BaseKeyboardShapeViewer
{
    protected override Button CreateButton(int value)
    {
        return new Button
        {
            WidthRequest = Size,
            HeightRequest = Size,
            BorderWidth = BorderWidth(),
            BorderColor = ShapeColor,
            BackgroundColor = ShapeColor.WithAlpha(0),
            CornerRadius = Convert.ToInt32(Size / 2.0),
            Text = $"{value}",
            FontSize = FontSize,
            TextColor = TextColor
        };
    }

    protected override void SetShapeColor(Button button) => button.BorderColor = ShapeColor;

    protected override void SetSize(Button button)
    {
        button.WidthRequest = Size;
        button.HeightRequest = Size;
        button.BorderWidth = BorderWidth();
        button.CornerRadius = Convert.ToInt32(Size / 2.0);
    }

    private double BorderWidth() => Size * 0.03;
}