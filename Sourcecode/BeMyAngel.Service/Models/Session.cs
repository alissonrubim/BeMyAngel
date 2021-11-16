using System;

namespace BeMyAngel.Service.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Token { get; set; }        
        public string UserAgent { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastAccessAt { get; set; }
        public int? UserId { get; set; }
        public string LocalIpAddress { get; set; }
        public int LocalPort { get; set; }
        public string RemoteIpAddress { get; set; }
        public int RemotePort { get; set; }
    }
}
