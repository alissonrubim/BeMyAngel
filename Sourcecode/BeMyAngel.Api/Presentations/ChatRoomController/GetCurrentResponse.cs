using BeMyAngel.Service.Models;
using System.Collections.Generic;

namespace BeMyAngel.Api.Presentations.ChatRoomController
{
    public class GetCurrentResponse
    {
        public ChatRoom ChatRoom { get; set; }
        public IEnumerable<ChatRoomEvent> ChatRoomEvents { get; set; }
    }
}
