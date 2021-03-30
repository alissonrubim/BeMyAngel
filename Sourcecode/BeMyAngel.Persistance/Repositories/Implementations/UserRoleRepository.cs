using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDatabase _database;

        public UserRoleRepository(IDatabase database)
        {
            _database = database;
        }

        public IEnumerable<UserRoleDto> GetByUserId(int UserId)
        {
            return _database.FetchAll<UserRoleDto>(@"SELECT [UserId], [RoleId] FROM [dbo].[UserRole] WHERE [UserId] = @UserId", new { UserId });
        }
    }
}
