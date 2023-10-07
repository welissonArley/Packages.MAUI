using MauiTabs.Entities;
using System.Collections.ObjectModel;

namespace MauiTabs.Base;

public abstract class TabView : ContentView
{
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

    public int SpacingBetweenTabAndContent
    {
        get => (int)GetValue(SpacingBetweenTabAndContentProperty);
        set => SetValue(SpacingBetweenTabAndContentProperty, value);
    }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TabView), defaultValue: Colors.Red, propertyChanged: null);
    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(TabView), defaultValue: Colors.Blue, propertyChanged: null);

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(TabView), null, propertyChanged: null);
    public static readonly BindableProperty SelectedFontFamilyProperty = BindableProperty.Create(nameof(SelectedFontFamily), typeof(string), typeof(TabView), null, propertyChanged: null);

    public static readonly BindableProperty ItemsListProperty = BindableProperty.Create(nameof(ItemsList), typeof(IList<Item>), typeof(TabView), defaultValue: new ObservableCollection<Item>(), propertyChanged: OnItemsListPropertyChanged);
    
    public static readonly BindableProperty SpacingBetweenTabAndContentProperty = BindableProperty.Create(nameof(SpacingBetweenTabAndContent), typeof(int), typeof(TabView), defaultValue: 20, propertyChanged: OnSpacingBetweenTabAndContentPropertyChanged);

    private static void OnItemsListPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((TabView)bindable).SetItems();

    private static void OnSpacingBetweenTabAndContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var content = (VerticalStackLayout)((TabView)bindable).Content;

        content.Spacing = (int)newValue;
    }

    protected virtual void SetItems()
    {
        var layout = (VerticalStackLayout)Content;

        layout.Children.Clear();

        EnforceSingleTrue();

        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition());

        grid.Add(new CollectionView
        {
            BackgroundColor = Colors.Transparent,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 10
            },
            ItemsSource = ItemsList,
            ItemTemplate = new DataTemplate(CreateTabContent)
        }, row: 0, column: 0);

        layout.Children.Add(grid);

        var selectedTab = ItemsList.First(c => c.IsSelected);
        layout.Children.Add(selectedTab.Content);
    }

    protected Label CreateLabel()
    {
        var label = new Label { FontSize = 14, TextColor = TextColor, FontFamily = FontFamily };
        label.SetBinding(Label.TextProperty, new Binding("Text"));

        FillTrigger(label);

        return label;
    }

    protected abstract Border CreateTabContent();

    protected abstract Label GetLabel(Border border);

    protected TapGestureRecognizer CreateGestureRecognizer()
    {
        var tapGestureRecognizer = new TapGestureRecognizer();

        tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding("."));

        tapGestureRecognizer.Tapped += TabClickedCommand;

        return tapGestureRecognizer;
    }

    protected void EnforceSingleTrue()
    {
        var selectedItem = ItemsList.FirstOrDefault(c => c.IsSelected);
        if (selectedItem is null)
            ItemsList.First().IsSelected = true;
        else
        {
            foreach(var item in ItemsList)
                item.IsSelected = false;

            selectedItem.IsSelected = true;
        }
    }

    private void TabClickedCommand(object sender, TappedEventArgs e)
    {
        ItemsList.First(c => c.IsSelected).IsSelected = false;

        var newTabSelected = (Border)sender;

        ItemsList.First(c => c.Text.Equals(GetLabel(newTabSelected).Text)).IsSelected = true;

        var layout = (VerticalStackLayout)Content;

        layout.Children.RemoveAt(1);
        layout.Children.Add(((Item)e.Parameter).Content);
    }

    private void FillTrigger(Label label)
    {
        label.Triggers.Clear();

        var trigger = new DataTrigger(typeof(Label))
        {
            Binding = new Binding("IsSelected"),
            Value = true,
        };

        trigger.Setters.Clear();
        
        trigger.Setters.Add(new Setter
        {
            Property = Label.TextColorProperty,
            Value = SelectedTextColor
        });

        trigger.Setters.Add(new Setter
        {
            Property = Label.FontFamilyProperty,
            Value = SelectedFontFamily
        });

        label.Triggers.Add(trigger);
    }

    public TabView() => Content = new VerticalStackLayout { Spacing = 20 };
}
