using System;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class ChatEventDto
    {
        [Key]
        public int ChatEventId { get; set; }
        public int ChatId { get; set; }
        public int ChatEventTypeId { get; set; }
        public int ChatSessionId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Data { get; set; }
    }
}
