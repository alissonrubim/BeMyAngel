using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomRepository
    {
        ChatRoomDto GetById(int ChatRoomId);

        int Insert(ChatRoomDto ChatRoom);
    }
}
