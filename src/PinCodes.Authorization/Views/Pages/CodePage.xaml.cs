using PinCodes.Authorization.Extensions;
using PinCodes.Authorization.Helpers;
using PinCodes.Authorization.Views.Components.CodeViewers;
using Microsoft.Maui.Accessibility;
using PinCodes.Authorization.Views.Components.Keyboards;
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

    public string ProgressAnnouncement { get => (string)GetValue(ProgressAnnouncementProperty); set => SetValue(ProgressAnnouncementProperty, value); }
    public static readonly BindableProperty ProgressAnnouncementProperty = BindableProperty.Create(nameof(ProgressAnnouncement), typeof(string), typeof(CodePage), defaultValue: "{0} of {1} entered");

    public string CodeCompletedAnnouncement { get => (string)GetValue(CodeCompletedAnnouncementProperty); set => SetValue(CodeCompletedAnnouncementProperty, value); }
    public static readonly BindableProperty CodeCompletedAnnouncementProperty = BindableProperty.Create(nameof(CodeCompletedAnnouncement), typeof(string), typeof(CodePage), defaultValue: "Code complete");

    public string CodeClearedAnnouncement { get => (string)GetValue(CodeClearedAnnouncementProperty); set => SetValue(CodeClearedAnnouncementProperty, value); }
    public static readonly BindableProperty CodeClearedAnnouncementProperty = BindableProperty.Create(nameof(CodeClearedAnnouncement), typeof(string), typeof(CodePage), defaultValue: "Code cleared");

    public bool AnnounceCodeContent { get => (bool)GetValue(AnnounceCodeContentProperty); set => SetValue(AnnounceCodeContentProperty, value); }
    public static readonly BindableProperty AnnounceCodeContentProperty = BindableProperty.Create(nameof(AnnounceCodeContent), typeof(bool), typeof(CodePage), defaultValue: false);

    private static void OnHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetPageHeader();
    private static void OnSubHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetPageSubHeader();
    private static void OnCodeViewerPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetCodeViewer();
    private static void OnKeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((CodePage)bindable).SetKeyboardViewer();

    public CodePage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        PinCodeAuthorizationCenter.ClearRequest -= OnClearRequested;
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
            var isBackspace = option == "-1";
            var previousLength = _code.Length;

            if (isBackspace && _code.NotEmpty())
                _code = _code[..^1];
            else if (!isBackspace && _code.Length < CodeViewer.CodeLength)
                _code += option;

            CodeViewer.SetCode(_code);

            if (_code.Length == CodeViewer.CodeLength)
            {
                Announce(string.Format(CodeCompletedAnnouncement, _code));
                CallbackCodeFinished?.Execute(_code);
            }
            else if (_code.Length != previousLength)
                AnnounceProgress(isBackspace ? null : option);
        });
    }

    void OnClearRequested()
    {
        CodeViewer.SetCode(string.Empty);
        _code = string.Empty;

        Announce(CodeClearedAnnouncement);
    }

    private void AnnounceProgress(string? enteredCharacter)
    {
        if (AnnounceCodeContent && enteredCharacter.NotEmpty())
            Announce(enteredCharacter);
        else
            Announce(FormatProgress());
    }

    private string FormatProgress()
    {
        try
        {
            return string.Format(ProgressAnnouncement, _code.Length, CodeViewer.CodeLength);
        }
        catch (FormatException)
        {
            return ProgressAnnouncement;
        }
    }

    private static void Announce(string text)
    {
        if (text.NotEmpty())
            SemanticScreenReader.Default.Announce(text);
    }
}