using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IChatRoomEventService
    {
        IEnumerable<ChatRoomEvent> GetAllByChatRoomId(int ChatRoomId);
        ChatRoomEvent GetById(int ChatRoomEventId);
        int CreatePostMessageEvent(Session Session, string Message);
    }
}
