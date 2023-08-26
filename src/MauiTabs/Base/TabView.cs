using MauiTabs.Entities;
using System.Collections.ObjectModel;

namespace MauiTabs.Base;

public abstract class TabView : ContentView
{
    protected Border _currentTab;
    protected readonly IList<Border> _tabs;

    public IList<Item> ItemsList
    {
        get => (IList<Item>)GetValue(ItemsListProperty);
        set => SetValue(ItemsListProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public string SelectedFontFamily
    {
        get => (string)GetValue(SelectedFontFamilyProperty);
        set => SetValue(SelectedFontFamilyProperty, value);
    }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TabView), defaultValue: Colors.Red, propertyChanged: OnTextColorPropertyChanged);
    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(TabView), defaultValue: Colors.Blue, propertyChanged: OnSelectedTextColorPropertyChanged);

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(TabView), null, propertyChanged: OnFontFamilyPropertyChanged);
    public static readonly BindableProperty SelectedFontFamilyProperty = BindableProperty.Create(nameof(SelectedFontFamily), typeof(string), typeof(TabView), null, propertyChanged: OnSelectedFontFamilyPropertyChanged);

    public static readonly BindableProperty ItemsListProperty = BindableProperty.Create(nameof(ItemsList), typeof(IList<Item>), typeof(TabView), defaultValue: new ObservableCollection<Item>(), propertyChanged: OnItemsListPropertyChanged);

    private static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabView)bindable).SetTextColor();
    private static void OnSelectedTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabView)bindable).SetSelectedTextColor();
    private static void OnItemsListPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabView)bindable).SetItems();
    private static void OnFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabView)bindable).SetFontFamily();
    private static void OnSelectedFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabView)bindable).SetSelectedFontFamily();

    protected virtual void SetItems()
    {
        var layout = (VerticalStackLayout)Content;

        layout.Children.Clear();

        layout.Children.Add(new CollectionView
        {
            BackgroundColor = Colors.Transparent,
            ItemsLayout = LinearItemsLayout.Horizontal,
            ItemsSource = ItemsList,
            ItemTemplate = new DataTemplate(CreateTabContent)
        });

        layout.Children.Add(ItemsList.First().Content);
    }

    private void SetTextColor()
    {
        if (_currentTab is not null)
        {
            var allTabs = _tabs.Where(c => c != _currentTab);
            foreach (var tab in allTabs)
                ChangeColorTextOnTab(tab, TextColor);
        }
    }

    private void SetSelectedTextColor()
    {
        if (_currentTab is not null)
            ChangeColorTextOnTab(_currentTab, SelectedTextColor);
    }

    private void SetFontFamily()
    {
        if (_currentTab is not null)
        {
            var allTabs = _tabs.Where(c => c != _currentTab);
            foreach (var tab in allTabs)
                ChangeFontTextOnTab(tab, FontFamily);
        }
    }

    private void SetSelectedFontFamily()
    {
        if (_currentTab is not null)
            ChangeFontTextOnTab(_currentTab, SelectedFontFamily);
    }

    protected abstract void ChangeColorTextOnTab(Border tab, Color changeTo);
    protected abstract void ChangeFontTextOnTab(Border tab, string fontFamily);

    protected Label CreateLabel()
    {
        var label = new Label { FontSize = 14, TextColor = TextColor, FontFamily = FontFamily };
        label.SetBinding(Label.TextProperty, new Binding("Text"));

        return label;
    }

    protected abstract Border CreateTabContent();
    protected abstract void UnselectCurrentTab();
    protected abstract void SelectTab(Border tab);

    protected TapGestureRecognizer CreateGestureRecognizer()
    {
        var tapGestureRecognizer = new TapGestureRecognizer();

        tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding("."));

        tapGestureRecognizer.Tapped += TabClickedCommand;

        return tapGestureRecognizer;
    }

    private void TabClickedCommand(object sender, TappedEventArgs e)
    {
        UnselectCurrentTab();

        var newTabSelected = (Border)sender;

        SelectTab(newTabSelected);

        _currentTab = newTabSelected;

        var layout = (VerticalStackLayout)Content;

        layout.Children.RemoveAt(1);
        layout.Children.Add(((Item)e.Parameter).Content);
    }

    public TabView()
    {
        _tabs = new List<Border>();

        Content = new VerticalStackLayout();
    }
}
