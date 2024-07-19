using PinCodes.Authorization.Views.Components.CodeViewers;
using PinCodes.Authorization.Views.Components.Keyboards;
using System.Text;

namespace PinCodes.Authorization.Views.Pages;

public partial class CodePage : ContentPage
{
    private string _code = string.Empty;

    public StackBase Header
    {
        get => (StackBase)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
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

    public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(StackBase), typeof(CodePage), null, propertyChanged: OnHeaderPropertyChanged);
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(BaseCodeViewer), typeof(CodePage), null, propertyChanged: OnCodeViewerPropertyChanged);
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(KeyboardViewer), typeof(CodePage), null, propertyChanged: OnKeyboardPropertyChanged);

    private static void OnCodeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetCodeViewer();
    private static void OnKeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetKeyboardViewer();
    private static void OnHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetPageHeader();

    public CodePage()
	{
		InitializeComponent();
	}

    private void SetPageHeader()
    {
        if (Header is not null)
        {
            LayoutHeaders.Clear();
            LayoutHeaders.Children.Add(Header);
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

    private Command CommandForKeyboard()
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