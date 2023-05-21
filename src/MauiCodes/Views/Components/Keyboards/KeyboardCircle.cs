using MauiCodes.Views.Components.Keyboards.Base;

namespace MauiCodes.Views.Components.Keyboards;
public class KeyboardCircle : BaseKeyboardShapeViewer
{
    protected override Button CreateButton(int value)
    {
        return new Button
        {
            WidthRequest = SizeForOrientation(),
            HeightRequest = SizeForOrientation(),
            BorderWidth = BorderWidth(),
            BorderColor = ShapeColor,
            BackgroundColor = ShapeColor.WithAlpha(0),
            CornerRadius = Convert.ToInt32(SizeForOrientation() / 2.0),
            Text = $"{value}",
            FontSize = FontSize,
            TextColor = TextColor
        };
    }

    protected override void SetShapeColor(Button button) => button.BorderColor = ShapeColor;

    protected override void SetSize(Button button)
    {
        button.WidthRequest = SizeForOrientation();
        button.HeightRequest = SizeForOrientation();
        button.BorderWidth = BorderWidth();
        button.CornerRadius = Convert.ToInt32(SizeForOrientation() / 2.0);
    }

    private double BorderWidth() => SizeForOrientation() * 0.03;
}