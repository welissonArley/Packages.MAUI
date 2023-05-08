using MauiDays.Services;
using MauiDays.Views.Components.Page;
using Shared.Helpers.Extensions;
using System.Globalization;
using System.Windows.Input;

namespace MauiDays.Views.Pages;

public class SingleDaySelectorPage : ContentPage
{
    public Color PrimaryColor
    {
        get { return (Color)GetValue(PrimaryColorProperty); }
        set { SetValue(PrimaryColorProperty, value); }
    }

    public Color SelectedBackgroundColor
    {
        get { return (Color)GetValue(SelectedBackgroundColorProperty); }
        set { SetValue(SelectedBackgroundColorProperty, value); }
    }

    public Color SelectedDayColor
    {
        get { return (Color)GetValue(SelectedDayColorProperty); }
        set { SetValue(SelectedDayColorProperty, value); }
    }

    public string HeaderFontFamily
    {
        get { return (string)GetValue(HeaderFontFamilyProperty); }
        set { SetValue(HeaderFontFamilyProperty, value); }
    }

    public string DaysOfWeekFontFamily
    {
        get { return (string)GetValue(DaysOfWeekFontFamilyProperty); }
        set { SetValue(DaysOfWeekFontFamilyProperty, value); }
    }

    public string DaysFontFamily
    {
        get { return (string)GetValue(DaysFontFamilyProperty); }
        set { SetValue(DaysFontFamilyProperty, value); }
    }

    public CultureInfo Culture
    {
        get { return (CultureInfo)GetValue(CultureProperty); }
        set { SetValue(CultureProperty, value); }
    }

    public DateOnly Date
    {
        get { return (DateOnly)GetValue(DateProperty); }
        set { SetValue(DateProperty, value); }
    }

    public DateOnly? MinimumDate
    {
        get { return (DateOnly?)GetValue(MinimumDateProperty); }
        set { SetValue(MinimumDateProperty, value); }
    }

    public DateOnly? MaximumDate
    {
        get { return (DateOnly?)GetValue(MaximumDateProperty); }
        set { SetValue(MaximumDateProperty, value); }
    }

    public IList<int> DaysWithEvents
    {
        get { return (IList<int>)GetValue(DaysWithEventsProperty); }
        set { SetValue(DaysWithEventsProperty, value); }
    }

    public Color HighlightColor
    {
        get { return (Color)GetValue(HighlightColorProperty); }
        set { SetValue(HighlightColorProperty, value); }
    }

    public IView MyContent
    {
        get { return (IView)GetValue(MyContentProperty); }
        set { SetValue(MyContentProperty, value); }
    }

    public ICommand OnDaySelectedCommand
    {
        get { return (ICommand)GetValue(OnDaySelectedCommandProperty); }
        set { SetValue(OnDaySelectedCommandProperty, value); }
    }

    public static readonly BindableProperty PrimaryColorProperty = BindableProperty.Create(nameof(PrimaryColor), typeof(Color), typeof(SingleDaySelectorPage), Application.Current.IsLightMode() ? Colors.Black : Colors.White, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(SingleDaySelectorPage), Application.Current.IsLightMode() ? Colors.Black : Colors.White, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty HighlightColorProperty = BindableProperty.Create(nameof(HighlightColor), typeof(Color), typeof(SingleDaySelectorPage), Application.Current.IsLightMode() ? Color.FromArgb("#DC143C") : Color.FromArgb("#F22613"), propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty SelectedDayColorProperty = BindableProperty.Create(nameof(SelectedDayColor), typeof(Color), typeof(SingleDaySelectorPage), Application.Current.IsLightMode() ? Colors.White : Colors.Black, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty HeaderFontFamilyProperty = BindableProperty.Create(nameof(HeaderFontFamily), typeof(string), typeof(SingleDaySelectorPage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty DaysOfWeekFontFamilyProperty = BindableProperty.Create(nameof(DaysOfWeekFontFamily), typeof(string), typeof(SingleDaySelectorPage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty DaysFontFamilyProperty = BindableProperty.Create(nameof(DaysFontFamily), typeof(string), typeof(SingleDaySelectorPage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty CultureProperty = BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(SingleDaySelectorPage), CultureInfo.CurrentCulture, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateOnly), typeof(SingleDaySelectorPage), DateOnly.FromDateTime(DateTime.Today), defaultBindingMode: BindingMode.OneTime, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateOnly?), typeof(SingleDaySelectorPage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateOnly?), typeof(SingleDaySelectorPage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty MyContentProperty = BindableProperty.Create(nameof(MyContent), typeof(IView), typeof(SingleDaySelectorPage), new Label { Text = "My content here", Margin = 40, HorizontalOptions = LayoutOptions.Center }, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty OnDaySelectedCommandProperty = BindableProperty.Create(nameof(OnDaySelectedCommand), typeof(ICommand), typeof(SingleDaySelectorPage), null, propertyChanged: OnPropertyChanged);
    public static readonly BindableProperty DaysWithEventsProperty = BindableProperty.Create(nameof(DaysWithEvents), typeof(IList<int>), typeof(SingleDaySelectorPage), new List<int>(), propertyChanged: OnPropertyChanged);

    protected static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((SingleDaySelectorPage)bindable).CreateContent();
    }

    public SingleDaySelectorPage() => CreateContent();

    public void CreateContent()
    {
        var date = new DateOnlyService();
        date.SetDate(Date);

        Content = new VerticalStackLayout
        {
            Spacing = 30,
            Children =
            {
                GetHeader(date),
                GetCalendar(date),
                MyContent
            }
        };
    }

    private IView GetHeader(DateOnlyService date)
	{
        return CalendarHeaderComponent
            .Instance()
            .SetDaySelectedCallback(OnDaySelectedCommand)
            .SetPrimaryColor(PrimaryColor)
            .SetBackgroundColor(BackgroundColor)
            .SetFontFamily(HeaderFontFamily)
            .SetDate(date)
            .SetCulture(Culture)
            .SetMinimumDate(MinimumDate)
            .SetMaximumDate(MaximumDate)
            .Build();
    }

    private IView GetCalendar(DateOnlyService date)
    {
        return CalendarSingleDaySelectorComponent
            .Instance()
            .SetDaySelectedCallback(OnDaySelectedCommand)
            .SetDaysWithEvents(DaysWithEvents, HighlightColor)
            .SetPrimaryColor(PrimaryColor)
            .SetCulture(Culture)
            .SetDate(date)
            .SetDaysOfWeekFontFamily(DaysOfWeekFontFamily)
            .SetDaysFontFamily(DaysFontFamily)
            .SetMinimumDate(MinimumDate)
            .SetMaximumDate(MaximumDate)
            .SetSelectedBackgroundColor(SelectedBackgroundColor)
            .SetSelectedDayColor(SelectedDayColor)
            .Build();
    }
}