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

    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(UnderlinedTabView), defaultValue: Colors.Red, propertyChanged: OnLineColorPropertyChanged);

    private static void OnLineColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((UnderlinedTabView)bindable).SetLineColor();

    private void SetLineColor()
    {
        if (_currentTab is not null)
            UnderlinedTabView.ChangeLineColor(_currentTab, LineColor);
    }

    protected override Border CreateTabContent()
    {
        var layout = new Border
        {
            BackgroundColor = Colors.Transparent,
            Padding = new Thickness(0, 15, 0, 15),
            Margin = new Thickness(0, 0, 10, 0),
            Content = new VerticalStackLayout
            {
                Spacing = 3,
                Children =
                {
                    CreateLabel(),
                    new LineTabView(),
                }
            }
        };

        layout.GestureRecognizers.Add(CreateGestureRecognizer());

        if (_currentTab is null)
        {
            _currentTab = layout;
            SelectTab(layout);
        }

        return layout;
    }

    private new Label CreateLabel()
    {
        var label = base.CreateLabel();
        label.Margin = new Thickness(15, 0, 15, 0);

        return label;
    }

    protected override void SelectTab(Border tab)
    {
        ChangeLineColor(tab, LineColor);
        ChangeColorTextOnTab(tab, SelectedTextColor);
        ChangeFontTextOnTab(tab, SelectedFontFamily);
    }

    protected override void UnselectCurrentTab()
    {
        ChangeLineColor(_currentTab, Colors.Transparent);
        ChangeColorTextOnTab(_currentTab, TextColor);
        ChangeFontTextOnTab(_currentTab, FontFamily);
    }

    protected override void ChangeColorTextOnTab(Border tab, Color changeTo) => ((Label)((VerticalStackLayout)tab.Content).Children.First()).TextColor = changeTo;

    protected override void ChangeFontTextOnTab(Border tab, string fontFamily) => ((Label)((VerticalStackLayout)tab.Content).Children.First()).FontFamily = fontFamily;

    private static void ChangeLineColor(Border tab, Color changeTo) => ((LineTabView)((VerticalStackLayout)tab.Content).Children.ElementAt(1)).Color = changeTo;
}
