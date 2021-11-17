using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IChatService
    {
        Chat GetCurrentBySession(Session session);
        Chat GetById(int chatId, Session session);
        Chat GetByIdentifier(string identifier, Session session);
        Chat Create(Session session);
    }
}
