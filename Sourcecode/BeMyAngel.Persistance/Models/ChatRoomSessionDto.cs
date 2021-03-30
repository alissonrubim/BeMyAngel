using System.ComponentModel.DataAnnotations;

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
