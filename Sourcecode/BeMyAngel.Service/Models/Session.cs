using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Token { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastAccessAt { get; set; }
        public int? UserId { get; set; }
    }
}
