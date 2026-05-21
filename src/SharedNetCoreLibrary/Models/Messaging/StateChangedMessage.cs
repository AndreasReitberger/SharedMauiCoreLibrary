using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AndreasReitberger.Shared.Core.Messaging
{
    public partial class StateChangedMessage(string state) : ValueChangedMessage<string>(state)
    {

    }
}
