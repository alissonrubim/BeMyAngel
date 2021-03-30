﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeMyAngel.Persistance.Models
{
    public class ChatRoomEventDto
    {
        [Key]
        public int ChatRoomEventId { get; set; }
        public int ChatRoomId { get; set; }
        public int ChatRoomEventTypeId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Data { get; set; }
        public int SessionId { get; set; }
    }
}
