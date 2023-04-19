using PinControl.MAUI.Views.Components.CodeViewer.Base;
using PinControl.MAUI.Views.Components.CodeViewer.Circle;
using PinControl.MAUI.Views.Components.Keyboards.Base;
using PinControl.MAUI.Views.Components.Keyboards.Circle;

namespace PinControl.MAUI.Views.Pages;

public class CodePage : ContentPage
{
    public Image Illustration
    {
        get { return (Image)GetValue(IllustrationProperty); }
        set { SetValue(IllustrationProperty, value); }
    }

    public string Title1
    {
        get { return (string)GetValue(Title1Property); }
        set { SetValue(Title1Property, value); }
    }

    public string Title2
    {
        get { return (string)GetValue(Title2Property); }
        set { SetValue(Title2Property, value); }
    }

    public CodeViewer CodeViewer
    {
        get { return (CodeViewer)GetValue(CodeViewerProperty); }
        set { SetValue(CodeViewerProperty, value); }
    }

    public KeyboardViewer KeyboardViewer
    {
        get { return (KeyboardViewer)GetValue(KeyboardViewerProperty); }
        set { SetValue(KeyboardViewerProperty, value); }
    }

    public static readonly BindableProperty IllustrationProperty = BindableProperty.Create(nameof(Illustration), typeof(Image), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty Title1Property = BindableProperty.Create(nameof(Title1), typeof(string), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty Title2Property = BindableProperty.Create(nameof(Title2), typeof(string), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(CodeViewer), typeof(CodePage), new CircleHidingCode(), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty KeyboardViewerProperty = BindableProperty.Create(nameof(KeyboardViewer), typeof(KeyboardViewer), typeof(CodePage), new KeyboardCircle(), propertyChanged: OnPropertyChanged);
    
    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).CreateContent();

    public void CreateContent()
    {
        var verticalLayout = CreateVerticalLayout();

        if (Illustration is not null)
            verticalLayout.Children.Add(Illustration);

        if (!string.IsNullOrEmpty(Title1) || !string.IsNullOrEmpty(Title2))
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
                new Label { Text = Title1, FontAttributes = FontAttributes.Bold },
                new Label { Text = Title2 }
            }
        };
    }
}