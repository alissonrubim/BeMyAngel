using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatRoomEventRepository : IChatRoomEventRepository
    {
        private readonly IDatabase _database;
        public ChatRoomEventRepository(IDatabase database)
        {
            _database = database;
        }

        public IEnumerable<ChatRoomEventDto> GetAllByChatRoomId(int ChatRoomId)
        {
            return _database.FetchAll<ChatRoomEventDto>(@"SELECT 
                                                            [ChatRoomEventId],
	                                                        [ChatRoomId],
	                                                        [ChatRoomEventTypeId],
	                                                        [CreatedAt],
	                                                        [Data],
	                                                        [SessionId]
                                                        FROM [dbo].[ChatRoomEvent]
                                                        WHERE
                                                           [ChatRoomId] = @ChatRoomId", new { ChatRoomId });
        }

        public ChatRoomEventDto GetById(int ChatRoomEventId)
        {
            return _database.Fetch<ChatRoomEventDto>(@"SELECT 
                                                            [ChatRoomEventId],
	                                                        [ChatRoomId],
	                                                        [ChatRoomEventTypeId],
	                                                        [CreatedAt],
	                                                        [Data],
	                                                        [SessionId]
                                                        FROM [dbo].[ChatRoomEvent]
                                                        WHERE
                                                           [ChatRoomEventId] = @ChatRoomEventId", new { ChatRoomEventId });
        }

        public int Insert(ChatRoomEventDto ChatRoomEvent)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[ChatRoomEvent]([ChatRoomId], [ChatRoomEventTypeId], [CreatedAt], [Data], [SessionId])  
                                          OUTPUT INSERTED.ChatRoomEventId  
                                          VALUES(@ChatRoomId, @ChatRoomEventTypeId, @CreatedAt, @Data, @SessionId)", ChatRoomEvent);
        }
    }
}
