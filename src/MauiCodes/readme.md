<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#creating-a-customizable-pin-code-page-in-net-maui">About The Project</a>
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
        <li><a href="#customizing-the-appearance">Customizing the Appearance</a></li>        
      </ul>
    </li>
  </ol>
</details>

# Creating a Customizable Pin Code Page in .NET MAUI

This library provides developers with an easy way to add a customizable pin code page to their .NET MAUI applications. With just a few lines of code, you can create a secure page that requires a pin code to access.

### **Compatibility**

| Platform | Version | Availability |
| --- | --- | --- |
| iOS | 14.0 and higher | ✅ |
| Android | 5.0 and higher | ✅ |
| UWP | 10.0.17763.0 and higher | ✅ |
| macOS | 10.15 and higher | ✅ |

### **Features**

- **Keyboards:** offers a variety of keyboard options to choose from.
- **Code viewer:** includes several different options for customization.
- **Headline:** you have the flexibility to fully customize the headline phrases.
- **Color customization:** giving you complete control over the look and feel of your keyboard and code viewer.
- **Customize the length of your code:** adjust the code lenght (default is 4).
- and others.

## Getting Started

### Installation

To use this package, simply install it in your .NET MAUI project using NuGet. In Visual Studio, you can do this by right-clicking on your project and selecting "Manage NuGet Packages". From there, search for "MauiCode" and install the latest version.

```powershell
Install-Package MauiCode
```

Once the package is installed, you can add a pin code page to your application.

### Usage

**REMEMBER:** be sure to provide a command callback. It will be automatically triggered once the user has provided the entire code.

To use the pin code page, you need to add a reference to the CodePage namespace in your XAML file:

```xaml
xmlns:codePage="clr-namespace:MauiCode.Views.Pages;assembly=MauiCodes"
```

Here's an example:

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<codePage:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:codePage="clr-namespace:MauiCode.Views.Pages;assembly=MauiCodes"
    x:Class="Packages.MAUI.App.Views.PinCode.PinCodePage"
    CallbackCodeFinished="{Binding UserEndTheCodeCommand}">
    
</codePage:CodePage>
```

The equivalent C# code is:

```csharp
var pinCodePage = new CodePage();

pinCodePage.CallbackCodeFinished = new Command((code) =>
{
    await Shell.Current.GoToAsync("..");
    //do something with the code response
});

await Navigation.PushAsync(pinCodePage);
```

## Customizing the Appearance

This package provides several ways to customize the appearance of the pin code page to fit the look and feel of your application. You can customize the colors and the page's elements.

### Headline & Image

You have the option to customize the headline, subheadline, and image on your page. However, I understand that not all developers may want to include these properties. That's why these properties can be null or empty. Of course, I encourage you to experiment with different combinations of these properties to create a truly unique and engaging experience for your users.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<codePage:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:codePage="clr-namespace:MauiCode.Views.Pages;assembly=MauiCodes"
    x:Class="Packages.MAUI.App.Views.PinCode.PinCodePage"
    CallbackCodeFinished="{Binding UserEndTheCodeCommand}"
    Headline="YOUR HEADLINE HERE"
    SubHeadline="YOUR SUBHEADLINE HERE">
    
    <codePage:CodePage.Illustration>
        <Image Source="illustration_dog.png" HeightRequest="80"/>
    </codePage:CodePage.Illustration>
    
</codePage:CodePage>
```

The equivalent C# code is:

```csharp
var pinCodePage = new CodePage();

pinCodePage.Headline = "YOUR HEADLINE HERE";

pinCodePage.SubHeadline = "YOUR SUBHEADLINE HERE";

pinCodePage.Illustration = new Image { Source = ImageSource.FromFile(path), HeightRequest = 80 };

```

### Code Viewer

You can choose to hide or show the pin code, as well as select from a variety of shapes, including circles or squares.

![Code Viewers Availables][code-viewers-screenshot]

Whether you prefer a minimalist or more elaborate design, my library has you covered.

Here's an example:

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<codePage:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:codePage="clr-namespace:MauiCode.Views.Pages;assembly=MauiCodes"
    xmlns:codeViewer="clr-namespace:MauiCode.Views.Components.CodeViewers.Show;assembly=MauiCodes"
    x:Class="Packages.MAUI.App.Views.PinCode.PinCodePage"
    CallbackCodeFinished="{Binding UserEndTheCodeCommand}">
    
    <codePage:CodePage.CodeViewer>
        <codeViewer:CircleShowingCodeViewer />
    </codePage:CodePage.CodeViewer>
    
    <!--  You can change CircleShowingCodeViewer, for the examples on the image above:
          SquareHidingCodeViewer | CircleHidingCodeViewer ... -->
    
</codePage:CodePage>
```

The equivalent C# code is:

```csharp
var pinCodePage = new CodePage();

pinCodePage.CodeViewer = new CircleHidingCodeViewer();

```

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

MauiCodes is released under the MIT License. See LICENSE.txt for details.


<!-- Images -->
[code-viewers-screenshot]: https://drive.google.com/uc?id=1EX_fTkVVkHcnq9b4twuyvvr06rF6ecC0
[keyboard-screenshot]: https://drive.google.com/uc?id=1NuYguBdXEx6K1UiqYgW7wSma1QVQNP5o
