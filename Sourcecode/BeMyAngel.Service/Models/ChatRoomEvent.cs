using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class ChatRoomEvent
    {
        public int ChatRoomEventId { get; set; }
        public int ChatRoomId { get; set; }
        public int ChatRoomEventTypeId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Data { get; set; }
        public int SessionId { get; set; }
    }
}
