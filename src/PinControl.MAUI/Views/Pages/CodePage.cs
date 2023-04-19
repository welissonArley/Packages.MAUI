using PinControl.MAUI.Views.Components.CodeViewers.Base;
using PinControl.MAUI.Views.Components.CodeViewers.Hide;
using PinControl.MAUI.Views.Components.Keyboards;
using PinControl.MAUI.Views.Components.Keyboards.Base;

namespace PinControl.MAUI.Views.Pages;

public class CodePage : ContentPage
{
    public Image Illustration
    {
        get { return (Image)GetValue(IllustrationProperty); }
        set { SetValue(IllustrationProperty, value); }
    }

    public string Headline
    {
        get { return (string)GetValue(HeadlineProperty); }
        set { SetValue(HeadlineProperty, value); }
    }

    public string SubHeadline
    {
        get { return (string)GetValue(SubHeadlineProperty); }
        set { SetValue(SubHeadlineProperty, value); }
    }

    public BaseCodeViewer CodeViewer
    {
        get { return (BaseCodeViewer)GetValue(CodeViewerProperty); }
        set { SetValue(CodeViewerProperty, value); }
    }

    public BaseKeyboardViewer KeyboardViewer
    {
        get { return (BaseKeyboardViewer)GetValue(KeyboardViewerProperty); }
        set { SetValue(KeyboardViewerProperty, value); }
    }

    public static readonly BindableProperty IllustrationProperty = BindableProperty.Create(nameof(Illustration), typeof(Image), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SubHeadlineProperty = BindableProperty.Create(nameof(SubHeadline), typeof(string), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(BaseCodeViewer), typeof(CodePage), new CircleHidingCodeViewer(), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty KeyboardViewerProperty = BindableProperty.Create(nameof(KeyboardViewer), typeof(BaseKeyboardViewer), typeof(CodePage), new KeyboardCircle(), propertyChanged: OnPropertyChanged);
    
    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).CreateContent();

    public void CreateContent()
    {
        var verticalLayout = CreateVerticalLayout();

        if (Illustration is not null)
            verticalLayout.Children.Add(Illustration);

        if (!string.IsNullOrEmpty(Headline) || !string.IsNullOrEmpty(SubHeadline))
            verticalLayout.Children.Add(CreateTitlePhrase());

        verticalLayout.Children.Add(CodeViewer);

        verticalLayout.Children.Add(KeyboardViewer);

        Content = verticalLayout;
    }

    public CodePage() => CreateContent();

    private static VerticalStackLayout CreateVerticalLayout()
    {
        return new VerticalStackLayout
        {
            Margin = new Thickness(20, 30, 20, 30),
            Spacing = 30,
            Children = { }
        };
    }

    private IView CreateTitlePhrase()
    {
       return new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 10,
            Children =
            {
                new Label { Text = Headline, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = SubHeadline, HorizontalOptions = LayoutOptions.Center }
            }
        };
    }
}