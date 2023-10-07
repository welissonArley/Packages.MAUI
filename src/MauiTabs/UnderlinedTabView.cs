using MauiTabs.Base;
using MauiTabs.Controls;

namespace MauiTabs;

public class UnderlinedTabView : TabView
{
    public Color LineColor
    {
        get => (Color)GetValue(LineColorProperty);
        set => SetValue(LineColorProperty, value);
    }

    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(UnderlinedTabView), defaultValue: Colors.Red, propertyChanged: null);

    protected override Border CreateTabContent()
    {
        var line = new LineTabView();
        FillTrigger(line);

        var layout = new Border
        {
            HorizontalOptions = LayoutOptions.Center,
            Background = Colors.Transparent,
            Padding = new Thickness(0, 15, 0, 15),
            Content = new VerticalStackLayout
            {
                Spacing = 3,
                Children =
                {
                    CreateLabel(),
                    line,
                }
            }
        };

        layout.GestureRecognizers.Add(CreateGestureRecognizer());

        return layout;
    }

    protected override Label GetLabel(Border border) => (Label)((VerticalStackLayout)border.Content).Children.First();

    private new Label CreateLabel()
    {
        var label = base.CreateLabel();
        label.Margin = new Thickness(15, 0, 15, 0);

        return label;
    }

    private void FillTrigger(LineTabView line)
    {
        line.Triggers.Clear();

        var trigger = new DataTrigger(typeof(LineTabView))
        {
            Binding = new Binding("IsSelected"),
            Value = true,
        };

        trigger.Setters.Clear();
        
        trigger.Setters.Add(new Setter
        {
            Property = LineTabView.ColorProperty,
            Value = LineColor
        });

        line.Triggers.Add(trigger);
    }
}
