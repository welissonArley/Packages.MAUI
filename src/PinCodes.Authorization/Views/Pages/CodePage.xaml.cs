using PinCodes.Authorization.Views.Components.CodeViewers;
using PinCodes.Authorization.Views.Components.Keyboards;
using System.Text;
using System.Windows.Input;

namespace PinCodes.Authorization.Views.Pages;

public partial class CodePage : ContentPage
{
    private string _code = string.Empty;

    public Image Illustration
    {
        get => (Image)GetValue(IllustrationProperty);
        set => SetValue(IllustrationProperty, value);
    }

    public Label Headline
    {
        get => (Label)GetValue(HeadlineProperty);
        set => SetValue(HeadlineProperty, value);
    }

    public Label SubHeadline
    {
        get => (Label)GetValue(SubHeadlineProperty);
        set => SetValue(SubHeadlineProperty, value);
    }

    public BaseCodeViewer CodeViewer
    {
        get => (BaseCodeViewer)GetValue(CodeViewerProperty);
        set => SetValue(CodeViewerProperty, value);
    }

    public KeyboardViewer Keyboard
    {
        get => (KeyboardViewer)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public static readonly BindableProperty IllustrationProperty = BindableProperty.Create(nameof(Illustration), typeof(Image), typeof(CodePage), null, propertyChanged: OnIllustrationPropertyChanged);
    public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(Label), typeof(CodePage), null, propertyChanged: OnHeadlinePropertyChanged);
    public static readonly BindableProperty SubHeadlineProperty = BindableProperty.Create(nameof(SubHeadline), typeof(Label), typeof(CodePage), null, propertyChanged: OnSubHeadlinePropertyChanged);
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(BaseCodeViewer), typeof(CodePage), null, propertyChanged: OnCodeViewerPropertyChanged);
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(KeyboardViewer), typeof(CodePage), null, propertyChanged: OnKeyboardPropertyChanged);

    private static void OnIllustrationPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetIllustration();
    private static void OnHeadlinePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetHeadline();
    private static void OnSubHeadlinePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetSubHeadline();
    private static void OnCodeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetCodeViewer();
    private static void OnKeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetKeyboardViewer();

    public CodePage()
	{
		InitializeComponent();
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
        if (Headline is not null)
        {
            if (LayoutHeaders.Children.Count > 0)
                LayoutHeaders.Children.Insert(0, Headline);
            else
                LayoutHeaders.Children.Add(Headline);
        }
    }

    private void SetSubHeadline()
    {
        if (SubHeadline is not null)
        {
            LayoutHeaders.Children.Add(SubHeadline);
        }
    }

    private void SetCodeViewer()
    {
        if(CodeViewer is not null)
        {
            CodeViewerComponent.Clear();
            CodeViewerComponent.Children.Add(CodeViewer);
        }
    }

    private void SetKeyboardViewer()
    {
        if (Keyboard is not null)
        {
            KeyboardComponent.Clear();
            Keyboard.SetCommandWhenUserPressButtonOnKeyboard(CommandForKeyboard());
            KeyboardComponent.Children.Add(Keyboard);
        }
    }

    private ICommand CommandForKeyboard()
    {
        return new Command((value) =>
        {
            var option = (int)value;

            if (option == -1 && !string.IsNullOrWhiteSpace(_code))
            {
                _code = _code.Remove(_code.Length - 1);

                CodeViewer.SetCode(_code);
            }
            else if (option != -1 && _code.Length + 1 <= CodeViewer.CodeLength)
            {
                var sb = new StringBuilder(_code, CodeViewer.CodeLength);
                sb.Append(value);

                _code = sb.ToString();

                CodeViewer.SetCode(_code);
            }

            /*if (_code.Length == CodeViewer.CodeLength)
                CallbackCodeFinished?.Execute(_code);*/
        });
    }
}