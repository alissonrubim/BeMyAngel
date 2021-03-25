using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public DateTime CreatedAtDateTime { get; set; }
        public DateTime? TerminatedAtDateTime { get; set; }
    }
}
