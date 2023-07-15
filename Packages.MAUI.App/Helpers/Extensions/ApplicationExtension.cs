namespace Packages.MAUI.App.Helpers.Extensions;
public static class ApplicationExtension
{
    public static bool IsLightMode(this Application application) => application.RequestedTheme == AppTheme.Light;
    public static Color GetDarkMode(this Application application) => (Color)application.Resources["DarkModeColor"];
}
