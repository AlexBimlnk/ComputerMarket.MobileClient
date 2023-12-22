using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MobileClient.UI.Messages;

public class ClearSignatureMessage : ValueChangedMessage<bool>
{
    public ClearSignatureMessage(bool value) : base(value)
    {
    }
}

