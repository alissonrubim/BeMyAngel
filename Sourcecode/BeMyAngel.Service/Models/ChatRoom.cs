using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? TerminatedAt { get; set; }
    }
}
