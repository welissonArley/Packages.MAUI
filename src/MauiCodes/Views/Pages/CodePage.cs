using MauiCode.Views.Components.CodeViewers.Base;
using MauiCode.Views.Components.CodeViewers.Hide;
using MauiCode.Views.Components.Keyboards;
using MauiCode.Views.Components.Keyboards.Base;
using System.Text;
using System.Windows.Input;

namespace MauiCode.Views.Pages;

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

    public static readonly BindableProperty IllustrationProperty = BindableProperty.Create(nameof(Illustration), typeof(Image), typeof(CodePage), null, propertyChanged: OnIllustrationPropertyChanged);
    public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(CodePage), null, propertyChanged: OnHeadlinePropertyPropertyChanged);
    public static readonly BindableProperty SubHeadlineProperty = BindableProperty.Create(nameof(SubHeadline), typeof(string), typeof(CodePage), null, propertyChanged: OnSubHeadlinePropertyChanged);
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(BaseCodeViewer), typeof(CodePage), new CircleHidingCodeViewer(), propertyChanged: OnCodeViewerPropertyChanged);
    public static readonly BindableProperty KeyboardViewerProperty = BindableProperty.Create(nameof(KeyboardViewer), typeof(BaseKeyboardViewer), typeof(CodePage), new KeyboardCircle(), propertyChanged: OnKeyboardViewerPropertyChanged);
    public static readonly BindableProperty CallbackCodeFinishedProperty = BindableProperty.Create(nameof(CallbackCodeFinishedProperty), typeof(ICommand), typeof(CodePage), new Command(async () => await Shell.Current.GoToAsync("..")), propertyChanged: null);
    
    private static void OnIllustrationPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetIllustration();
    private static void OnHeadlinePropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetHeadline();
    private static void OnSubHeadlinePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetSubHeadline();
    private static void OnKeyboardViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetKeyBoard();
    private static void OnCodeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetCodeViewer();

    private readonly VerticalStackLayout _layout;

    public CodePage()
    {
        _layout = CreateVerticalLayout();

        CreateContent();
    }

    private void CreateContent()
    {
        _layout.Children.Add(CreateHeadlines());

        CodeViewer.Margin = new Thickness(0, 0, 0, 30);
        _layout.Children.Add(CodeViewer);

        KeyboardViewer.SetCommand(CommandForKeyboard());
        _layout.Children.Add(KeyboardViewer);

        Content = _layout;
    }

    private void SetIllustration()
    {
        if (Illustration is not null)
            _layout.Children.Insert(0, Illustration);
    }
    private void SetHeadline()
    {
        if (string.IsNullOrWhiteSpace(Headline))
            return;

        var view = GetViewHeadlines();

        var textLabel = view.Children.ElementAt(0) as Label;
        textLabel.Text = Headline;
        textLabel.IsVisible = true;
    }
    private void SetSubHeadline()
    {
        if (string.IsNullOrWhiteSpace(SubHeadline))
            return;

        var view = GetViewHeadlines();

        var textLabel = view.Children.ElementAt(1) as Label;
        textLabel.Text = SubHeadline;
        textLabel.IsVisible = true;
    }
    private void SetKeyBoard()
    {
        KeyboardViewer.SetCommand(CommandForKeyboard());

        _layout.Children.RemoveAt(_layout.Children.Count - 1);

        _layout.Children.Add(KeyboardViewer);
    }
    private void SetCodeViewer()
    {
        _layout.Children.RemoveAt(_layout.Children.Count - 2);

        _layout.Children.Insert(_layout.Children.Count - 1, CodeViewer);
    }

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

    private VerticalStackLayout GetViewHeadlines()
    {
        if (_layout.Children.ElementAt(0) is Image)
            return _layout.Children.ElementAt(1) as VerticalStackLayout;

        return _layout.Children.ElementAt(0) as VerticalStackLayout;
    }

    private ICommand CommandForKeyboard()
    {
        return new Command((value) =>
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
        });
    }
}