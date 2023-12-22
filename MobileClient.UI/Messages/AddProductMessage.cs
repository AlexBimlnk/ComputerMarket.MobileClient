using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MobileClient.UI.Messages;

public class AddProductMessage : ValueChangedMessage<bool>
{
    public AddProductMessage(bool value) : base(value)
    {
    }
}

