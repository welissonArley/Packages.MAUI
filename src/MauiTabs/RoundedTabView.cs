using MauiTabs.Base;
using Microsoft.Maui.Controls.Shapes;

namespace MauiTabs;

public class RoundedTabView : TabView
{
    public Color BackgroundTabColor
    {
        get { return (Color)GetValue(BackgroundTabColorProperty); }
        set { SetValue(BackgroundTabColorProperty, value); }
    }

    public Color SelectedBackgroundTabColor
    {
        get { return (Color)GetValue(SelectedBackgroundTabColorProperty); }
        set { SetValue(SelectedBackgroundTabColorProperty, value); }
    }

    public static readonly BindableProperty BackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(RoundedTabView), defaultValue: Colors.Transparent, propertyChanged: null);
    public static readonly BindableProperty SelectedBackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(RoundedTabView), defaultValue: Colors.Yellow, propertyChanged: null);

    protected override Border CreateTabContent()
    {
        var layout = new Border
        {
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = BackgroundTabColor,
            Padding = 15,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10)
            },
            Content = CreateLabel()
        };

        layout.GestureRecognizers.Add(CreateGestureRecognizer());

        FillTrigger(layout);

        return layout;
    }

    protected override Label GetLabel(Border border) => (Label)border.Content;

    private void FillTrigger(Border border)
    {
        border.Triggers.Clear();

        var trigger = new DataTrigger(typeof(Border))
        {
            Binding = new Binding("IsSelected"),
            Value = true,
        };

        trigger.Setters.Clear();
        
        trigger.Setters.Add(new Setter
        {
            Property = Border.BackgroundColorProperty,
            Value = SelectedBackgroundTabColor
        });

        border.Triggers.Add(trigger);
    }
}
