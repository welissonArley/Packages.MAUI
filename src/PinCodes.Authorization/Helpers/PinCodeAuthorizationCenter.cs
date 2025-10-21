namespace PinCodes.Authorization.Helpers;
public static class PinCodeAuthorizationCenter
{
    public static event Action? ClearRequest;

    public static void ClearPinCode() => ClearRequest?.Invoke();
}