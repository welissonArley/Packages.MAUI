using MauiDays.Services;
using MauiDays.Views.Components;
using System.Globalization;
using System.Windows.Input;

namespace MauiDays.Views.Pages;

public class SingleDaySelectorPage : ContentPage
{
    private readonly DateOnlyService _dateOnlyService;

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

    public static readonly BindableProperty HighlightColorProperty = BindableProperty.Create(nameof(HighlightColor), typeof(Color), typeof(SingleDaySelectorPage), null, propertyChanged: OnHighlightColorPropertyChanged);
    public static readonly BindableProperty DaysWithEventsProperty = BindableProperty.Create(nameof(DaysWithEvents), typeof(IList<int>), typeof(SingleDaySelectorPage), null, propertyChanged: OnDaysWithEventsPropertyChanged);
    
    public static readonly BindableProperty OnDaySelectedCommandProperty = BindableProperty.Create(nameof(OnDaySelectedCommand), typeof(ICommand), typeof(SingleDaySelectorPage), null, propertyChanged: OnOnDaySelectedCommandPropertyChanged);

    public static readonly BindableProperty SelectedDayColorProperty = BindableProperty.Create(nameof(SelectedDayColor), typeof(Color), typeof(SingleDaySelectorPage), Colors.Yellow, propertyChanged: OnSelectedDayColorPropertyChanged);
    public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(SingleDaySelectorPage), Colors.Blue, propertyChanged: OnSelectedBackgroundColorPropertyChanged);

    public static readonly BindableProperty DaysFontFamilyProperty = BindableProperty.Create(nameof(DaysFontFamily), typeof(string), typeof(SingleDaySelectorPage), null, propertyChanged: OnDaysFontFamilyPropertyChanged);
    public static readonly BindableProperty DaysOfWeekFontFamilyProperty = BindableProperty.Create(nameof(DaysOfWeekFontFamily), typeof(string), typeof(SingleDaySelectorPage), null, propertyChanged: OnDaysOfWeekFontFamilyPropertyChanged);

    public static readonly BindableProperty CultureProperty = BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(SingleDaySelectorPage), null, propertyChanged: OnCulturePropertyChanged);
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateOnly), typeof(SingleDaySelectorPage), null, defaultBindingMode: BindingMode.OneTime, propertyChanged: OnDatePropertyChanged);
    
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateOnly?), typeof(SingleDaySelectorPage), null, propertyChanged: OnMinimumDatePropertyChanged);
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateOnly?), typeof(SingleDaySelectorPage), null, propertyChanged: OnMaximumDatePropertyChanged);
    
    public static readonly BindableProperty PrimaryColorProperty = BindableProperty.Create(nameof(PrimaryColor), typeof(Color), typeof(SingleDaySelectorPage), null, propertyChanged: OnPrimaryColorPropertyChanged);
    public static readonly BindableProperty HeaderFontFamilyProperty = BindableProperty.Create(nameof(HeaderFontFamily), typeof(string), typeof(SingleDaySelectorPage), null, propertyChanged: OnHeaderFontFamilyPropertyChanged);
    public static readonly BindableProperty MyContentProperty = BindableProperty.Create(nameof(MyContent), typeof(IView), typeof(SingleDaySelectorPage), null, propertyChanged: OnMyContentPropertyChanged);

    private static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetDate();
    private static void OnMyContentPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetMyContent();
    private static void OnCulturePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetCulture();
    private static void OnHeaderFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetHeaderFontFamily();
    private static void OnPrimaryColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetPrimaryColor();
    private static void OnMinimumDatePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetMinimumDate();
    private static void OnMaximumDatePropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetMaximumDateColor();
    private static void OnDaysOfWeekFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetDaysOfWeekFontFamily();
    private static void OnDaysFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetDaysFontFamily();
    private static void OnSelectedBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetSelectedBackgroundColor();
    private static void OnSelectedDayColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetSelectedDayColor();
    private static void OnOnDaySelectedCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetCallbackCommand();
    private static void OnHighlightColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetHighlightColor();
    private static void OnDaysWithEventsPropertyChanged(BindableObject bindable, object oldValue, object newValue) => ((SingleDaySelectorPage)bindable).SetDaysWithEvents();

    private readonly VerticalStackLayout _layout;
    private readonly CalendarHeaderComponent _headerComponent;
    private readonly CalendarSingleDaySelectorComponent _calendarComponent;

    public SingleDaySelectorPage()
    {
        _dateOnlyService = new DateOnlyService();

        _headerComponent = new(_dateOnlyService, monthCalendar: false);
        _headerComponent.SetColor(PrimaryColor);

        _calendarComponent = new(_dateOnlyService);
        _calendarComponent.SetSelectedBackgroundColor(SelectedBackgroundColor);
        _calendarComponent.SetSelectedDayColor(SelectedDayColor);

        _layout = new()
        {
            Spacing = 30,
            Children =
            {
                _headerComponent,
                _calendarComponent
            }
        };

        Content = _layout;
    }

    private void SetMyContent() => _layout.Children.Add(MyContent);
    private void SetCulture() => CheckDateAndCulture();
    private void SetHeaderFontFamily() => _headerComponent.SetFontFamily(HeaderFontFamily);
    private void SetPrimaryColor()
    {
        _headerComponent.SetColor(PrimaryColor);
        _calendarComponent.SetColor(PrimaryColor);
    }
    private void SetMinimumDate()
    {
        _headerComponent.SetMinimumDate(MinimumDate.Value);
        _calendarComponent.SetMinimumDate(MinimumDate.Value);
    }
    private void SetMaximumDateColor()
    {
        _headerComponent.SetMaximumDate(MaximumDate.Value);
        _calendarComponent.SetMaximumDate(MaximumDate.Value);
    }
    private void SetDaysOfWeekFontFamily() => _calendarComponent.SetDaysOfWeekFontFamily(DaysOfWeekFontFamily);
    private void SetDaysFontFamily() => _calendarComponent.SetDaysFontFamily(DaysFontFamily);
    private void SetDate() => CheckDateAndCulture();
    private void SetSelectedBackgroundColor() => _calendarComponent.SetSelectedBackgroundColor(SelectedBackgroundColor);
    private void SetSelectedDayColor() => _calendarComponent.SetSelectedDayColor(SelectedDayColor);
    private void SetCallbackCommand() => _calendarComponent.SetCallback(OnDaySelectedCommand);
    private void SetHighlightColor() => _calendarComponent.SetHighlightColor(HighlightColor);
    private void SetDaysWithEvents() => _calendarComponent.SetDaysWithEvents(DaysWithEvents);
    private void CheckDateAndCulture()
    {
        if (Culture is not null && Date.CompareTo(DateOnly.MinValue) != 0)
        {
            _dateOnlyService.SetDate(Date);

            _headerComponent.SetCulture(Culture);
            _headerComponent.UpdateDate();

            _calendarComponent.Build(Culture);
        }
    }
}