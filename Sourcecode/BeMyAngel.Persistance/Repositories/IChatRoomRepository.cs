using BeMyAngel.Persistance.Generics;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomRepository
    {
        ChatRoomDto GetById(int ChatRoomId);

        int Insert(ChatRoomDto ChatRoom);
    }
}
