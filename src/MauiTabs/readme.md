<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#creating-a-customizable-pin-code-page-in-net-maui">About The Project</a>
      <ul>
        <li><a href="#compatibility">Compatibility</a></li>
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

# Creating a Customizable Tabs component in .NET MAUI

Easily incorporate tabbed navigation into your app, providing access to different sections and content. Streamline user experience and improve app organization
with our user-friendly Tab Control library designed specifically for .NET MAUI projects.

![Tabs View Page Screenshot][hero-image]

### **Compatibility**

| Platform | Version | Availability |
| --- | --- | --- |
| iOS | 14.0 and higher | ✅ |
| Android | 5.0 and higher | ✅ |
| Windows | 10.0.17763.0 and higher | ✅|
| macOS | 10.15 and higher | ✅ |

## Getting Started

### Installation

To use this package, simply install the NuGet package **MauiTabs** in your .NET MAUI project. In Visual Studio, you can do this by right-clicking on your project and selecting "Manage NuGet Packages". From there, search for "PinCodes.Authorization.Maui" and install the latest version.

```csharp
dotnet add package MauiTabs
```

Once the package is installed, you can use the Tabs to your application.

### Usage

Create a new ContentPage in your .NET MAUI project and add three references in your file:

```xaml
xmlns:tabs="clr-namespace:MauiTabs;assembly=MauiTabs"
xmlns:tabItem="clr-namespace:MauiTabs.Entities;assembly=MauiTabs"
xmlns:list="clr-namespace:System.Collections.Generic;assembly=System.Collections"
```

After adding the xmlns namespace to your page, it's decision time! You need to choose one of the available tab view styles, which are:

- **RoundedTabView:** Tabs with a background and rounded corners;
- **UnderlinedTabView:** Tabs without background but with a small line on the selected tab.

Now, you can use the tabs component doing that:

```xaml
<tabs:RoundedTabView>
  <tabs:RoundedTabView.ItemsList>
    <list:List x:TypeArguments="tabItem:Item">
        <tabItem:Item Text="Income">
            <tabItem:Item.Content>
                <VerticalStackLayout HorizontalOptions="Center" Spacing="20">
                    <Label Text="Income" FontSize="22" HorizontalOptions="Center"/>
                    <Label Text="{Binding Text}" HorizontalOptions="Center"/>
                </VerticalStackLayout>
            </tabItem:Item.Content>
        </tabItem:Item>

        <tabItem:Item Text="Expense">
            <tabItem:Item.Content>
                <VerticalStackLayout HorizontalOptions="Center" Spacing="20">
                    <Label Text="Expense" FontSize="22" HorizontalOptions="Center"/>
                    <Label Text="{Binding Text}" HorizontalOptions="Center"/>
                </VerticalStackLayout>
            </tabItem:Item.Content>
        </tabItem:Item>
    </list:List>
  </tabs:RoundedTabView.ItemsList>
</tabs:RoundedTabView>
```

add how many **tabItem:Item** you want and remember that you need to tell the tab title using the property ***Text*** and write your content inside ***tabItem:Item.Content*** :)

## Customizing the Appearance

#### Text (for all tabs style)

- **TextColor:** allows you the text color for the tabs.
- **SelectedTextColor:** allows you to set the text color for the tab which is selected.
- **FontFamily:** allows you to set the text font family for the tabs.
- **SelectedFontFamily:** allows you to set the text font family for the tab which is selected.

```xaml
<tabs:UnderlinedTabView
  TextColor="Black"
  SelectedTextColor="White"
  FontFamily="Roboto"
  SelectedFontFamily="Raleway">
```

#### UnderlinedTabView - Customizable properties

- **LineColor:** allows you to set the line color for the selected tab.

#### RoundedTabView - Customizable properties

- **BackgroundTabColor:** allows you to set the background color for the tabs (except the selected one).
- **SelectedBackgroundTabColor:** allows you to set the background color for the selected tab.

## License

MauiTabs is released under the MIT License. See LICENSE.txt for details.

<!-- Images -->
[hero-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/master/Resources/Images/SmartphoneMockupTabs.png