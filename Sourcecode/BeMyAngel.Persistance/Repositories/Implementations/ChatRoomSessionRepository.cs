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
            return _database.Fetch<ChatRoomSessionDto>(@"SELECT 
                                                            [ChatRoomSessionId],
                                                            [ChatRoomId], 
                                                            [SessionId], 
                                                            [Token] 
                                                        FROM [dbo].[ChatRoomSession] WHERE [Token] = @Token", new { Token });
        }

        public ChatRoomSessionDto Get(int ChatRoomSessionId)
        {
            return _database.Fetch<ChatRoomSessionDto>(@"SELECT 
                                                            [ChatRoomSessionId],
                                                            [ChatRoomId], 
                                                            [SessionId], 
                                                            [Token] 
                                                         FROM [dbo].[ChatRoomSession] WHERE [ChatRoomSessionId] = @ChatRoomSessionId", new { ChatRoomSessionId });
        }

        public ChatRoomSessionDto Get(int ChatRoomId, int SessionId)
        {
            return _database.Fetch<ChatRoomSessionDto>(@"SELECT 
                                                            [ChatRoomSessionId],
                                                            [ChatRoomId], 
                                                            [SessionId], 
                                                            [Token] 
                                                         FROM [dbo].[ChatRoomSession] WHERE [ChatRoomId] = @ChatRoomId AND [SessionId] = @SessionId", new { ChatRoomId, SessionId });
        }

        public ChatRoomSessionDto GetBySessionId(int SessionId, bool IncludeClosedChatRooms = false)
        {
            if (IncludeClosedChatRooms)
                throw new NotImplementedException("The IncludeClosedChatRooms its not implemented yet!");

            return _database.Fetch<ChatRoomSessionDto>(@"SELECT 
                                                                crs.[ChatRoomSessionId], 
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

        public int Insert(int ChatRoomId, int SessionId, string Token)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[ChatRoomSession]([ChatRoomId], [SessionId], [Token]) 
                                          OUTPUT INSERTED.ChatRoomSessionId 
                                          VALUES(@ChatRoomId, @SessionId, @token)", new { SessionId, ChatRoomId, Token });
        }
    }
}
