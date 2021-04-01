using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomSessionRepository
    {
        ChatRoomSessionDto Get(int ChatRoomSessionId);
        ChatRoomSessionDto Get(int ChatRoomId, int SessionId);
        ChatRoomSessionDto GetByToken(string Token);
        ChatRoomSessionDto GetBySessionId(int SessionId, bool IncludeClosedChatRooms = false);
        int Insert(int ChatRoomId, int SessionId, string Token);
    }
}
