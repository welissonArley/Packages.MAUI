using Android.Content.PM;

namespace Packages.MAUI.App.Services.Screen;
public partial class DeviceOrientationService
{
    public readonly ScreenOrientation _currentOrientation;

    public DeviceOrientationService()
    {
        _currentOrientation = Platform.CurrentActivity.RequestedOrientation;
    }

    public partial void SetPortraitOrientation()
    {
        Platform.CurrentActivity.RequestedOrientation = ScreenOrientation.Portrait;
    }
    public partial void ResetOrientation()
    {
        Platform.CurrentActivity.RequestedOrientation = ScreenOrientation.User;
    }
    public partial void ResetOrientationForPopup()
    {
        Platform.CurrentActivity.RequestedOrientation = _currentOrientation;
    }
}
