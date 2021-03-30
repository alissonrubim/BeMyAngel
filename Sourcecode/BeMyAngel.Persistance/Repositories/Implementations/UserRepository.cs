using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class UserRepository: IUserRepository
    {
        private readonly IDatabase _database;
        public UserRepository(IDatabase database)
        {
            _database = database;
        }

        public UserDto GetById(int UserId)
        {
            return _database.Fetch<UserDto>(@"SELECT [UserId], [Username], [Password], [EncryptKey], [IsEnabled] FROM [dbo].[User] WHERE [UserId] = @UserId", new { UserId });
        }

        public UserDto GetByUserName(string UserName)
        {
            return _database.Fetch<UserDto>(@"SELECT [UserId], [UserName], [Password], [EncryptKey], [IsEnabled] FROM [dbo].[User] WHERE [UserName] = @UserName", new { UserName });
        }
    }
}
