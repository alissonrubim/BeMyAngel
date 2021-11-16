using BeMyAngel.Service.Models;

namespace BeMyAngel.Service.Services
{
    public interface IChatSessionService
    {
        ChatSession Get(int ChatSessionId);
        ChatSession Get(int ChatId, int SessionId);
        ChatSession GetByToken(string Token);
        ChatSession GetBySessionId(int SessionId, bool IncludeClosedChats = false);
        void AddSessionToChat(int ChatId, int SessionId);
        void DefineConnectionId(int ChatSessionId, string ConnectionId);
    }
}
