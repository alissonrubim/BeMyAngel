using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly IDatabase _database;
        public RoleRepository(IDatabase database)
        {
            _database = database;
        }
        public IEnumerable<RoleDto> GetAll()
        {
            return _database.FetchAll<RoleDto>(@"SELECT [RoleId], [Name], [Identifier] FROM [dbo].[Role]");
        }

        public RoleDto GetById(int RoleId)
        {
            return _database.Fetch<RoleDto>(@"SELECT [RoleId], [Name], [Identifier] FROM [dbo].[Role] WHERE [RoleId] = @RoleId", new { RoleId });
        }

        public RoleDto GetByIdentifier(string Identifier)
        {
            return _database.Fetch<RoleDto>(@"SELECT [RoleId], [Name], [Identifier] FROM [dbo].[Role] WHERE [Identifier] = @Identifier", new { Identifier });
        }
    }
}
