using BeMyAngel.Persistance.Generics;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class ChatRoomDto: IDto
    {
        [Key]
        public int ChatRoomId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? TerminatedAt { get; set; }
    }
}
