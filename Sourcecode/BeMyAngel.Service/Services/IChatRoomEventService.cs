using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IChatRoomEventService
    {
        IEnumerable<ChatRoomEvent> GetByChatRoomId(int ChatRoomId);
    }
}
