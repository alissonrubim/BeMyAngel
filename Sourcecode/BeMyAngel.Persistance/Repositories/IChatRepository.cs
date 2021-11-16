using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IChatRepository
    {
        ChatDto GetById(int ChatId);

        ChatDto GetByIdentifier(string Identifier);

        int Insert(ChatDto Chat);
    }
}
