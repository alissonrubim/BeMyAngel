using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BeMyAngel.Persistance.Helpers
{
    internal class Database : IDatabase
    {
        private readonly PersistanceSettings _settings;
        private DatabaseConfiguration _configuration;
        public Database(PersistanceSettings settings)
        {
            _settings = settings;
            _configuration = new DatabaseConfiguration();
        }

        public void Execute(string command, object parms, CommandType commandType = CommandType.Text)
        {
            using (var context = GetDbConnection())
            {
                context.Execute(command, parms, commandType: commandType);
            };
        }

        public List<T> FetchAll<T>(string query, object parms, CommandType commandType = CommandType.Text)
        {
            using (var context = GetDbConnection())
            {
                return context.Query<T>(query, parms, commandType: commandType).ToList();
            }
        }

        public T Fetch<T>(string query, object parms, CommandType commandType = CommandType.Text)
        {
            using (var context = GetDbConnection())
            {
                return context.Query<T>(query, parms, commandType: commandType).FirstOrDefault();
            }
        }

        public DbConnection GetDbConnection()
        {
            var connectionBuilder = new List<string>();
            connectionBuilder.Add($"data source={_settings.Host}");
            if(_configuration.ConnectDirectToDatabase)
                connectionBuilder.Add($"initial catalog={_settings.Database}");
            connectionBuilder.Add($"persist security info=True");
            connectionBuilder.Add($"Integrated Security=SSPI");
            return new SqlConnection(String.Join(';', connectionBuilder.ToArray()));
        }

        public void Configure(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
