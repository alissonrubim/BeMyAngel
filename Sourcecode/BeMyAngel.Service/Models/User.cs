using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public string EncryptKey { get; set; }
    }
}
