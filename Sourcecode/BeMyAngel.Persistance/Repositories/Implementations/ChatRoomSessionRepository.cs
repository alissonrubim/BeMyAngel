using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatRoomSessionRepository : IChatRoomSessionRepository
    {
        private readonly IDatabase _database;
        public ChatRoomSessionRepository(IDatabase database)
        {
            _database = database;
        }

        public void AddSessionToChatRoom(int ChatRoomId, int SessionId)
        {
            _database.Execute(@"INSERT INTO [dbo].[ChatRoomSession]([ChatRoomId], [SessionId]) VALUES(@ChatRoomId, @SessionId)", new { SessionId, ChatRoomId });
        }

        public ChatRoomSessionDto GetCurrentChatRoomBySessionId(int SessionId)
        {
            return _database.Fetch<ChatRoomSessionDto>(@"SELECT 
	                                                            crs.[ChatRoomId], 
	                                                            crs.[SessionId] 
                                                         FROM [dbo].[ChatRoomSession] crs
                                                         INNER JOIN [dbo].[ChatRoom] cr
	                                                         ON cr.[ChatRoomId] = crs.[ChatRoomId]
                                                         WHERE 
                                                            crs.[SessionId] = @SessionId and 
                                                            cr.[TerminatedAt] is null", new { SessionId });
        }
    }
}
