using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomSessionRepository
    {
        ChatRoomSessionDto GetCurrentChatRoomBySessionId(int SessionId);

        void AddSessionToChatRoom(int ChatRoomId, int SessionId);
    }
}
