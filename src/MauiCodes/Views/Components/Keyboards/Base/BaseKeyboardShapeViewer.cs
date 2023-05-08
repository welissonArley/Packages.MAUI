using Shared.Helpers.Extensions;

namespace MauiCode.Views.Components.Keyboards.Base;
public abstract class BaseKeyboardShapeViewer : BaseKeyboardViewer
{
    public Color ShapeColor
    {
        get { return (Color)GetValue(ShapeColorProperty); }
        set { SetValue(ShapeColorProperty, value); }
    }

    public static readonly BindableProperty ShapeColorProperty = BindableProperty.Create(nameof(ShapeColor), typeof(Color), typeof(BaseKeyboardShapeViewer), Color.FromArgb(Application.Current.IsLightMode() ? "#BEBEBE" : "#353535"), propertyChanged: OnPropertyChanged);
}
