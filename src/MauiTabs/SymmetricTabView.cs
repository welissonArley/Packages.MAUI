using MauiTabs.Base;
using Microsoft.Maui.Controls.Shapes;

namespace MauiTabs;

public class SymmetricTabView : TabView
{
    public Color BackgroundComponentColor
    {
        get { return (Color)GetValue(BackgroundComponentColorProperty); }
        set { SetValue(BackgroundComponentColorProperty, value); }
    }

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

    public static readonly BindableProperty BackgroundComponentColorProperty = BindableProperty.Create(nameof(BackgroundComponentColor), typeof(Color), typeof(SymmetricTabView), defaultValue: Colors.Red, propertyChanged: OnBackgroundComponentColorPropertyChanged);
    public static readonly BindableProperty BackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(SymmetricTabView), defaultValue: Colors.Transparent, propertyChanged: null);
    public static readonly BindableProperty SelectedBackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(SymmetricTabView), defaultValue: Colors.Yellow, propertyChanged: null);

    private static void OnBackgroundComponentColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SymmetricTabView)bindable).SetBackgroundComponentColor();

    private void SetBackgroundComponentColor()
    {
        var layout = (VerticalStackLayout)Content;
        if (layout.Children.Any())
        {
            var content = (Border)layout.Children.First();
            content.BackgroundColor = BackgroundComponentColor;
        }
    }

    protected override void SetItems()
    {
        var layout = (VerticalStackLayout)Content;
        layout.Children.Clear();

        EnforceSingleTrue();

        var grid = new Grid { ColumnSpacing = 0 };

        for(var index = 0; index < ItemsList.Count; index++)
        {
            var tab = ItemsList[index];

            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var contentForTab = CreateTabContent();
            contentForTab.BindingContext = tab;
            
            ((Label)contentForTab.Content).Text = tab.Text;

            ((TapGestureRecognizer)contentForTab.GestureRecognizers.First()).BindingContext = tab;

            grid.Add(contentForTab, row: 0, column: index);
        }

        layout.Children.Add(new Border
        {
            BackgroundColor = BackgroundComponentColor,
            Padding = 3,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10)
            },
            Content = grid
        });

        var selectedTab = ItemsList.First(c => c.IsSelected);
        layout.Children.Add(selectedTab.Content);
    }

    protected override Border CreateTabContent()
    {
        var label = CreateLabel();
        label.RemoveBinding(Label.TextProperty);
        label.HorizontalOptions = LayoutOptions.Center;
        label.VerticalOptions = LayoutOptions.Center;

        var layout = new Border
        {
            BackgroundColor = BackgroundTabColor,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10)
            },
            Padding = new Thickness(horizontalSize: 0, verticalSize: 12),
            Content = label
        };

        FillTrigger(layout);

        layout.GestureRecognizers.Add(CreateGestureRecognizer());

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