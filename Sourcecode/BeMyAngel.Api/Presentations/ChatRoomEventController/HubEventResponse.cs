using System;

namespace BeMyAngel.Api.Presentations.ChatRoomEventController
{
    public class HubEventResponse
    {
        public int ChatRoomEventId { get; set; }
        public int ChatRoomEventTypeId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Data { get; set; }
        public string ChatRoomSessionToken { get; set; }
    }
}
