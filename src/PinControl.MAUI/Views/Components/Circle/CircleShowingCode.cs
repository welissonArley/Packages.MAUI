using Microsoft.Maui.Controls.Shapes;
using PinControl.MAUI.Views.Components.Base;

namespace PinControl.MAUI.Views.Components.Circle;

public class CircleShowingCode : ShowCodeViewer
{
    public override void CreateContent()
    {
        Content = CreateGridContent();
    }

    private Grid CreateGridContent()
    {
        var grid = CreateGridToShowContent();

        for (var index = 0; index < CodeLength; index++)
        {
            char? codeChar = Code.Length > index ? Code.ElementAt(index) : null;

            grid.Add(view: CreateCircle(codeChar.HasValue), column: index);
            grid.Add(view: CreateLabel(codeChar), column: index);
        }

        return grid;
    }

    private Ellipse CreateCircle(bool hasCodeValue)
    {
        return new Ellipse
        {
            WidthRequest = Size,
            HeightRequest = Size,
            HorizontalOptions = LayoutOptions.Start,
            Fill = hasCodeValue ? new SolidColorBrush(Color) : new SolidColorBrush(Color.WithAlpha(0.2f))
        };
    }

    private Label CreateLabel(char? codeChar = null)
    {
        return new Label
        {
            TextColor = TextColor,
            FontSize = FontSize,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = codeChar.HasValue ? $"{codeChar}" : string.Empty
        };
    }
}