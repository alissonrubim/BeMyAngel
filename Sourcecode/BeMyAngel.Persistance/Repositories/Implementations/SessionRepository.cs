using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class SessionRepository: ISessionRepository
    {
        private readonly IDatabase _database;
        public SessionRepository(IDatabase database)
        {
            _database = database;
        }

        public SessionDto GetByToken(string Token)
        {
            return _database.Fetch<SessionDto>(@"SELECT 
                                                    [SessionId], 
                                                    [Token], 
                                                    [UserAgent], 
                                                    [CreatedAt], 
                                                    [LastAccessAt], 
                                                    [UserId], 
                                                    [LocalIpAddress], 
                                                    [LocalPort], 
                                                    [RemoteIpAddress],
                                                    [RemotePort]
                                                FROM [dbo].[Session] WHERE [Token] = @Token", new { Token });
        }

        public SessionDto GetById(int SessionId)
        {
            return _database.Fetch<SessionDto>(@"SELECT 
                                                    [SessionId], 
                                                    [Token], 
                                                    [UserAgent], 
                                                    [CreatedAt], 
                                                    [LastAccessAt], 
                                                    [UserId], 
                                                    [LocalIpAddress], 
                                                    [LocalPort], 
                                                    [RemoteIpAddress],
                                                    [RemotePort]
                                                FROM [dbo].[Session] WHERE [SessionId] = @SessionId", new { SessionId });
        }


        public int Insert(SessionDto Session)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[Session](
                                                    [Token], 
                                                    [UserAgent], 
                                                    [CreatedAt], 
                                                    [LastAccessAt], 
                                                    [UserId], 
                                                    [LocalIpAddress], 
                                                    [LocalPort], 
                                                    [RemoteIpAddress],
                                                    [RemotePort]
                                         ) 
                                        OUTPUT INSERTED.SessionId 
                                        VALUES(
                                                    @Token, 
                                                    @UserAgent, 
                                                    @CreatedAt,
                                                    @LastAccessAt,
                                                    @UserId,
                                                    @LocalIpAddress,
                                                    @LocalPort,
                                                    @RemoteIpAddress,
                                                    @RemotePort
                                         )", Session);
        }

        public void Update(SessionDto Session)
        {
            _database.Execute(@"UPDATE [dbo].[Session] SET [LastAccessAt] = @LastAccessAt, [UserId] = @UserId WHERE [SessionId] = @SessionId", Session);
        }
    }
}
