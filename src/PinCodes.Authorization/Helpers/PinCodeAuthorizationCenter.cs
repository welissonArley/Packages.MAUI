using CommunityToolkit.Mvvm.Messaging;
using PinCodes.Authorization.Messages;

namespace PinCodes.Authorization.Helpers;
public class PinCodeAuthorizationCenter
{
    public static void ClearPinCode()
    {
        WeakReferenceMessenger.Default.Send(new CleanPinCodeMessage(string.Empty));
    }
}