using Mopups.Hosting;

namespace MauiDays;
public static class Initializer
{
    public static MauiAppBuilder ConfigureMauiDays(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.ConfigureMopups();

        return mauiAppBuilder;
    }
}
