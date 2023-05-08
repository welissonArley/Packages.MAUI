namespace Shared.Helpers.Extensions;
public static class ApplicationExtension
{
    public static bool IsLightMode(this Application application) => application.RequestedTheme == AppTheme.Light;
    public static Color GetDarkMode(this Application _) => Color.FromArgb("#1F1E19");
}
