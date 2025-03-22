using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using PinCodes.Authorization.Extensions;
using System;
using System.Threading;

namespace PinCodes.Authorization.Views.Components.CodeViewers;
public partial class MaskedCodeViewer : BaseCodeViewer
{
    private const ushort MIN_MASK_TIMEOUT = 250;
    private const ushort MIN_MASK_SPEED = 100;

    private readonly List<Label> _labels;
    private readonly List<Shape> _maskShapes;
    private readonly System.Timers.Timer _uiTimer;
    private int _lastIndex = -1;
    private ushort _maskSpeed = MIN_MASK_SPEED;

    public Label PinCharacterLabel
    {
        get => (Label)GetValue(PinCharacterLabelProperty);
        set => SetValue(PinCharacterLabelProperty, value);
    }

    public static readonly BindableProperty PinCharacterLabelProperty = BindableProperty.Create(nameof(PinCharacterLabel), typeof(Label), typeof(MaskedCodeViewer), null, propertyChanged: OnPinCharacterLabelPropertyChanged);

    private static void OnPinCharacterLabelPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((MaskedCodeViewer)bindable).CreateLayout();

    public Shape MaskShape
    {
        get => (Shape)GetValue(MaskShapeProperty);
        set => SetValue(MaskShapeProperty, value);
    }

    public static readonly BindableProperty MaskShapeProperty = BindableProperty.Create(nameof(MaskShape), typeof(Shape), typeof(MaskedCodeViewer), null, propertyChanged: OnMaskShapePropertyChanged);

    private static void OnMaskShapePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((MaskedCodeViewer)bindable).SetMaskShape();

    public ushort? MaskTimeout
    {
        get => (ushort?)GetValue(MaskTimeoutProperty);
        set => SetValue(MaskTimeoutProperty, value);
    }

    public static readonly BindableProperty MaskTimeoutProperty = BindableProperty.Create(nameof(MaskTimeout), typeof(ushort?), typeof(MaskedCodeViewer), null, propertyChanged: OnMaskTimeoutPropertyChanged);

    private static void OnMaskTimeoutPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((MaskedCodeViewer)bindable).SetMaskTimer((newValue as ushort?) ?? MIN_MASK_TIMEOUT);

    private void SetMaskTimer(ushort value)
    {
        _uiTimer.Interval = value;
    }

    public ushort? MaskAppearanceSpeed
    {
        get => (ushort?)GetValue(MaskSpeedProperty);
        set => SetValue(MaskSpeedProperty, value);
    }

    public static readonly BindableProperty MaskSpeedProperty = BindableProperty.Create(nameof(MaskAppearanceSpeed), typeof(ushort?), typeof(MaskedCodeViewer), null, propertyChanged: OnMaskSpeedPropertyChanged);

    private static void OnMaskSpeedPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((MaskedCodeViewer)bindable).SetMaskSpeed((newValue as ushort?) ?? MIN_MASK_SPEED);

    private void SetMaskSpeed(ushort value)
    {
        _maskSpeed = value;
    }

    public MaskedCodeViewer()
    {
        _labels = [];
        _maskShapes = [];

        _uiTimer = new System.Timers.Timer(MIN_MASK_TIMEOUT)
        {
            Enabled = false,
            AutoReset = false
        };

        _uiTimer.Elapsed += async (s, e) =>
        {
            _uiTimer.Stop();

            await HideLastDigit();
        };
    }

    private bool IsValidTimer => _uiTimer.Interval >= MIN_MASK_TIMEOUT;

    public override void SetCode(string code)
    {
        base.SetCode(code);

        if(IsValidTimer) _uiTimer.Stop();

        int lastIndex = 0;

        for (var index = 0; index < CodeLength; index++)
        {
            char? codeChar = code.Length > index ? code[index] : null;

            if (codeChar is not null)
            {
                //new digit > show 
                if (index > _lastIndex)
                {
                    SetDigit(index, $"{codeChar}");
                }

                //hide previous digit
                if (index > 0)
                {
                    HideDigit(index - 1);
                }

                lastIndex = index;
            }
            else //deleted digit > set to empty
            {
                SetDigit(index, string.Empty);
            }
        }

        _lastIndex = lastIndex;

        if (code.Length > 0 && IsValidTimer)
        {
            _uiTimer.Start();
        }

    }

    private void CreateLayout()
    {
        for (var index = 0; index < CodeLength; index++)
        {
            var container = base[index];
            var label = PinCharacterLabel.Clone();
            label.IsVisible = true;

            _labels.Add(label);
            container.Add(label);

            AbsoluteLayout.SetLayoutBounds(label, new Rect(0.5, 0.5, label.WidthRequest, label.HeightRequest));
            AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.PositionProportional);
        }
    }

    private void SetMaskShape()
    {
        for (var index = 0; index < CodeLength; index++)
        {
            var container = base[index];

            var shape = MaskShape.Clone();
            shape.IsVisible = false;

            _maskShapes.Add(shape);
            container.Add(shape);

            AbsoluteLayout.SetLayoutBounds(shape, new Rect(0.5, 0.5, shape.WidthRequest, shape.HeightRequest));
            AbsoluteLayout.SetLayoutFlags(shape, AbsoluteLayoutFlags.PositionProportional);

            ShowMask(index, show: false);
        }
    }

    public async Task HideLastDigit()
    {
        if (_lastIndex < 0) return;

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            HideDigit(_lastIndex);
        });
    }

    private void HideDigit(int index)
    {
        if (!_labels[index].IsVisible) return;

        ShowMask(index, show: true);

        _labels[index].IsVisible = false;
    }

    private void SetDigit(int index, string digit)
    {
        if (_labels[index].Text == digit) return;

        _labels[index].Text = digit;
        _labels[index].IsVisible = true;

        ShowMask(index, show: false);
    }

    private void ShowMask(int index, bool show)
    {
        _maskShapes[index].IsVisible = show;

        if (_maskSpeed >= MIN_MASK_SPEED)
        {
            if (show)
            {
                _maskShapes[index].TranslateTo(0, 0, _maskSpeed, Easing.CubicIn);
            }
            else
            {
                _maskShapes[index].TranslateTo(0, base[index].Height, 0, null);
            }
        }
    }

}
