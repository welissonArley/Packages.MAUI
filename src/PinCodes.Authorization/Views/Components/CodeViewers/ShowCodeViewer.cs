using PinCodes.Authorization.Extensions;

namespace PinCodes.Authorization.Views.Components.CodeViewers;
public class ShowCodeViewer : BaseCodeViewer
{
    private readonly List<Label> _labels;

    public Label PinCharacterLabel
    {
        get => (Label)GetValue(PinCharacterLabelProperty);
        set => SetValue(PinCharacterLabelProperty, value);
    }

    public static readonly BindableProperty PinCharacterLabelProperty = BindableProperty.Create(nameof(PinCharacterLabel), typeof(Label), typeof(ShowCodeViewer), null, propertyChanged: OnPinCharacterLabelPropertyChanged);

    private static void OnPinCharacterLabelPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((ShowCodeViewer)bindable).CreateLayout();

    public ShowCodeViewer()
    {
        _labels = [];
    }

    public override void SetCode(string code)
    {
        base.SetCode(code);

        if (string.IsNullOrWhiteSpace(code))
        {
            foreach (var label in _labels)
                label.Text = null;

            return;
        }

        _labels[code.Length - 1].Text = code[^1].ToString();

        if (code.Length < CodeLength)
            _labels[code.Length].Text = null;
    }

    private void CreateLayout()
    {
        var grid = Content as Grid;

        for (var index = 0; index < CodeLength; index++)
        {
            _labels.Add(PinCharacterLabel.Clone());
            grid.Add(view: _labels[index], column: index);
        }
    }
}
