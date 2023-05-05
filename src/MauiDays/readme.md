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
          <li><a href="#calendar-page">Calendar Page</a></li>     
        </ul>
        <li><a href="#customizing-the-appearance">Customizing the Appearance</a></li>        
      </ul>
    </li>
    <li><a href="#conclusion">Conclusion</a></li>
  </ol>
</details>

# MAUIDays NuGet Package for Month and Day Calendar in .NET MAUI

MauiDays is a .NET MAUI library that provides a customizable month and day calendar popup selector, along with a calendar page. It allows developers to easily add date selection functionality to their .NET MAUI apps.

The library has a dependency on [Mopups][mopups-url], which is used for the month and day calendar popup selector functionality. Note that if you only need the calendar page, you do not need to install [Mopups][mopups-url].

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

await await MopupService.Instance.PushAsync(popup);
```

and the same for the Month selector:

```csharp
var popup = SingleMonthSelectorCalendarPopup
            .Instance(async (month) =>
            {
                await OnDateChanged(month);
            })
            .Build();

await await MopupService.Instance.PushAsync(popup);
```

## Customizable properties

This package provides several ways to customize the appearance of the pin code page to fit the look and feel of your application. You can customize the colors and the page's elements.

### Headline & Image

You have the option to customize the headline, subheadline, and image on your page. However, I understand that not all developers may want to include these properties. That's why these properties can be null or empty. Of course, I encourage you to experiment with different combinations of these properties to create a truly unique and engaging experience for your users.



#### Customizable properties

- **CodeLength:** allows you to set the desired length of your pin code.
- **Color:** allows you to set the color of the pin code shape.
- **Size:** allows you to set the size of the pin code shape.

If you choose show the pin code, you can use the following properties too:

- **FontSize:** allows you to set the font size for the numbers.
- **TextColor:** allows you to set the text color for the numbers.
- **FontFamily:** allows you to set the font family for the numbers.

```xaml
<codePage:CodePage.CodeViewer>
        <codeViewer:CircleShowingCodeViewer
            Size="40"
            TextColor="{AppThemeBinding Light=White, Dark=Black}"
            Color="{AppThemeBinding Light=Black,Dark=White}"
            FontSize="25"
            FontFamily="RalewayBlack"
            CodeLength="6"
            Margin="0,0,0,40"/>
    </codePage:CodePage.CodeViewer>
```

### Keyboard

You can select from a keyboard without any shape, a square shape, or a circle shape, depending on the look and feel you want to achieve.

![Keyboard Availables][keyboard-screenshot]

Here's an example:

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<codePage:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:codePage="clr-namespace:MauiCode.Views.Pages;assembly=MauiCodes"
    xmlns:keyboard="clr-namespace:MauiCode.Views.Components.Keyboards;assembly=MauiCodes"
    x:Class="Packages.MAUI.App.Views.PinCode.PinCodePage"
    CallbackCodeFinished="{Binding UserEndTheCodeCommand}">
    
    <codePage:CodePage.KeyboardViewer>
        <keyboard:KeyboardSquare />
    </codePage:CodePage.KeyboardViewer>
    
</codePage:CodePage>
```

The equivalent C# code is:

```csharp
var pinCodePage = new CodePage();

pinCodePage.KeyboardViewer = new KeyboardSquare();

```

#### Customizable properties

- **Size:** allows you to set the size of the pin code shape.
- **FontSize:** allows you to set the font size for the numbers.
- **CancelTextFontSize:** allows you to set the font size for the text "Cancel".
- **TextColor:** allows you to set the text color for the numbers.
- **CancelTextColor:** allows you to set the text color for the text "Cancel".
- **CancelText:** allows you to set the string to show if the user want to cancel the operation, this is useful for translation for example.
- **BackspaceColor:** allows you to set the color of backspace button.

If you choose the keyboard with shape, you can use the following property too:

- **ShapeColor:** allows you to set the color for the shape.

```xaml
<codePage:CodePage.KeyboardViewer>
        <keyboard:KeyboardCircle
            ShapeColor="{AppThemeBinding Light=Black, Dark=White}"
            CancelTextColor="{AppThemeBinding Light=Black, Dark=White}"
            FontSize="25"
            Size="70"
            CancelTextFontSize="18"
            CancelText="CANCEL"
            TextColor="{AppThemeBinding Light=Black, Dark=White}"/>
    </codePage:CodePage.CodeViewer>
```

## License

Mauidays is released under the MIT License. See LICENSE.txt for details.


<!-- Images -->
[code-viewers-screenshot]: https://drive.google.com/uc?id=1EX_fTkVVkHcnq9b4twuyvvr06rF6ecC0
[keyboard-screenshot]: https://drive.google.com/uc?id=1NuYguBdXEx6K1UiqYgW7wSma1QVQNP5o
[mopups-url]: https://github.com/LuckyDucko/Mopups
