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

To use this package, simply install the NuGet package **MauiTabs** in your .NET MAUI project. In Visual Studio, you can do this by right-clicking on your project and selecting "Manage NuGet Packages". From there, search for "MauiTabs" and install the latest version.

```csharp
dotnet add package MauiTabs
```

Once the package is installed, you can use the Tabs in your application.

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
- **SymmetricTabView:** This style will generate a component that seamlessly adapts to the entire page, ensuring uniform tab sizes.

Now, you can use the tabs component like this example:

```xaml
<tabs:RoundedTabView
    BackgroundTabColor="#e7ebed"
    SelectedBackgroundTabColor="#0064f3"
    SelectedTextColor="White"
    SpacingBetweenTabAndContent="30"
    TextColor="Black">
    <tabs:RoundedTabView.ItemsList>
        <list:List x:TypeArguments="tabItem:Item">
            <tabItem:Item Text="Income" IsSelected="True">
                <tabItem:Item.Content>
                    <VerticalStackLayout HorizontalOptions="Center" Spacing="20">
                        <Label
                            FontSize="22"
                            HorizontalOptions="Center"
                            Text="Income" />
                        <Label HorizontalOptions="Center" Text="{Binding Text}" />
                    </VerticalStackLayout>
                </tabItem:Item.Content>
            </tabItem:Item>
            <tabItem:Item Text="Expense">
                <tabItem:Item.Content>
                    <VerticalStackLayout HorizontalOptions="Center" Spacing="20">
                        <Label
                            FontSize="22"
                            HorizontalOptions="Center"
                            Text="Expense" />
                        <Label HorizontalOptions="Center" Text="{Binding Text}" />
                    </VerticalStackLayout>
                </tabItem:Item.Content>
            </tabItem:Item>
        </list:List>
    </tabs:RoundedTabView.ItemsList>
</tabs:RoundedTabView>
```

add how many **tabItem:Item** you want and remember that you need to tell the tab title using the property ***Text*** and write your content inside ***tabItem:Item.Content*** :)

## Customizing the Appearance

#### For all tabs styles

- **TextColor:** allows you the text color for the tabs.
- **SelectedTextColor:** allows you to set the text color for the tab which is selected.
- **FontFamily:** allows you to set the text font family for the tabs.
- **SelectedFontFamily:** allows you to set the text font family for the tab which is selected.
- **SpacingBetweenTabAndContent:** allows you to set the space between the tab and the content for the selected tab.

```xaml
<tabs:UnderlinedTabView
  TextColor="Black"
  SelectedTextColor="White"
  FontFamily="Roboto"
  SpacingBetweenTabAndContent="50"
  SelectedFontFamily="Raleway">
```

#### UnderlinedTabView - Customizable properties

![Underlined Tab Style][underlined-tab-style-image]

- **LineColor:** allows you to set the line color for the selected tab.

#### RoundedTabView - Customizable properties

![Rounded Tab Style][rounded-tab-style-image]

- **BackgroundTabColor:** allows you to set the background color for the tabs (except the selected one).
- **SelectedBackgroundTabColor:** allows you to set the background color for the selected tab.

#### SymmetricTabView - Customizable properties

![Symmetric Tab Style][symmetric-tab-style-image]

- **BackgroundTabColor:** allows you to set the background color for the tabs (except the selected one).
- **SelectedBackgroundTabColor:** allows you to set the background color for the selected tab.
- **BackgroundComponentColor:** allows you to define the background color positioned behind the tabs.

## License

MauiTabs is released under the MIT License. See LICENSE.txt for details.

<!-- Images -->
[hero-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/feature/tabviews/Resources/Images/SmartphonesMockupTabs.png
[underlined-tab-style-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/feature/tabviews/Resources/Images/UnderlinedTabStyle.png
[rounded-tab-style-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/feature/tabviews/Resources/Images/RoundedTabStyle.png
[symmetric-tab-style-image]: https://raw.githubusercontent.com/welissonArley/Packages.MAUI/feature/tabviews/Resources/Images/SymmetricTabStyle.png
