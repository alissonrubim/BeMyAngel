using BeMyAngel.Persistance.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories.Implementations
{
    internal class DatabaseSchemaRepostory : IDatabaseSchemaRepostory
    {
        private readonly IDatabase _database;
        public DatabaseSchemaRepostory(IDatabase database)
        {
            _database = database;
        }

        private void SetConnectDirectToDatabase(bool value)
        {
            _database.Configure(new DatabaseConfiguration
            {
                ConnectDirectToDatabase = value //when true: just connect to the server, not the database
            });
        }


        public void CreateDatabase(string DatabaseName)
        {
            SetConnectDirectToDatabase(false);
            _database.Execute($"CREATE DATABASE [{DatabaseName}]");
            SetConnectDirectToDatabase(true);
        }

        public bool DatabaseExists(string DatabaseName)
        {
            SetConnectDirectToDatabase(false);
            var databaseExists = _database.Fetch<int>(@"SELECT count(name) FROM master.dbo.sysdatabases WHERE name = @DatabaseName", new { DatabaseName }) == 0;
            SetConnectDirectToDatabase(true);
            return databaseExists;
        }
    }
}
