using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatSessionRepository
    {
        ChatSessionDto Get(int ChatSessionId);
        ChatSessionDto Get(int ChatId, int SessionId);
        ChatSessionDto GetByToken(string Token);
        ChatSessionDto GetBySessionId(int SessionId, bool IncludeClosedChats = false);
        int Insert(int ChatId, int SessionId, string Token);
    }
}
