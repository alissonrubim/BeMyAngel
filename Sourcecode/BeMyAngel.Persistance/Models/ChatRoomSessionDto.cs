using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeMyAngel.Persistance.Models
{
    public class ChatRoomSessionDto
    {
        [Key]
        public int ChatRoomId { get; set; }
        [Key]
        public int SessionId { get; set; }
    }
}
