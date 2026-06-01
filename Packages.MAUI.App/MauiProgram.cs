using CommunityToolkit.Maui;
using Packages.MAUI.App.Constants;
using Packages.MAUI.App.Navigation;
using Packages.MAUI.App.ViewModels.Pages.Dashboard;
using Packages.MAUI.App.ViewModels.Pages.PinCodes;
using Packages.MAUI.App.ViewModels.Popups.Information;
using Packages.MAUI.App.ViewModels.Popups.InvalidCode;
using Packages.MAUI.App.ViewModels.Popups.UserCompletedCode;
using Packages.MAUI.App.Views.Pages.PinCodes;
using Packages.MAUI.App.Views.Popups;

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
            .AddPopups()
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
        appBuilder.Services.AddTransientWithShellRoute<AlphanumericPinCodePage, PinCodeViewModel>(RoutePages.ALPHANUMERICPINCODE_PAGE);

        return appBuilder;
    }

    private static MauiAppBuilder AddPopups(this MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddTransientPopup<InformationPopup, InformationVieweModel>();
        appBuilder.Services.AddTransientPopup<InvalidCodePopup, InvalidCodeViewModel>();
        appBuilder.Services.AddTransientPopup<UserCompletedCodePopup, UserCompletedCodeViewModel>();

        return appBuilder;
    }
}
