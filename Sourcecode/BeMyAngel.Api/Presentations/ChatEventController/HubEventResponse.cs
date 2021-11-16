using System;

namespace BeMyAngel.Api.Presentations.ChatEventController
{
    public class HubEventResponse
    {
        public int ChatEventId { get; set; }
        public int ChatEventTypeId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Data { get; set; }
        public string ChatSessionToken { get; set; }
    }
}
