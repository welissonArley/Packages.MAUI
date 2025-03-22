using CommunityToolkit.Maui;
using Mopups.Hosting;
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
            .ConfigureMopups()
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

        mauiAppBuilder.Services.AddTransient<Views.PinCodes.MaskedPinCodePage>();
        mauiAppBuilder.Services.AddTransient<ViewModels.PinCodes.MaskedPinCodeViewModel>();

        Routing.RegisterRoute(RoutePages.PINCODE_PAGE, typeof(Views.PinCodes.PinCodePage));
        Routing.RegisterRoute(RoutePages.MASKED_PINCODE_PAGE, typeof(Views.PinCodes.MaskedPinCodePage));

        return mauiAppBuilder;
    }
}
