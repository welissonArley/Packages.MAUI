<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#mauidays-nuget-package-for-month-and-day-calendar-in-net-maui">About The Project</a>
      <ul>
        <li><a href="#compatibility">Compatibility</a></li>
        <li><a href="#features">Features</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#usage">Usage</a></li>
        <ul>
          <li><a href="#popups">Popups</a></li>
          <ul>
            <li><a href="#customizable-properties-for-calendar-mopups">Customizable properties for Calendar Mopups</a></li>    
          </ul>
          <li><a href="#pages">Pages</a></li>
          <ul>
            <li><a href="#customizable-properties-for-calendar-pages">Customizable properties for Calendar Pages</a></li>    
          </ul>
        </ul>      
      </ul>
    </li>
  </ol>
</details>

# MAUIDays NuGet Package for Month and Day Calendar in .NET MAUI

MauiDays is a .NET MAUI library that provides a customizable month and day calendar popup selector, along with a calendar page. It allows developers to easily add date selection functionality to their .NET MAUI apps.

The library has a dependency on [Mopups][mopups-url], which is used for the month and day calendar popup selector functionality. Note that if you only need the calendar page, you do not need to install Mopups.

![Calendar Example Screenshot][hero-image]

### **Compatibility**

| Platform | Version | Availability |
| --- | --- | --- |
| iOS | 14.0 and higher | ✅ |
| Android | 5.0 and higher | ✅ |
| UWP | 10.0.17763.0 and higher | ✅ |
| macOS | 10.15 and higher | ✅ |

### **Features**

- **Single Day Selector popup:** your users can easily select a specific day of the month to view or schedule events.
- **Single Month Selector popup:** similarly, the Month Selector popup allows users to quickly select a specific month and year to view or schedule events.
- **Calendar Page:** displays a day calendar along with a customizable view below it.

## Getting Started

### Installation

To use this package, simply install it in your .NET MAUI project using NuGet. In Visual Studio, you can do this by right-clicking on your project and selecting "Manage NuGet Packages". From there, search for "MauiDays" and install the latest version.

```powershell
Install-Package MauiDays
```

### Usage

#### Popups

Please, remember that you need to install the Mopups nuget package. This dependency is necessary to use the popups.

```powershell
Install-Package Mopups
```

You can use the Calendar popups with a fluent syntax, a programming style that focuses on making code more readable. By using the fluent syntax, it becomes easier for the developer to understand and modify the behavior of the popups, making it a more user-friendly and efficient approach to programming.

Keep in mind that:

1. You should always start by calling the **Instance** function, which takes a command as an argument. This command will be triggered when the user clicks the confirm button on the selector popups.
2. You should always finish by calling the **Build** function to receive the instance of the popup.

Here's an example:

```csharp
var popup = SingleDaySelectorCalendarPopup
            .Instance(async (date) =>
            {
                await OnDateChanged(date);
            })
            .Build();

await MopupService.Instance.PushAsync(popup);
```

and the same for the Month selector:

```csharp
var popup = SingleMonthSelectorCalendarPopup
            .Instance(async (month) =>
            {
                await OnDateChanged(month);
            })
            .Build();

await MopupService.Instance.PushAsync(popup);
```

Below, you will find a list of available options for the Day Selector popup and Month Selector popup:

- **SingleDaySelectorCalendarPopup:** the popup allows the user to select only one day.
- **SingleMonthSelectorCalendarPopup:** popup with the option to select a single month.

##### Customizable properties for Calendar Mopups

- **Date:** allows you to set an initial date for the calendar.
- **Culture:** allows you to set the calendar culture, which can affect the formatting of dates and the names of months and weekdays displayed in the calendar popups.
- **PrimaryColor:** allows you to customize the color scheme of the calendar and selectors to match your app's branding.
- **SelectedMonthColor/SelectedDayColor:** allows you to change the text color that is displayed when a user selects a day/month on the calendar.
- **SelectedBackgroundColor:** allows you to change the background color that is displayed when a user selects a day/month on the calendar.
- **MinimumDate:** You may use this property to set the minimum selectable date on the calendar.
- **MaximumDate:** You may use this property to set the maximum selectable date on the calendar.
- **DaysOfWeekFontFamily:** allows you to customize the font family for the days of the week in the calendar.
- **DaysFontFamily/MonthsFontFamily:** allowd you to set the font family for the text displayed for the days/month on the popups.
- **HeaderFontFamily:** allows you to customize the font family for the calendar's header, that means the month year displayed
- **TextCancel:**  is a property that allows you to customize the text for the cancel button.
- **SetCancelFontFamily:** allows you to set the font family for the cancel button text.
- **ConfirmButtonColor:** is a property that allows you to customize the color of the confirm button on the popup. Keep in mind the contrast ration, because the text color for this button is always the PrimaryColor.
- **CalendarBackgroundColor:** property allows you to set the background color of the calendar page.
- **PopupBackgroundColor:** property allows you to set the background color of the popups.

Now that you know all properties to customize your popups, let's see an example with the fluent syntax:

```csharp
public static class CalendarPopupBuilder
{
    public static SingleMonthSelectorCalendarPopup SingleMonth(Action<DateOnly> callbackConfirm)
    {
        return SingleMonthSelectorCalendarPopup
            .Instance(callbackConfirm)
            .PopupBackgroundColor(PopupBackgroundColor())
            .SetCancelFontFamily(CancelFontFamily())
            .SetHeaderFontFamily(HeaderFontFamily())
            .SetMonthFontFamily(LabelFontFamily())
            .CalendarBackgroundColor(CalendarBackgroundColor())
            .SetPrimaryColor(PrimaryColor())
            .SetConfirmButtonColor(ColorForConfirmButton())
            .SetSelectedBackgroundColor(SelectedBackgroundColor())
            .SetSelectedMonthColor(SelectedLabelColor());
    }

    private static Color ColorForConfirmButton() => Color.FromArgb(Application.Current.IsLightMode() ? "#40806A" : "#00D46A");
    private static Color PrimaryColor() => Application.Current.IsLightMode() ? Colors.Black : Colors.White;
    private static Color SelectedBackgroundColor() => Application.Current.IsLightMode() ? Colors.Black : Colors.White;
    private static Color SelectedLabelColor() => Application.Current.IsLightMode() ? Colors.White : Colors.Black;
    private static Color CalendarBackgroundColor() => Application.Current.IsLightMode() ? Colors.White : Application.Current.GetDarkMode();
    private static Color PopupBackgroundColor() => Color.FromArgb("#80A1A1A1");
    private static string CancelFontFamily() => "OpenSansRegular";
    private static string HeaderFontFamily() => "OpenSansSemibold";
    private static string LabelFontFamily() => "OpenSansRegular";
}


public partial class CalendarDashboardViewModel : ObservableObject
{
    [RelayCommand]
    public static async Task SingleMonth()
    {
        var today = DateTime.Today;

        var popup = CalendarPopupBuilder
            .SingleMonth((date) =>
            {
                //do something with the date
            })
            .SetMinimumDate(new DateOnly(today.Year - 1, today.Month, 7))
            .SetMaximumDate(new DateOnly(today.Year + 1, today.Month, 7))
            .Build();

        await MopupService.Instance.PushAsync(popup);
    }
}
```

#### Pages

My customizable calendar page give you the freedom to create a personalized interface with a customizable calendar and IView of your choice. The page receive as parameter a command that triggers every time a user selects a day. This powerful combination allows you displaying detailed information about a selected day, or triggering a specific event.

Here's an example:

```xaml
<page:SingleDaySelectorPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:page="clr-namespace:MauiDays.Views.Pages;assembly=MauiDays"
    x:Class="Packages.MAUI.App.Views.Calendar.SingleDaySelectorPage"
    OnDaySelectedCommand="{Binding SelectedDayCommand}">

   <page:SingleDaySelectorPage.MyContent>
        <VerticalStackLayout Margin="0,40,0,0">
            <Label Text="YOU CAN CREATE ANY KIND OF IVIEW INSIDE THE PROPERTY MyContent" />
        </VerticalStackLayout>
    </page:SingleDaySelectorPage.MyContent>

</page:SingleDaySelectorPage>
```

don't forget to change the Code-behind

```csharp
public partial class SingleDaySelectorPage : MauiDays.Views.Pages.SingleDaySelectorPage
{
  public SingleDaySelectorPage()
	{
		InitializeComponent();
	}
}
```

##### Customizable properties for Calendar Pages

- **Date:** allows you to set an initial date for the calendar.
- **Culture:** allows you to set the calendar culture, which can affect the formatting of dates and the names of months and weekdays displayed in the calendar popups.
- **PrimaryColor:** allows you to customize the color scheme of the calendar and selectors to match your app's branding.
- **SelectedDayColor:** allows you to change the text color that is displayed when a user selects a day on the calendar.
- **SelectedBackgroundColor:** allows you to change the background color that is displayed when a user selects a day/month on the calendar.
- **MinimumDate:** You may use this property to set the minimum selectable date on the calendar.
- **MaximumDate:** You may use this property to set the maximum selectable date on the calendar.
- **HeaderFontFamily:** allows you to customize the font family for the calendar's header, that means the month year displayed
- **DaysOfWeekFontFamily:** allows you to customize the font family for the days of the week in the calendar.
- **DaysFontFamily:** allows you to set the font family for the text displayed for the days on the popups.
- **DaysWithEvents:** a list of int that represents all days with some event, that means, you want to put attention on this days on the current month.
- **HighlightColor:** allows you change the color for bring attention to event-filled days.
- **MyContent:** the versatile property that lets you add any content you desire.
- **OnDaySelectedCommand:** command that you trigger automatically when the user choose a day.

Now that you know all properties to customize your calendar pages, let's see an example:

```xaml
<page:SingleDaySelectorPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:page="clr-namespace:MauiDays.Views.Pages;assembly=MauiDays"
    xmlns:viewModel="clr-namespace:Packages.MAUI.App.ViewModels.Calendar"
    xmlns:s="clr-namespace:System;assembly=mscorlib"
    x:Class="Packages.MAUI.App.Views.Calendar.SingleDaySelectorPage"
    x:DataType="viewModel:SingleDaySelectorViewModel"
    OnDaySelectedCommand="{Binding SelectedDayCommand}"
    MinimumDate="{Binding MinimumDate}"
    MaximumDate="{Binding MaximumDate}"
    PrimaryColor="{AppThemeBinding Light=Black, Dark=White}"
    SelectedBackgroundColor="{AppThemeBinding Light=Black, Dark=White}"
    SelectedDayColor="{AppThemeBinding Light=White, Dark=Black}"
    HeaderFontFamily="OpenSansRegular"
    DaysOfWeekFontFamily="OpenSansRegular"
    DaysFontFamily="OpenSansRegular"
    DaysWithEvents="{Binding DaysWithEvents}"
    HighlightColor="Blue"
    Date="{Binding Date}">

   <page:SingleDaySelectorPage.MyContent>
        <VerticalStackLayout Margin="0,40,0,0">
            <Label Text="Current data:" FontSize="18" FontAttributes="Bold" HorizontalOptions="Center" TextColor="{AppThemeBinding Light=Black, Dark=White}" />
            <Label Text="{Binding Date, StringFormat='{}{0:MMMM/dd/yyyy}'}" HorizontalOptions="Center" FontSize="16" TextColor="{AppThemeBinding Light=Black, Dark=White}" />
        </VerticalStackLayout>
    </page:SingleDaySelectorPage.MyContent>

</page:SingleDaySelectorPage>
```

```csharp
public partial class SingleDaySelectorViewModel : ObservableObject
{
    [ObservableProperty]
    public DateOnly date;

    [ObservableProperty]
    public DateOnly minimumDate;

    [ObservableProperty]
    public DateOnly maximumDate;

    [ObservableProperty]
    public IList<int> daysWithEvents;

    public SingleDaySelectorViewModel()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        Date = today;
        MinimumDate = new DateOnly(today.Year, today.Month - 1, 7);
        MaximumDate = new DateOnly(today.Year, today.Month + 1, 7);

        DaysWithEvents = new List<int> { 6, 11, 23, 24 };
    }

    [RelayCommand]
    public void SelectedDay(DateOnly date)
    {
        Date = date;
    }
}
```

## License

MauiDays is released under the MIT License. See LICENSE.txt for details.


<!-- Images -->
[code-viewers-screenshot]: https://drive.google.com/uc?id=1EX_fTkVVkHcnq9b4twuyvvr06rF6ecC0
[keyboard-screenshot]: https://drive.google.com/uc?id=1NuYguBdXEx6K1UiqYgW7wSma1QVQNP5o
[mopups-url]: https://github.com/LuckyDucko/Mopups
[hero-image]: https://drive.google.com/uc?id=19PMZPpDlhSSrAbPP30HWyLi65tKZufWE
