namespace BeMyAngel.Service.Models.Collections
{
    public static class ChatRoomEventTypes
    {
        public static ChatRoomEventType CreateChat { get; } = new ChatRoomEventType
        {
            ChatRoomEventTypeId = 1,
            Identifier = "CreateChat"
        };
        public static ChatRoomEventType PostMessage { get; } = new ChatRoomEventType
        {
            ChatRoomEventTypeId = 2,
            Identifier = "PostMessage"
        };
    }
}
