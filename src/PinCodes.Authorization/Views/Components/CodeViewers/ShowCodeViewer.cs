using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;
public class ShowCodeViewer : BaseCodeViewer
{
    private List<Label> _labels;

    public Label PinCharacterLabel
    {
        get => (Label)GetValue(PinCharacterLabelProperty);
        set => SetValue(PinCharacterLabelProperty, value);
    }

    public static readonly BindableProperty PinCharacterLabelProperty = BindableProperty.Create(nameof(PinCharacterLabel), typeof(Label), typeof(ShowCodeViewer), GetDefaultLabelShape(), propertyChanged: OnPinCharacterLabelPropertyChanged);

    private static void OnPinCharacterLabelPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((ShowCodeViewer)bindable).UpdateLabelLayout();

    public ShowCodeViewer()
    {
        _labels = Enumerable.Range(0, CodeLength).Select(_ => PinCharacterLabel.Clone()).ToList();

        var grid = Content as Grid;
        for (var index = 0; index < CodeLength; index++)
            grid!.Add(view: _labels[index], column: index);
    }

    public override void SetCode(string code)
    {
        base.SetCode(code);

        for (var index = 0; index < CodeLength; index++)
        {
            var item = _labels[index];

            char? codeChar = code.Length > index ? code[index] : null;

            if (codeChar is not null)
                item.Text = $"{codeChar}";
            else
                item.Text = string.Empty;
        }
    }

    private void UpdateLabelLayout()
    {
        var grid = Content as Grid;

        for (var index = 0; index < CodeLength; index++)
        {
            _labels[index] = PinCharacterLabel.Clone();
            grid![index + CodeLength] = _labels[index];

            Grid.SetColumn(_labels[index], index);
        }
    }

    private static Label GetDefaultLabelShape()
    {
        return new Label
        {
            TextColor = Colors.Red,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };
    }
}
