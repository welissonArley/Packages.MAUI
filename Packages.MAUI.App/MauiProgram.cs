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
            .ConfigureMopups()
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
        mauiAppBuilder.Services.AddTransient<Views.DashboardPage, ViewModels.DashboardViewModel>();
        mauiAppBuilder.Services.AddTransientWithShellRoute<Views.PinCode.PinCodeDashboardPage, ViewModels.PinCode.PinCodeDashboardViewModel>(RoutePages.DASHBOARD_PINCODE_PAGE);
        mauiAppBuilder.Services.AddTransientWithShellRoute<Views.PinCode.PinCodePage, ViewModels.PinCode.PinCodeViewModel>(RoutePages.PINCODE_PAGE);
        mauiAppBuilder.Services.AddTransientWithShellRoute<Views.Calendar.CalendarDashboardPage, ViewModels.Calendar.CalendarDashboardViewModel>(RoutePages.DASHBOARD_CALENDAR_PAGE);
        mauiAppBuilder.Services.AddTransientWithShellRoute<Views.Calendar.SingleDaySelectorPage, ViewModels.Calendar.SingleDaySelectorViewModel>(RoutePages.SINGLE_DAY_CALENDAR_PAGE);

        return mauiAppBuilder;
    }
}
