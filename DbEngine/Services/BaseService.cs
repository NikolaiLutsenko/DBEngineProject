using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.SessionState;
using System.Web;
using System.Runtime.Caching;
using System.IO;

namespace DBEngineProject.Services
{
    public abstract class BaseService: Cacheble
    {
        protected string SqlConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=TestDBEngine;Integrated Security=True";

        private SqlCommand GetCommand(string sqlText, CommandType type, string connectionString)
        {
            var command = new SqlCommand(sqlText, new SqlConnection(connectionString))
            {
                CommandType = type
            };
            return command;
        }

        protected virtual SqlCommand CreateCommand(string sqlText)
        {
            return GetCommand(sqlText, CommandType.Text, SqlConnectionString);
        }

        protected virtual SqlCommand CreateCommand(string sqlText, CommandType type)
        {
            return GetCommand(sqlText, type, SqlConnectionString);
        }

        protected virtual SqlCommand CreateCommand(string sqlText, CommandType type, string connectionString)
        {
            return GetCommand(sqlText, type, connectionString);
        }
    }
}
