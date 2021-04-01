using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class ChatRoomSession
    { 
        public int ChatRoomId { get; set; }
        public int SessionId { get; set; }
        public string Token { get; set; }
    }
}
