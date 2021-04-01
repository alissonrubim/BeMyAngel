using System;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class ChatRoomSessionDto
    {
        public int ChatRoomSessionId { get; set; }
        public int ChatRoomId { get; set; }
        public int SessionId { get; set; }
        public string Token { get; set; }
    }
}
