namespace MauiCode.Helpers.Extensions;
public static class ApplicationExtension
{
    public static bool IsLightMode(this Application application) => application.RequestedTheme == AppTheme.Light;
}
