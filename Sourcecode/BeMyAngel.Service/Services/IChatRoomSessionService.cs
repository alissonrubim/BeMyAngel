using BeMyAngel.Service.Models;

namespace BeMyAngel.Service.Services
{
    public interface IChatRoomSessionService
    {
        ChatRoomSession Get(int ChatRoomId, int SessionId);
        ChatRoomSession GetByToken(string Token);
        ChatRoomSession GetBySessionId(int SessionId, bool IncludeClosedChatRooms = false);
        void AddSessionToChatRoom(int ChatRoomId, int SessionId);
    }
}
