using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatRoomRepository : IChatRoomRepository
    {
        private readonly IDatabase _database;
        public ChatRoomRepository(IDatabase database)
        {
            _database = database;
        }
        public ChatRoomDto GetById(int ChatRoomId)
        {
            return _database.Fetch<ChatRoomDto>(@"SELECT 
                                                    cr.[ChatRoomId], 
                                                    cr.[CreatedAt], 
                                                    cr.[TerminatedAt] 
                                                FROM [ChatRoom] cr
                                                WHERE 
                                                    cr.[ChatRoomId] = @ChatRoomId", new { ChatRoomId });
        }

        public int Insert(ChatRoomDto ChatRoom)
        { 
            return _database.Fetch<int>(@"INSERT INTO [dbo].[ChatRoom]([CreatedAt]) OUTPUT INSERTED.ChatRoomId VALUES(@CreatedAt)", ChatRoom);
        }
    }
}
