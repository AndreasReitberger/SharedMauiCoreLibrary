using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AppBasement.Models.Messages
{
    public partial class StateChangedMessage : ValueChangedMessage<string>
    {
        public StateChangedMessage(string state) : base(state)
        {

        }
    }
}
