﻿using BeMyAngel.Persistance.Generics;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class UserDto : IDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }

        public string EncryptKey { get; set; }
    }
}
