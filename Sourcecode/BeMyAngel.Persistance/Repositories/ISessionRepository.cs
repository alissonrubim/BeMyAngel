using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
