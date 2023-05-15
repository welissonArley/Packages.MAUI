using Shared.Helpers.Extensions;

namespace MauiCodes.Views.Components.CodeViewers.Base;
public abstract class BaseShowingCodeViewer : BaseCodeViewer
{
    protected const double FONT_SIZE = 32;
    protected const string FONT_FAMILY = "OpenSansRegular";

    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(BaseShowingCodeViewer), FONT_SIZE, propertyChanged: null);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseShowingCodeViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#FFFFFF" : "#000000"), propertyChanged: null);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(BaseShowingCodeViewer), FONT_FAMILY, propertyChanged: null);

    public override void SetCode(string code)
    {
        base.SetCode(code);

        for (var index = 0; index < CodeLength; index++)
        {
            var item = (Border) _layout.ElementAt(index);

            char? codeChar = code.Length > index ? code.ElementAt(index) : null;

            if (item.Content is null && codeChar.HasValue)
            {
                item.Background = new SolidColorBrush(Color);
                item.Content = CreateLabel(codeChar.Value);
            }
            else if (item.Content is not null && codeChar.HasValue)
                ((Label)item.Content).Text = $"{codeChar.Value}";
            else if (item.Content is not null)
            {
                item.Background = ColorWithAlpha();
                item.Content = null;
            }
        }
    }

    private Label CreateLabel(char codeChar)
    {
        return new Label
        {
            TextColor = TextColor,
            FontSize = FontSize,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = $"{codeChar}",
            FontFamily = FontFamily
        };
    }
}
