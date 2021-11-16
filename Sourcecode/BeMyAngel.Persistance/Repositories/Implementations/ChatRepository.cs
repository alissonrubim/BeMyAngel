using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatRepository : IChatRepository
    {
        private readonly IDatabase _database;
        public ChatRepository(IDatabase database)
        {
            _database = database;
        }
        public ChatDto GetById(int ChatId)
        {
            return _database.Fetch<ChatDto>(@"SELECT 
                                                    cr.[ChatId], 
                                                    cr.[CreatedAt], 
                                                    cr.[TerminatedAt],
                                                    cr.[Identifier]
                                                FROM [Chat] cr
                                                WHERE 
                                                    cr.[ChatId] = @ChatId", new { ChatId });
        }

        public ChatDto GetByIdentifier(string Identifier)
        {
            return _database.Fetch<ChatDto>(@"SELECT 
                                                    cr.[ChatId], 
                                                    cr.[CreatedAt], 
                                                    cr.[TerminatedAt],
                                                    cr.[Identifier]
                                                FROM [Chat] cr
                                                WHERE 
                                                    cr.[Identifier] = @Identifier", new { Identifier });
        }

        public int Insert(ChatDto Chat)
        { 
            return _database.Fetch<int>(@"INSERT INTO [dbo].[Chat]([CreatedAt], [Identifier]) OUTPUT INSERTED.ChatId VALUES(@CreatedAt, @Identifier)", Chat);
        }
    }
}
