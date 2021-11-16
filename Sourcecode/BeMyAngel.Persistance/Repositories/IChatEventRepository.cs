using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatEventRepository
    {
        IEnumerable<ChatEventDto> GetAllByChatId(int ChatId);
        ChatEventDto GetById(int ChatEventId);
        int Insert(ChatEventDto ChatEvent);
    }
}
