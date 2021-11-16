namespace BeMyAngel.Service.Models.Collections
{
    public static class ChatEventTypes
    {
        public static ChatEventType CreateChat { get; } = new ChatEventType
        {
            ChatEventTypeId = 1,
            Identifier = "CreateChat"
        };
        public static ChatEventType PostMessage { get; } = new ChatEventType
        {
            ChatEventTypeId = 2,
            Identifier = "PostMessage"
        };
    }
}
