using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatEventRepository : IChatEventRepository
    {
        private readonly IDatabase _database;
        public ChatEventRepository(IDatabase database)
        {
            _database = database;
        }

        public IEnumerable<ChatEventDto> GetAllByChatId(int ChatId)
        {
            return _database.FetchAll<ChatEventDto>(@"SELECT 
                                                            [ChatEventId],
	                                                        [ChatId],
	                                                        [ChatEventTypeId],
	                                                        [CreatedAt],
	                                                        [Data],
	                                                        [ChatSessionId]
                                                        FROM [dbo].[ChatEvent]
                                                        WHERE
                                                           [ChatId] = @ChatId", new { ChatId });
        }

        public ChatEventDto GetById(int ChatEventId)
        {
            return _database.Fetch<ChatEventDto>(@"SELECT 
                                                            [ChatEventId],
	                                                        [ChatId],
	                                                        [ChatEventTypeId],
	                                                        [CreatedAt],
	                                                        [Data],
	                                                        [ChatSessionId]
                                                        FROM [dbo].[ChatEvent]
                                                        WHERE
                                                           [ChatEventId] = @ChatEventId", new { ChatEventId });
        }

        public int Insert(ChatEventDto ChatEvent)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[ChatEvent]([ChatId], [ChatEventTypeId], [CreatedAt], [Data], [ChatSessionId])  
                                          OUTPUT INSERTED.ChatEventId  
                                          VALUES(@ChatId, @ChatEventTypeId, @CreatedAt, @Data, @ChatSessionId)", ChatEvent);
        }
    }
}
