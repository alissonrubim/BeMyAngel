using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface ISessionRepository
    {
        SessionDto GetByToken(string Token);
        SessionDto GetById(int SessionId);
        int Insert(SessionDto session);
        void Update(SessionDto session);
    }
}
