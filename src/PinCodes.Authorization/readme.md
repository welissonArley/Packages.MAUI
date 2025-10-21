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
        <li><a href="#full-xaml-code">Full code</a></li>
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
    x:Name="PagePinCode"
    CallbackCodeFinished="{Binding BindingContext.UserCompletedCodeCommand, Source={x:Reference PagePinCode}}">
    
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

You can customize the header on your page by passing it as a **StackLayout** (e.g., VerticalStackLayout, HorizontalStackLayout), giving you the flexibility to tailor its appearance and functionality to your needs. This allows you to include images, labels, commands, and other UI elements, providing complete control over the header's content and layout. To set up your header, use the Header property within CodePage.

```xaml
<pinCodeAuthorization:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    x:Name="PagePinCode"
    CallbackCodeFinished="{Binding BindingContext.UserCompletedCodeCommand, Source={x:Reference PagePinCode}}">
    
    <pinCodeAuthorization:CodePage.Header>
      <VerticalStackLayout Margin="0,20,0,40" Spacing="5">
          <Label
                FontSize="36"
                HorizontalOptions="Center"
                HorizontalTextAlignment="Center"
                Text="Verify Phone" />

            <Label
                HorizontalOptions="Center"
                Text="code has been sent to +351 912345678" />
      </VerticalStackLayout>
  </pinCodeAuthorization:CodePage.Header>
</pinCodeAuthorization:CodePage>
```

### SubHeader (Below the PinCode Viewer)

You have the option to add a subheader to display more information beneath the code viewer component. Utilize the StackLayout (such as VerticalStackLayout or HorizontalStackLayout) to customize the subheader's look and functionality according to your requirements. This feature allows you to incorporate images, labels, commands, and various UI elements, providing comprehensive control over the subheader's content and structure. To add a subheader, simply assign your customized layout to the SubHeader property within CodePage.

```xaml
<pinCodeAuthorization:CodePage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    x:Name="PagePinCode"
    CallbackCodeFinished="{Binding BindingContext.UserCompletedCodeCommand, Source={x:Reference PagePinCode}}">
    
    <pinCodeAuthorization:CodePage.SubHeader>
        <VerticalStackLayout Margin="0,30,0,20" HorizontalOptions="Center">
            <Label Margin="0,0,0,10" FontSize="16" Text="Didn’t get the verification code?" />

            <VerticalStackLayout Padding="0,5,0,5" HeightRequest="40" HorizontalOptions="Center">
                <VerticalStackLayout.GestureRecognizers>
                    <TapGestureRecognizer Command="{Binding BindingContext.ResendCodeCommand, Source={x:Reference PagePinCode}}" />
                </VerticalStackLayout.GestureRecognizers>
                <Label HorizontalOptions="Center" Text="Resend code" />
            </VerticalStackLayout>
        </VerticalStackLayout>
    </pinCodeAuthorization:CodePage.SubHeader>
</pinCodeAuthorization:CodePage>
```

### Code Viewer

The first step is to reference the CodeViewer component namespace in your XAML file:

```xaml
xmlns:codeViewer="clr-namespace:PinCodes.Authorization.Views.Components.CodeViewers;assembly=PinCodes.Authorization.Maui"
```

The **CodeViewer** property allows you to control how the PIN code is displayed in the UI. You can choose whether to show, hide, or temporarily display the code, and each behavior is handled by a different component in the XAML.

🔒 Option 1 — Hide the Code

If you want to completely hide the code from the user, use the **HideCodeViewer** component:

```xaml
<pinCodeAuthorization:CodePage.CodeViewer>
    <codeViewer:HideCodeViewer>
      ... CUSTOMIZATION HERE
    </codeViewer:HideCodeViewer>
</pinCodeAuthorization:CodePage.CodeViewer>
```

👀 Option 2 — Show the Code

If you prefer to display the PIN code clearly, use the **ShowCodeViewer** component:

```xaml
<pinCodeAuthorization:CodePage.CodeViewer>
    <codeViewer:ShowCodeViewer>
      ... CUSTOMIZATION HERE
    </codeViewer:ShowCodeViewer>
</pinCodeAuthorization:CodePage.CodeViewer>
```

⏳ Option 3 — Temporarily Show Then Hide

If you want to show the code briefly and automatically mask it after a delay, use the **MaskedCodeViewer** component:

```xaml
<pinCodeAuthorization:CodePage.CodeViewer>
    <codeViewer:MaskedCodeViewer>
      ... CUSTOMIZATION HERE
    </codeViewer:MaskedCodeViewer>
</pinCodeAuthorization:CodePage.CodeViewer>
```

#### Customizing the CodeViewer

No matter which viewer type you choose (HideCodeViewer, ShowCodeViewer, or MaskedCodeViewer), the following customization properties are available:

| Option | Type | Purpose |
| --- | --- | --- |
| CodeLength | ushort | Defines the number of digits in the code. Default is 4. |
| Spacing | ushort | Sets the spacing between each character indicator. |
| ShapeViewer | Shape | Defines the shape used to represent each character slot. Accepts any shape such as Ellipse, Rectangle, etc. |
| CodeColor | Color | Fill color used when a code digit is entered. If empty, the default shape fill color is used. |
| CodeStrokeColor | Color | Stroke color used when a code digit is entered. If empty, the default shape stroke color is used. |

##### Showing or Briefly Revealing the Code

If you want users to see the code as they type, you must define the **PinCharacterLabel** property. This property accepts a Label that controls how each digit is displayed. You can fully customize its style using any label properties like FontSize, TextColor, FontAttributes, and more.

```xaml
<pinCodeAuthorization:CodePage.CodeViewer>
    <codeViewer:ShowCodeViewer
        CodeColor="Transparent"
        CodeStrokeColor="Blue">
        <codeViewer:ShowCodeViewer.ShapeViewer>
            <RoundRectangle
                CornerRadius="10"
                Fill="Transparent"
                HeightRequest="50"
                Stroke="Red"
                StrokeThickness="2"
                WidthRequest="50" />
        </codeViewer:ShowCodeViewer.ShapeViewer>

        <codeViewer:ShowCodeViewer.PinCharacterLabel>
            <Label
                FontSize="16"
                HorizontalOptions="Center"
                VerticalOptions="Center" />
        </codeViewer:ShowCodeViewer.PinCharacterLabel>
    </codeViewer:ShowCodeViewer>
</pinCodeAuthorization:CodePage.CodeViewer>
```

If instead you want a more secure experience, where each digit is briefly shown and then automatically hidden, use the **MaskedCodeViewer**. This component improves security in scenarios like banking or authentication apps.

The MaskedCodeViewer offers two additional customization properties:

| Property	| Type | Description
| --- | --- | --- |
| HideCodeAfter |	TimeSpan | Defines how long each character remains visible before being masked. Default is 1 second.
| MaskContent | View | (Optional) Custom content displayed after hiding the code.


```xaml
<pinCodeAuthorization:CodePage.CodeViewer>
    <codeViewer:MaskedCodeViewer
        CodeColor="Transparent"
        CodeStrokeColor="Blue"
        HideCodeAfter="0:0:0.300"> <!-- 300 milliseconds -->
        <codeViewer:MaskedCodeViewer.ShapeViewer>
            <RoundRectangle
                CornerRadius="10"
                Fill="Transparent"
                HeightRequest="50"
                Stroke="Red"
                StrokeThickness="2"
                WidthRequest="50" />
        </codeViewer:MaskedCodeViewer.ShapeViewer>

        <codeViewer:MaskedCodeViewer.MaskContent>
            <Ellipse
                Fill="Red"
                HeightRequest="20"
                HorizontalOptions="Center"
                VerticalOptions="Center"
                WidthRequest="20" />
        </codeViewer:MaskedCodeViewer.MaskContent>

        <codeViewer:MaskedCodeViewer.PinCharacterLabel>
            <Label
                FontSize="16"
                HorizontalOptions="Center"
                VerticalOptions="Center" />
        </codeViewer:MaskedCodeViewer.PinCharacterLabel>
    </codeViewer:MaskedCodeViewer>
</pinCodeAuthorization:CodePage.CodeViewer>
```

### Keyboard

You must use the **Keyboard** property available on the CodePage to select the appearance of the keyboard buttons, depending on the look and feel you want to achieve. Don't forget to add the namespace on your XAML file:

```xaml
xmlns:keyboardViewer="clr-namespace:PinCodes.Authorization.Views.Components.Keyboards;assembly=PinCodes.Authorization.Maui"
```

The keyboard view has several properties for customization, with two being mandatory (ShapeViewer & BackspaceViewer) and the others optional:

| Option | Type | Purpose |
| --- | --- | --- |
| ShapeViewer | Button | Use this property to define the appearance of the buttons for the numbers. Feel free to choose the size, colors, etc. |
| BackspaceViewer | Button or ImageButton | You can pass a Button or an ImageButton to be displayed as the backspace option for the keyboard. |
| LeftSideButtonShapeViewer | Button or ImageButton | You can pass a Button or an ImageButton to be displayed as an option on the left side of the number 0. This button is optional; if you don't pass it, the left side of the number 0 will be empty. |
| RowSpacing | ushort | Defines the spacing between each line of the keyboard. |
| ColumnSpacing | ushort | Defines the spacing between each column of the keyboard. |

Below is an example demonstrating how easy it is to define the properties:

```xaml
<pinCodeAuthorization:CodePage.Keyboard>
    <keyboardViewer:KeyboardViewer ColumnSpacing="40" RowSpacing="20">
        <keyboardViewer:KeyboardViewer.ShapeViewer>
            <Button
                BackgroundColor="Transparent"
                CornerRadius="20"
                FontSize="24"
                HeightRequest="80"
                WidthRequest="80" />
        </keyboardViewer:KeyboardViewer.ShapeViewer>

        <keyboardViewer:KeyboardViewer.BackspaceViewer>
            <ImageButton
                Padding="{OnPlatform Default=15,
                                        Android=20,
                                        iOS=22}"
                BackgroundColor="Transparent"
                Source="illustration_delete.png" />
        </keyboardViewer:KeyboardViewer.BackspaceViewer>

        <keyboardViewer:KeyboardViewer.LeftSideButtonShapeViewer>
            <ImageButton
                Padding="{OnPlatform Default=15,
                                        Android=20,
                                        iOS=22}"
                BackgroundColor="Transparent"
                Command="{Binding BindingContext.FaceIdCommand, Source={x:Reference PagePinCode}}"
                Source="illustration_faceid.png" />
        </keyboardViewer:KeyboardViewer.LeftSideButtonShapeViewer>
    </keyboardViewer:KeyboardViewer>
</pinCodeAuthorization:CodePage.Keyboard>
```

| Component | Is it Require? |
| --- | --- |
| Header | ⛔ No, it's optional |
| SubHeader | ⛔ No, it's optional |
| CodeViewer | ✅ Yes, it's mandatory |
| Keyboard | ✅ Yes, it's mandatory |

## License

PinCodes.Authorization.Maui is released under the MIT License. See LICENSE.txt for details.


<!-- Images -->
[hero-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/master/Resources/Images/SmartPhoneMockupPinCode.png
