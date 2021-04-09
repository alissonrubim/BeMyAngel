using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System.Collections.Generic;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class SettingRepository : ISettingRepository
    {
        private readonly IDatabase _database;
        public SettingRepository(IDatabase database)
        {
            _database = database;
        }
        public SettingDto GetByIdentifier(string Identifier)
        {
            return _database.Fetch<SettingDto>(@"SELECT [SettingId], [Identifier], [Value] FROM [dbo].[Setting] WHERE [Identifier] = @Identifier", new { Identifier });
        }

        public int Insert(string Identifier, string Value)
        {
            return _database.Fetch<int>(@"INSERT INTO [dbo].[Setting]([Value], [Identifier]) OUTPUT INSERTED.SettingId  VALUES(@Value, @Identifier)", new { Identifier, Value });
        }

        public void Update(string Identifier, string Value)
        {
            _database.Execute("UPDATE [dbo].[Setting] SET [Value] = @Value WHERE [Identifier] = @Identifier", new { Identifier, Value });
        }
    }
}
