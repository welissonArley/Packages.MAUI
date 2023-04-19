using PinControl.MAUI.Helpers.Extensions;

namespace PinControl.MAUI.Views.Components.Keyboards.Base;
public abstract class KeyboardShapeViewer : KeyboardViewer
{
    public Color ShapeColor
    {
        get { return (Color)GetValue(ShapeColorProperty); }
        set { SetValue(ShapeColorProperty, value); }
    }

    public static readonly BindableProperty ShapeColorProperty = BindableProperty.Create(nameof(ShapeColor), typeof(Color), typeof(KeyboardShapeViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#BEBEBE" : "#353535"), propertyChanged: OnPropertyChanged);
}
