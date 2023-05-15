using Shared.Helpers.Extensions;

namespace MauiCodes.Views.Components.Keyboards.Base;
public abstract class BaseKeyboardShapeViewer : BaseKeyboardViewer
{
    public Color ShapeColor
    {
        get { return (Color)GetValue(ShapeColorProperty); }
        set { SetValue(ShapeColorProperty, value); }
    }

    public static readonly BindableProperty ShapeColorProperty = BindableProperty.Create(nameof(ShapeColor), typeof(Color), typeof(BaseKeyboardShapeViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#BEBEBE" : "#353535"), propertyChanged: OnShapeColorPropertyChanged);

    private static void OnShapeColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((BaseKeyboardShapeViewer)bindable).SetShapeColor();

    private void SetShapeColor()
    {
        var button = _layout.ElementAt(10) as Button;
        SetShapeColor(button);

        for (var index = 0; index < 9; index++)
        {
            button = _layout.ElementAt(index) as Button;
            SetShapeColor(button);
        }
    }

    protected abstract void SetShapeColor(Button button);
}
