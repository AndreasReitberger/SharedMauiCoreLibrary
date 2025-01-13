using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AndreasReitberger.Shared.Core.Messaging
{
    public partial class StateChangedMessage : ValueChangedMessage<string>
    {
        public StateChangedMessage(string state) : base(state)
        {

        }
    }
}
