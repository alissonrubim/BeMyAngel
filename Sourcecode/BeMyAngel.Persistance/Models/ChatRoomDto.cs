﻿using BeMyAngel.Persistance.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
