using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomEventRepository
    {
        IEnumerable<ChatRoomEventDto> GetByChatRoomId(int ChatRoomId);
    }
}
