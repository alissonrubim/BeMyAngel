using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class ChatSession
    { 
        public int ChatSessionId { get; set; }
        public int ChatId { get; set; }
        public int SessionId { get; set; }
        public string Token { get; set; }
        public string ConnectionId { get; set; }
    }
}
