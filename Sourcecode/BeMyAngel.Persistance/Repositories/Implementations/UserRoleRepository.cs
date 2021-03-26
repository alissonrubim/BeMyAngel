using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            return _database.FecthAll<UserRoleDto>(@"SELECT [UserId], [RoleId] FROM [dbo].[UserRole] WHERE [UserId] = @UserId", new { UserId });
        }
    }
}
