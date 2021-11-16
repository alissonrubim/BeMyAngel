using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IChatService
    {
        Chat GetCurrentBySession(Session session);
        Chat GetById(int ChatId, Session session);
        Chat Create(Session session);
    }
}
