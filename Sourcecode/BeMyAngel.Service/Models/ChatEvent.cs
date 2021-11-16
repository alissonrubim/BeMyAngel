using System;

namespace BeMyAngel.Service.Models
{
    public class ChatEvent
    {
        public int ChatEventId { get; set; }
        public int ChatId { get; set; }
        public int ChatEventTypeId { get; set; }
        public int ChatSessionId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Data { get; set; }
    }
}
