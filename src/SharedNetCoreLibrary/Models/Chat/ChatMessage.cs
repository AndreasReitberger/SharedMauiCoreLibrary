using AndreasReitberger.Shared.Core.Contacts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chat
{
    public partial class ChatMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        public partial Contact? User { get; set; }

        [ObservableProperty]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        public partial DateTimeOffset? Sent { get; set; }

        [ObservableProperty]
        public partial DateTimeOffset? Read { get; set; }

        #endregion

        #region Ctor

        public ChatMessage() { }
        public ChatMessage(Contact? user, string message, DateTimeOffset? sent, DateTimeOffset? read)
        {
            User = user;
            Message = message;
            Sent = sent;
            Read = read;
        }

        #endregion
    }
}
