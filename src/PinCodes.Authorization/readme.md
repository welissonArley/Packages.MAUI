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
        <li><a href="#full-code">Full code</a></li>
      </ul>
    </li>
  </ol>
</details>

# Creating a Customizable Pin Code Page in .NET MAUI

This library provides developers with an easy way to add a customizable PIN Code page to their .NET MAUI applications. With just a few lines of code, you can create a secure page that requires a PIN Code to access.

![PinCode Page Screenshot][hero-image]

### **Compatibility**

| Platform | Version | Availability |
| --- | --- | --- |
| iOS | 14.0 and higher | ✅ |
| Android | 5.0 and higher | ✅ |
| Windows | 10.0.17763.0 and higher | ✅|
| macOS | 10.15 and higher | ✅ |

### **Features**

- **Keyboards:** customize buttons by defining their appearance, including shape, color, size, and other properties.
- **Code viewer:** fully customizable, allowing developers to define a [Shape](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/shapes/?view=net-maui-8.0) (e.g., ellipse, rectangle) and other properties for a tailored viewing experience.
- **Headers:** complete flexibility, allowing developers incorporate images, labels, and various other elements to suit their needs.
- **Customize the length of your code:** adjust the amount of digits (default is 4).
- and others.

## Getting Started

### Installation

To use this package, simply install the NuGet package **PinCodes.Authorization.Maui** in your .NET MAUI project. In Visual Studio, you can do this by right-clicking on your project and selecting "Manage NuGet Packages". From there, search for "PinCodes.Authorization.Maui" and install the latest version.

```csharp
dotnet add package PinCodes.Authorization.Maui
```

Once the package is installed, you can add a PIN Code Page to your application.

### Usage

Create a new ContentPage in your .NET MAUI project and add a reference to the CodePage namespace in your file:

```xaml
xmlns:pinCodeAuthorization="clr-namespace:PinCodes.Authorization.Views.Pages;assembly=PinCodes.Authorization.Maui"
```

Now, instead of having a ContentPage in your XAML file, you need to change it to:

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<pinCodeAuthorization:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:pinCodeAuthorization="clr-namespace:PinCodes.Authorization.Views.Pages;assembly=PinCodes.Authorization.Maui"
    x:Class="MAUI.App.Views.MyPinCodePage">
    
</pinCodeAuthorization:CodePage>
```

And the code-behind to:

```csharp
public partial class MyPinCodePage : PinCodes.Authorization.Views.Pages.CodePage
{
   public MyPinCodePage()
   {
      InitializeComponent();
   }
}
```

### Command Callback

**REMEMBER**, be sure to provide a command callback. It will be automatically triggered once the user has provided the entire code. So your XAML file will look like:

```xaml
<pinCodeAuthorization:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:pinCodeAuthorization="clr-namespace:PinCodes.Authorization.Views.Pages;assembly=PinCodes.Authorization.Maui"
    x:Class="MAUI.App.Views.MyPinCodePage"
    CallbackCodeFinished="{Binding UserCompletedCodeCommand}">
    
</pinCodeAuthorization:CodePage>
```

And your ViewModel will look like:

```csharp
[RelayCommand]
public void UserCompletedCode(string code)
{
    //do something with the code response
}
```

## Customizing the Appearance

This package provides several ways to customize the appearance of the PIN Code Page to fit the look and feel of your application. You can customize the colors and the page's elements.

### Header (Above the PinCode Viewer)

You have the option to customize the header on your page by passing it as a **StackLayout** (VerticalStackLayout, HorizontalStackLayout ...), allowing you to fully tailor the appearance and functionality to suit your needs. This flexibility means you can include images, labels, commands, and other UI elements, giving you complete control over the header's content and layout.

```xaml

```

### Code Viewer

You can choose to hide or show the PIN Code, as well as select from a variety of shapes, including circles or squares. It is important for you to make a decision to hide or show the code in order to add the correct namespace on XAML.

If you want to hide it, add:

```xaml
xmlns:codeViewer="clr-namespace:MauiCodes.Views.Components.CodeViewers.Hide;assembly=PinCodes.Authorization.Maui"
```

but if you want to show the PIN Code, use:

```xaml
xmlns:codeViewer="clr-namespace:MauiCodes.Views.Components.CodeViewers.Show;assembly=PinCodes.Authorization.Maui"
```

Whether you prefer a minimalist or more elaborate design, my library has you covered.

Here's an example showing the PIN Code with a circle shape:

```xaml

```

### Keyboard

You can select from a keyboard a circle shape, a keyboard without shape, or a square shape, depending on the look and feel you want to achieve. Don't forget to add the namespace on your XAML file:

```xaml
xmlns:keyboard="clr-namespace:MauiCodes.Views.Components.Keyboards;assembly=PinCodes.Authorization.Maui"
```

## Full code

```xaml

```

## License

PinCodes.Authorization.Maui is released under the MIT License. See LICENSE.txt for details.


<!-- Images -->
[hero-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/master/Resources/Images/SmartPhoneMockupPinCode.png
