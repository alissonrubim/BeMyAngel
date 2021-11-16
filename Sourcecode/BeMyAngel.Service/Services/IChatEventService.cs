using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services
{
    public interface IChatEventService
    {
        IEnumerable<ChatEvent> GetAllByChatId(int ChatId, int SessionId);
        ChatEvent GetById(int ChatEventId);
        int CreatePostMessageEvent(int ChatId, string Message, Session Session);
    }
}
