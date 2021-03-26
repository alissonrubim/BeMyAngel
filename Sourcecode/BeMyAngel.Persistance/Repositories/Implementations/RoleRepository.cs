using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            return _database.FecthAll<RoleDto>(@"SELECT [RoleId], [Name], [Identifier] FROM [dbo].[Role]");
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
