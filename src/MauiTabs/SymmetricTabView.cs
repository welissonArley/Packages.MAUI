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
    public static readonly BindableProperty BackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(SymmetricTabView), defaultValue: Colors.Transparent, propertyChanged: OnBackgroundTabColorPropertyChanged);
    public static readonly BindableProperty SelectedBackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(SymmetricTabView), defaultValue: Colors.Yellow, propertyChanged: OnSelectedBackgroundTabColorPropertyChanged);

    private static void OnBackgroundComponentColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SymmetricTabView)bindable).SetBackgroundComponentColor();
    private static void OnBackgroundTabColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SymmetricTabView)bindable).SetBackgroundColor();
    private static void OnSelectedBackgroundTabColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SymmetricTabView)bindable).SetSelectedBackgroundColor();

    private void SetBackgroundComponentColor()
    {
        var layout = (VerticalStackLayout)Content;
        if (layout.Children.Any())
        {
            var content = (Border)layout.Children.First();
            content.BackgroundColor = BackgroundComponentColor;
        }
    }

    private void SetBackgroundColor()
    {
        if (_currentTab is not null)
        {
            var allTabs = _tabs.Where(c => c != _currentTab);
            foreach (var tab in allTabs)
                tab.BackgroundColor = BackgroundTabColor;
        }
    }

    private void SetSelectedBackgroundColor()
    {
        if (_currentTab is not null)
            _currentTab.BackgroundColor = SelectedBackgroundTabColor;
    }

    protected override void SetItems()
    {
        var layout = (VerticalStackLayout)Content;
        layout.Children.Clear();

        var grid = new Grid { ColumnSpacing = 0 };

        for(var index = 0; index < ItemsList.Count; index++)
        {
            var tab = ItemsList[index];

            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var contentForTab = CreateTabContent();
            
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

        layout.Children.Add(ItemsList.First().Content);
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

        layout.GestureRecognizers.Add(CreateGestureRecognizer());

        if (_currentTab is null)
        {
            _currentTab = layout;
            SelectTab(layout);
        }

        return layout;
    }

    protected override void SelectTab(Border tab)
    {
        tab.BackgroundColor = SelectedBackgroundTabColor;
        ChangeColorTextOnTab(tab, SelectedTextColor);
        ChangeFontTextOnTab(tab, SelectedFontFamily);
    }

    protected override void UnselectCurrentTab()
    {
        _currentTab.BackgroundColor = BackgroundTabColor;
        ChangeColorTextOnTab(_currentTab, TextColor);
        ChangeFontTextOnTab(_currentTab, FontFamily);
    }

    protected override void ChangeColorTextOnTab(Border tab, Color changeTo) => ((Label)tab.Content).TextColor = changeTo;

    protected override void ChangeFontTextOnTab(Border tab, string fontFamily) => ((Label)tab.Content).FontFamily = fontFamily;
}
