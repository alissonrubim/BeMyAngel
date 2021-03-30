using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomEventRepository
    {
        IEnumerable<ChatRoomEventDto> GetAllByChatRoomId(int ChatRoomId);
        ChatRoomEventDto GetById(int ChatRoomEventId);
        int Insert(ChatRoomEventDto ChatRoomEvent);
    }
}
