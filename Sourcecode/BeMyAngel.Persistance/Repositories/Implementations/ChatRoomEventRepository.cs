using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatRoomEventRepository : IChatRoomEventRepository
    {
        private readonly IDatabase _database;
        public ChatRoomEventRepository(IDatabase database)
        {
            _database = database;
        }

        public IEnumerable<ChatRoomEventDto> GetByChatRoomId(int ChatRoomId)
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
    }
}
