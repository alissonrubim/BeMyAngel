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
        public Database(PersistanceSettings settings)
        {
            _settings = settings;
        }

        public void Execute(string command, object parms, CommandType commandType = CommandType.Text)
        {
            using (var context = GetDbConnection())
            {
                context.Execute(command, parms, commandType: commandType);
            };
        }

        public List<T> FecthAll<T>(string query, object parms, CommandType commandType = CommandType.Text)
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
            return new SqlConnection(_settings.DatabaseConnectionString);
        }
    }
}
