using BeMyAngel.Persistance.Generics;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories.ChatRoom
{
    public interface IChatRoomRepository
    {
        ChatRoomDto Get(int userId, int chatRoomId);
        ChatRoomDto GetCurrent(int userId);
    }
}
