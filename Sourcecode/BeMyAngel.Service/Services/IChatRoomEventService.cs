using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IChatRoomEventService
    {
        IEnumerable<ChatRoomEvent> GetAllByChatRoomId(int ChatRoomId, int SessionId);
        ChatRoomEvent GetById(int ChatRoomEventId);
        int CreatePostMessageEvent(int ChatRoomId, string Message, Session Session);
    }
}
