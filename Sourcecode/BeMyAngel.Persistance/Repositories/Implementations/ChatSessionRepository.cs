        using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class ChatSessionRepository : IChatSessionRepository
    {
        private readonly IDatabase _database;
        public ChatSessionRepository(IDatabase database)
        {
            _database = database;
        }

        public ChatSessionDto GetByToken(string Token)
        {
            return _database.Fetch<ChatSessionDto>(@"SELECT 
                                                            [ChatSessionId],
                                                            [ChatId], 
                                                            [SessionId], 
                                                            [Token] 
                                                        FROM [dbo].[ChatSession] WHERE [Token] = @Token", new { Token });
        }

        public ChatSessionDto Get(int ChatSessionId)
        {
            return _database.Fetch<ChatSessionDto>(@"SELECT 
                                                            [ChatSessionId],
                                                            [ChatId], 
                                                            [SessionId], 
                                                            [Token] 
                                                         FROM [dbo].[ChatSession] WHERE [ChatSessionId] = @ChatSessionId", new { ChatSessionId });
        }

        public ChatSessionDto Get(int ChatId, int SessionId)
        {
            return _database.Fetch<ChatSessionDto>(@"SELECT 
                                                            [ChatSessionId],
                                                            [ChatId], 
                                                            [SessionId], 
                                                            [Token] 
                                                         FROM [dbo].[ChatSession] WHERE [ChatId] = @ChatId AND [SessionId] = @SessionId", new { ChatId, SessionId });
        }

        public ChatSessionDto GetBySessionId(int SessionId, bool IncludeClosedChats = false)
        {
            if (IncludeClosedChats)
                throw new NotImplementedException("The IncludeClosedChats its not implemented yet!");

            return _database.Fetch<ChatSessionDto>(@"SELECT 
                                                                crs.[ChatSessionId], 
	                                                            crs.[ChatId], 
	                                                            crs.[SessionId],
                                                                crs.[Token]
                                                         FROM [dbo].[ChatSession] crs
                                                         INNER JOIN [dbo].[Chat] cr
	                                                         ON cr.[ChatId] = crs.[ChatId]
                                                         WHERE 
                                                            crs.[SessionId] = @SessionId and 
                                                            cr.[TerminatedAt] is null", new { SessionId });
        }

        public int Insert(int ChatId, int SessionId, string Token)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[ChatSession]([ChatId], [SessionId], [Token]) 
                                          OUTPUT INSERTED.ChatSessionId 
                                          VALUES(@ChatId, @SessionId, @token)", new { SessionId, ChatId, Token });
        }
    }
}
