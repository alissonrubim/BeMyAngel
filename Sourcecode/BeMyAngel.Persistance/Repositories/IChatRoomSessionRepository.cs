using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRoomSessionRepository
    {
        ChatRoomSessionDto GetCurrentChatRoomBySessionId(int SessionId);

        void AddSessionToChatRoom(int ChatRoomId, int SessionId);
    }
}
