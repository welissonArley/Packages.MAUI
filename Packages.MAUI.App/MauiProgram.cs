using CommunityToolkit.Maui;
using Mopups.Hosting;
using Packages.MAUI.App.Constants;
using Packages.MAUI.App.Navigation;
using Packages.MAUI.App.ViewModels.Pages.Dashboard;
using Packages.MAUI.App.ViewModels.Pages.PinCodes;
using Packages.MAUI.App.Views.Pages.PinCodes;

namespace Packages.MAUI.App;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .AddNavigationService()
            .RegisterPagesAndViewModels()
            .ConfigureMopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", FontFamily.MAIN_FONT_REGULAR);
                fonts.AddFont("OpenSans-Light.ttf", FontFamily.MAIN_FONT_LIGHT);
                fonts.AddFont("OpenSans-Bold.ttf", FontFamily.MAIN_FONT_BOLD);
            });

        return builder.Build();
    }

    private static MauiAppBuilder AddNavigationService(this MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddSingleton<INavigationService, NavigationService>();

        return appBuilder;
    }

    private static MauiAppBuilder RegisterPagesAndViewModels(this MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddTransient<DashboardViewModel>();

        appBuilder.Services.AddTransientWithShellRoute<ShowPinCodePage, PinCodeViewModel>(RoutePages.SHOWPINCODE_PAGE);
        appBuilder.Services.AddTransientWithShellRoute<HidePinCodePage, PinCodeViewModel>(RoutePages.HIDEPINCODE_PAGE);
        appBuilder.Services.AddTransientWithShellRoute<MaskedPinCodePage, PinCodeViewModel>(RoutePages.MASKEDPINCODE_PAGE);

        return appBuilder;
    }
}
