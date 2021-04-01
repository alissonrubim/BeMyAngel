using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface ISessionService
    {
        Session GetByToken(string Token);
        Session GetById(int SessionId);
        int Create(Session session);
        void Renew(Session session);
        void AttachUser(Session session, User user);
        void DeattachUser(Session session);
    }
}
