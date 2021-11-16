using BeMyAngel.Persistance.Generics;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class ChatDto: IDto
    {
        [Key]
        public int ChatId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? TerminatedAt { get; set; }
        public string Identifier { get; set; }
    }
}
