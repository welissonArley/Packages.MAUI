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

    public static readonly BindableProperty BackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(RoundedTabView), defaultValue: Colors.Transparent, propertyChanged: OnBackgroundTabColorPropertyChanged);
    public static readonly BindableProperty SelectedBackgroundTabColorProperty = BindableProperty.Create(nameof(BackgroundTabColor), typeof(Color), typeof(RoundedTabView), defaultValue: Colors.Yellow, propertyChanged: OnSelectedBackgroundTabColorPropertyChanged);

    private static void OnBackgroundTabColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((RoundedTabView)bindable).SetBackgroundColor();
    private static void OnSelectedBackgroundTabColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((RoundedTabView)bindable).SetSelectedBackgroundColor();

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

    protected override Border CreateTabContent()
    {
        var layout = new Border
        {
            BackgroundColor = BackgroundTabColor,
            Padding = 15,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10)
            },
            Margin = new Thickness(0, 0, 10, 0),
            Content = CreateLabel()
        };

        layout.GestureRecognizers.Add(CreateGestureRecognizer());

        if (_currentTab is null)
        {
            _currentTab = layout;
            SelectTab(layout);
        }

        return layout;
    }

    protected override void UnselectCurrentTab()
    {
        _currentTab.BackgroundColor = BackgroundTabColor;
        ChangeColorTextOnTab(_currentTab, TextColor);
    }

    protected override void SelectTab(Border tab)
    {
        tab.BackgroundColor = SelectedBackgroundTabColor;
        ChangeColorTextOnTab(tab, SelectedTextColor);
    }

    protected override void ChangeColorTextOnTab(Border tab, Color changeTo) => ((Label)tab.Content).TextColor = changeTo;

    protected override void ChangeFontTextOnTab(Border tab, string fontFamily) => ((Label)tab.Content).FontFamily = fontFamily;
}
