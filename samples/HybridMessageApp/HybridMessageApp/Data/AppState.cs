using System;
using System.Collections.Generic;

namespace HybridMessageApp.Data
{
    public class AppState
    {
        public List<string> Folders { get; } = new List<string> { "Inbox", "Archive", "Sent", "Junk" };
        public string CurrentFolder { get; } = "Inbox";
        public Message CurrentMessage { get; private set; }

        public EventHandler<Message> CurrentMessageChanged;

        public void NavigateToMessage(Message message)
        {
            CurrentMessage = message;
            CurrentMessageChanged?.Invoke(this, CurrentMessage);
        }
    }
}
