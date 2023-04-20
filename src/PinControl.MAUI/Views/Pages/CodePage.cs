using PinControl.MAUI.Views.Components.CodeViewers.Base;
using PinControl.MAUI.Views.Components.CodeViewers.Hide;
using PinControl.MAUI.Views.Components.Keyboards;
using PinControl.MAUI.Views.Components.Keyboards.Base;
using System.Text;
using System.Windows.Input;

namespace PinControl.MAUI.Views.Pages;

public class CodePage : ContentPage
{
    private string _code = string.Empty;

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

    public ICommand CallbackCodeFinished
    {
        get { return (ICommand)GetValue(CallbackCodeFinishedProperty); }
        set { SetValue(CallbackCodeFinishedProperty, value); }
    }

    public static readonly BindableProperty IllustrationProperty = BindableProperty.Create(nameof(Illustration), typeof(Image), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SubHeadlineProperty = BindableProperty.Create(nameof(SubHeadline), typeof(string), typeof(CodePage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(BaseCodeViewer), typeof(CodePage), new CircleHidingCodeViewer(), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty KeyboardViewerProperty = BindableProperty.Create(nameof(KeyboardViewer), typeof(BaseKeyboardViewer), typeof(CodePage), new KeyboardCircle(), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CallbackCodeFinishedProperty = BindableProperty.Create(nameof(CallbackCodeFinishedProperty), typeof(ICommand), typeof(CodePage), new Command(async () => await Shell.Current.GoToAsync("..")), propertyChanged: null);
    
    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).CreateContent();

    public void CreateContent()
    {
        var verticalLayout = CreateVerticalLayout();

        if (Illustration is not null)
            verticalLayout.Children.Add(Illustration);

        if (!string.IsNullOrEmpty(Headline) || !string.IsNullOrEmpty(SubHeadline))
            verticalLayout.Children.Add(CreateHeadlines());

        verticalLayout.Children.Add(CodeViewer);

        KeyboardViewer.SetCommand(new Command((value) =>
        {
            if ((int)value == -1 && !string.IsNullOrWhiteSpace(_code))
            {
                _code = _code.Remove(_code.Length - 1);

                CodeViewer.SetCode(_code);
            }
            else if ((int)value != -1 && _code.Length + 1 <= CodeViewer.CodeLength)
            {
                var sb = new StringBuilder(_code, CodeViewer.CodeLength);
                sb.Append(value);

                _code = sb.ToString();

                CodeViewer.SetCode(_code);
            }

            if (_code.Length == CodeViewer.CodeLength)
                CallbackCodeFinished?.Execute(_code);
        }));
        verticalLayout.Children.Add(KeyboardViewer);

        Content = verticalLayout;
    }

    public CodePage() => CreateContent();

    private static VerticalStackLayout CreateVerticalLayout()
    {
        return new VerticalStackLayout
        {
            Margin = new Thickness(20, 0, 20, 30),
            Spacing = 0,
            Children = { }
        };
    }

    private IView CreateHeadlines()
    {
       return new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 10,
            Margin = new Thickness(0,10, 0, 40),
            Children =
            {
                new Label { Text = Headline, FontAttributes = FontAttributes.Bold, HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center, IsVisible = !string.IsNullOrWhiteSpace(Headline) },
                new Label { Text = SubHeadline, HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center, IsVisible = !string.IsNullOrWhiteSpace(SubHeadline) }
            }
        };
    }
}