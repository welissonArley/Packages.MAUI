using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PinCodes.Authorization.Messages;
internal sealed class CleanPinCodeMessage(string message) : ValueChangedMessage<string>(message)
{
}