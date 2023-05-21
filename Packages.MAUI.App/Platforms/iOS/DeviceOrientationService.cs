using Foundation;
using UIKit;

namespace Packages.MAUI.App.Services.Screen;
public partial class DeviceOrientationService
{
    //https://stackoverflow.com/questions/74838009/forcing-specific-maui-view-to-landscape-orientation-using-multitargeting-feature

    private readonly UIDeviceOrientation _currentOrientation;

    public DeviceOrientationService()
    {
        _currentOrientation = UIDevice.CurrentDevice.Orientation;
    }

    public partial void SetPortraitOrientation()
    {
        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
    }
    public partial void ResetOrientation()
    {
        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Unknown), new NSString("orientation"));
    }
    public partial void ResetOrientationForPopup()
    {
        UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)_currentOrientation), new NSString("orientation"));
    }
}
