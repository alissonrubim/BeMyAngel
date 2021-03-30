using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Presentations.ChatRoomController
{
    public class GetCurrentResponse
    {
        public ChatRoom ChatRoom { get; set; }
        public IEnumerable<ChatRoomEvent> ChatRoomEvents { get; set; }
    }
}
