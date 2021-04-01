using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomSessionRepository
    {
        ChatRoomSessionDto Get(int ChatRoomId, int SessionId);
        ChatRoomSessionDto GetByToken(string Token);
        ChatRoomSessionDto GetBySessionId(int SessionId, bool IncludeClosedChatRooms = false);
        void AddSessionToChatRoom(int ChatRoomId, int SessionId);
    }
}
