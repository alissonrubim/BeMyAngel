using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatRoomSessionRepository : IChatRoomSessionRepository
    {
        private readonly IDatabase _database;
        public ChatRoomSessionRepository(IDatabase database)
        {
            _database = database;
        }

        public ChatRoomSessionDto GetByToken(string Token)
        {
            return _database.Fetch<ChatRoomSessionDto>(@"SELECT [ChatRoomId], [SessionId], [Token] FROM [dbo].[ChatRoomSession] WHERE [Token] = @Token", new { Token });
        }

        public ChatRoomSessionDto Get(int ChatRoomId, int SessionId)
        {
            return _database.Fetch<ChatRoomSessionDto>(@"SELECT [ChatRoomId], [SessionId], [Token] FROM [dbo].[ChatRoomSession] WHERE [ChatRoomId] = @ChatRoomId AND [SessionId] = @SessionId", new { ChatRoomId, SessionId });
        }

        public ChatRoomSessionDto GetBySessionId(int SessionId, bool IncludeClosedChatRooms = false)
        {
            if (IncludeClosedChatRooms)
                throw new NotImplementedException("The IncludeClosedChatRooms its not implemented yet!");

            return _database.Fetch<ChatRoomSessionDto>(@"SELECT 
	                                                            crs.[ChatRoomId], 
	                                                            crs.[SessionId],
                                                                crs.[Token]
                                                         FROM [dbo].[ChatRoomSession] crs
                                                         INNER JOIN [dbo].[ChatRoom] cr
	                                                         ON cr.[ChatRoomId] = crs.[ChatRoomId]
                                                         WHERE 
                                                            crs.[SessionId] = @SessionId and 
                                                            cr.[TerminatedAt] is null", new { SessionId });
        }

        public void AddSessionToChatRoom(int ChatRoomId, int SessionId)
        {
            var token = string.Empty;
            do
            {
                token = Guid.NewGuid().ToString().ToUpper();
            } while (GetByToken(token) != null);

            _database.Execute(@"INSERT INTO [dbo].[ChatRoomSession]([ChatRoomId], [SessionId], [Token]) VALUES(@ChatRoomId, @SessionId, @token)", new { SessionId, ChatRoomId, token });
        }

        
    }
}
