using CommunityToolkit.Maui;
using Packages.MAUI.App.Helpers;

namespace Packages.MAUI.App;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterPagesAndViewModels()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }

    private static MauiAppBuilder RegisterPagesAndViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<DashboardPage, ViewModels.DashboardViewModel>();

        mauiAppBuilder.Services.AddTransient<Views.PinCodes.PinCodePage>();
        mauiAppBuilder.Services.AddTransient<ViewModels.PinCodes.PinCodeViewModel>();

        Routing.RegisterRoute(RoutePages.DASHBOARD_PINCODE_PAGE, typeof(Views.PinCodes.PinCodePage));

        return mauiAppBuilder;
    }
}
