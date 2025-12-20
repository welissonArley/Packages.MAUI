using PinCodes.Authorization.Extensions;
using PinCodes.Authorization.Helpers;
using PinCodes.Authorization.Views.Components.CodeViewers;
using PinCodes.Authorization.Views.Components.Keyboards;
using System.Text;
using System.Windows.Input;

namespace PinCodes.Authorization.Views.Pages;

public partial class CodePage : ContentPage
{
    private string _code = string.Empty;

    public ICommand CallbackCodeFinished { get => (ICommand)GetValue(CallbackCodeFinishedProperty); set => SetValue(CallbackCodeFinishedProperty, value); }
    public static readonly BindableProperty CallbackCodeFinishedProperty = BindableProperty.Create(nameof(CallbackCodeFinished), typeof(ICommand), typeof(CodePage));

    public LayoutOptions VerticalOptions { get => (LayoutOptions)GetValue(VerticalOptionsProperty); set => SetValue(VerticalOptionsProperty, value); }
    public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(nameof(VerticalOptions), typeof(LayoutOptions), typeof(CodePage), defaultValue: LayoutOptions.Start);

    public LayoutOptions HorizontalOptions { get => (LayoutOptions)GetValue(HorizontalOptionsProperty); set => SetValue(HorizontalOptionsProperty, value); }
    public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create(nameof(HorizontalOptions), typeof(LayoutOptions), typeof(CodePage), defaultValue: LayoutOptions.Center);

    public View Header { get => (View)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(View), typeof(CodePage), propertyChanged: OnHeaderPropertyChanged);

    public View SubHeader { get => (StackBase)GetValue(SubHeaderProperty); set => SetValue(SubHeaderProperty, value); }
    public static readonly BindableProperty SubHeaderProperty = BindableProperty.Create(nameof(SubHeader), typeof(View), typeof(CodePage), propertyChanged: OnSubHeaderPropertyChanged);

    public BaseCodeViewer CodeViewer { get => (BaseCodeViewer)GetValue(CodeViewerProperty); set => SetValue(CodeViewerProperty, value); }
    public static readonly BindableProperty CodeViewerProperty = BindableProperty.Create(nameof(CodeViewer), typeof(BaseCodeViewer), typeof(CodePage), propertyChanged: OnCodeViewerPropertyChanged);

    public KeyBoardViewerBase Keyboard { get => (KeyBoardViewerBase)GetValue(KeyboardProperty); set => SetValue(KeyboardProperty, value); }
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(KeyBoardViewerBase), typeof(CodePage), propertyChanged: OnKeyboardPropertyChanged);

    private static void OnHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetPageHeader();
    private static void OnSubHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetPageSubHeader();
    private static void OnCodeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetCodeViewer();
    private static void OnKeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetKeyboardViewer();

    public CodePage()
	{
		InitializeComponent();

        PinCodeAuthorizationCenter.ClearRequest += OnClearRequested;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        PinCodeAuthorizationCenter.ClearRequest -= OnClearRequested;
    }

    private void SetPageHeader()
    {
        if (Header is not null)
        {
            LayoutHeader.Clear();
            LayoutHeader.Children.Add(Header);
        }
    }

    private void SetPageSubHeader()
    {
        if (SubHeader is not null)
        {
            LayoutSubHeader.Clear();
            LayoutSubHeader.Children.Add(SubHeader);
        }
    }

    private void SetCodeViewer()
    {
        if (CodeViewer is not null)
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
            var option = (string)value;

            if (option.Equals("-1") && _code.NotEmpty())
                _code = _code.Remove(_code.Length - 1);
            else if (option.Equals("-1").IsFalse() && _code.Length + 1 <= CodeViewer.CodeLength)
            {
                var sb = new StringBuilder(_code, CodeViewer.CodeLength);
                sb.Append(value);

                _code = sb.ToString();
            }

            CodeViewer.SetCode(_code);

            if (_code.Length == CodeViewer.CodeLength)
                CallbackCodeFinished?.Execute(_code);
        });
    }

    void OnClearRequested()
    {
        CodeViewer.SetCode(string.Empty);
        _code = string.Empty;
    }
}