using System;

namespace BeMyAngel.Service.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? TerminatedAt { get; set; }
        public string Identifier { get; set; }
    }
}
