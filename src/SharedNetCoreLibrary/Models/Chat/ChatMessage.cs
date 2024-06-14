using AndreasReitberger.Shared.Core.Contacts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chat
{
    public partial class ChatMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        Contact? user;

        [ObservableProperty]
        string message = string.Empty;

        [ObservableProperty]
        DateTimeOffset? sent;

        [ObservableProperty]
        DateTimeOffset? read;

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
