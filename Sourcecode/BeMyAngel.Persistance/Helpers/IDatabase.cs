using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace BeMyAngel.Persistance.Helpers
{
    internal interface IDatabase
    {
        DbConnection GetDbConnection();
        T Fetch<T>(string query, object parms = null, CommandType commandType = CommandType.Text);
        List<T> FetchAll<T>(string query, object parms = null, CommandType commandType = CommandType.Text);
        void Execute(string command, object parms = null, CommandType commandType = CommandType.Text);
        void Configure(DatabaseConfiguration configuration);
    }
}
