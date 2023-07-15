using MauiCodes.Views.Components.CodeViewers.Base;
using MauiCodes.Views.Components.CodeViewers.Hide;
using MauiCodes.Views.Components.Keyboards;
using MauiCodes.Views.Components.Keyboards.Base;
using System.Text;
using System.Windows.Input;

namespace MauiCodes.Views.Pages;

public partial class CodePage : ContentPage
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
    private static void OnCodeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetCodeViewer();
    private static void OnKeyboardViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetKeyBoard();

    public CodePage()
	{
		InitializeComponent();

        CreateContent();
    }

    private void CreateContent()
    {
        SetCodeViewer();
        SetKeyBoard();
    }

    private void SetIllustration()
    {
        if (Illustration is not null)
        {
            IllustrationImage.Source = Illustration.Source;
            IllustrationImage.HeightRequest = Illustration.HeightRequest;
            IllustrationImage.WidthRequest = Illustration.WidthRequest;
        }
    }
    private void SetHeadline()
    {
        if (string.IsNullOrWhiteSpace(Headline))
            return;

        HeadlineLabel.Text = Headline;
        HeadlineLabel.IsVisible = true;
    }
    private void SetSubHeadline()
    {
        if (string.IsNullOrWhiteSpace(SubHeadline))
            return;

        SubheadlineLabel.Text = SubHeadline;
        SubheadlineLabel.IsVisible = true;
    }
    private void SetCodeViewer()
    {
        CodeViewerComponent.Clear();
        CodeViewerComponent.Children.Add(CodeViewer);
    }
    private void SetKeyBoard()
    {
        KeyboardViewer.SetCommand(CommandForKeyboard());

        KeyboardComponent.Clear();
        KeyboardComponent.Children.Add(KeyboardViewer);
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