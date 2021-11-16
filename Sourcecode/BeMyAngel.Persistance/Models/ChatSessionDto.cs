using System;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class ChatSessionDto
    {
        public int ChatSessionId { get; set; }
        public int ChatId { get; set; }
        public int SessionId { get; set; }
        public string Token { get; set; }
    }
}
