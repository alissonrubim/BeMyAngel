using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            return _database.Fetch<SessionDto>(@"SELECT [SessionId], [Token], [IpAddress], [UserAgent], [CreatedAt], [LastAccessAt], [UserId] FROM [dbo].[Session] WHERE [Token] = @Token", new { Token });
        }

        public SessionDto GetById(int SessionId)
        {
            return _database.Fetch<SessionDto>(@"SELECT [SessionId], [Token], [IpAddress], [UserAgent], [CreatedAt], [LastAccessAt], [UserId] FROM [dbo].[Session] WHERE [SessionId] = @SessionId", new { SessionId });
        }


        public int Insert(SessionDto Session)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[Session]([Token], [IpAddress], [UserAgent], [CreatedAt]) OUTPUT INSERTED.SessionId VALUES(@Token, @IpAddress, @UserAgent, @CreatedAt)", Session);
        }

        public void Update(SessionDto Session)
        {
            _database.Execute(@"UPDATE [dbo].[Session] SET [LastAccessAt] = @LastAccessAt, [UserId] = @UserId WHERE [SessionId] = @SessionId", Session);
        }
    }
}
