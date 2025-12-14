using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;
public sealed class MaskedCodeViewer : BaseCodeViewer
{
    private readonly List<Label> _labels;
    private readonly List<View> _maskContent;
    private readonly List<CancellationTokenSource> _cancellationTokenSources;

    public Label PinCharacterLabel { get => (Label)GetValue(PinCharacterLabelProperty); set => SetValue(PinCharacterLabelProperty, value); }
    public static readonly BindableProperty PinCharacterLabelProperty = BindableProperty.Create(nameof(PinCharacterLabel), typeof(Label), typeof(ShowCodeViewer), propertyChanged: OnPinCharacterLabelPropertyChanged);

    public TimeSpan HideCodeAfter { get => (TimeSpan) GetValue(HideAfterProperty); set => SetValue(HideAfterProperty, value); }
    public static readonly BindableProperty HideAfterProperty = BindableProperty.Create(nameof(HideCodeAfter), typeof(TimeSpan), typeof(MaskedCodeViewer), defaultValue: new TimeSpan(hours: 0, minutes: 0, seconds: 1));

    public View MaskContent { get => (View)GetValue(MaskContentProperty); set => SetValue(MaskContentProperty, value); }
    public static readonly BindableProperty MaskContentProperty = BindableProperty.Create(nameof(MaskContent), typeof(View), typeof(MaskedCodeViewer), propertyChanged: OnMaskContentPropertyChanged);

    private static void OnPinCharacterLabelPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((MaskedCodeViewer)bindable).CreateLayout();
    private static void OnMaskContentPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((MaskedCodeViewer)bindable).AddMaskedContent();

    public MaskedCodeViewer()
    {
        _labels = [];
        _maskContent = [];
        _cancellationTokenSources = [];
    }

    public override void SetCode(string code)
    {
        base.SetCode(code);

        if (code.IsEmpty())
        {
            for (var index = 0; index < _labels.Count; index++)
            {
                if (_labels[index].Text.NotEmpty())
                {
                    _cancellationTokenSources[index].Cancel();

                    _labels[index].Text = null;
                    _labels[index].Opacity = 1;

                    _maskContent[index].IsVisible = false;

                    _cancellationTokenSources[index].Dispose();
                    _cancellationTokenSources[index] = new CancellationTokenSource();
                }
            }

            return;
        }

        if (code.Length < CodeLength)
        {
            var label = _labels[code.Length];

            if(label.Text.NotEmpty())
            {
                _cancellationTokenSources[code.Length].Cancel();

                label.Text = null;
                label.Opacity = 1;

                _maskContent[code.Length].IsVisible = false;

                _cancellationTokenSources[code.Length].Dispose();
                _cancellationTokenSources[code.Length] = new CancellationTokenSource();
            }
        }

        var current = _labels[code.Length - 1];
        if(current.Opacity == 1)
        {
            current.Text = code[^1].ToString();

            _ = HideAfter(label: current, index: code.Length - 1);
        }
    }

    private void CreateLayout()
    {
        var grid = Content as Grid;

        for (var index = 0; index < CodeLength; index++)
        {
            _labels.Add(PinCharacterLabel.Clone());

            _cancellationTokenSources.Add(new CancellationTokenSource());

            grid.Add(view: _labels[index], column: index);
        }

        if (_maskContent.Count == 0 && MaskContent is not null)
            AddMaskedContent();
    }

    private void AddMaskedContent()
    {
        if (_labels.Count == 0)
            return;

        var grid = Content as Grid;
        for (var index = 0; index < CodeLength; index++)
        {
            _maskContent.Add(MaskContent.Clone());
            _maskContent[index].IsVisible = false;

            grid.Add(view: _maskContent[index], column: index);
        }
    }

    private async Task HideAfter(Label label, int index)
    {
        await Task.Delay(HideCodeAfter, _cancellationTokenSources[index].Token).ConfigureAwait(false);

        if (_cancellationTokenSources[index].IsCancellationRequested)
            return;

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            if (_cancellationTokenSources[index].IsCancellationRequested)
                return;

            label.Opacity = 0;

            if (_maskContent.Count != 0)
                _maskContent[index].IsVisible = true;
        });
    }
}
