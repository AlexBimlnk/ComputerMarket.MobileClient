using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MobileClient.UI.Messages;

public class SaveSignatureMessage : ValueChangedMessage<int>
{
    public SaveSignatureMessage(int value) : base(value)
    {
    }
}

