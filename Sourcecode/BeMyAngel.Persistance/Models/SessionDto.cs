using System;
using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class SessionDto
    {
        [Key]
        public int SessionId { get; set; }
        public string Token { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastAccessAt { get; set; }
        public int? UserId { get; set; }
        public string LocalIpAddress { get; set; }
        public string LocalPort { get; set; }
        public string RemoteIpAddress { get; set; }
        public string RemotePort { get; set; }
        public string ConnectionIdentifier { get; set; }
    }
}
