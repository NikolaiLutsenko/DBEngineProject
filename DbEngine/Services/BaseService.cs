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

	/// <summary>
	/// Class for work with db (invoke procedure, DML and etc.).
	/// </summary>
    public abstract class BaseService: Cacheble
    {

		#region Protected: Properties

		/// <summary>
		/// Connection string.
		/// </summary>
		protected string SqlConnectionString { get; private set; }

		#endregion

		#region Pubblic: Constructors

		/// <summary>
		/// Ctor.
		/// </summary>
		/// <param name="connectionString">Connection string for db.</param>
		public BaseService(string connectionString)
		{
			SqlConnectionString = connectionString;
		}

		#endregion

		#region Private: Methods

		/// <summary>
		/// Gets data base command. 
		/// </summary>
		/// <param name="sqlText">Sql text.</param>
		/// <param name="type">Command type text or procedure.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>Sql command.</returns>
		private SqlCommand GetCommand(string sqlText, CommandType type, string connectionString)
        {
            var command = new SqlCommand(sqlText, new SqlConnection(connectionString))
            {
                CommandType = type
            };
            return command;
        }

		#endregion

		#region Protected: Methods

		/// <summary>
		/// Gets sql command.
		/// </summary>
		/// <param name="sqlText">Sql command.</param>
		/// <returns>Returns database command.</returns>
		protected virtual SqlCommand CreateCommand(string sqlText)
        {
            return GetCommand(sqlText, CommandType.Text, SqlConnectionString);
        }

		/// <summary>
		/// Gets sql command.
		/// </summary>
		/// <param name="sqlText">Sql command.</param>
		/// <param name="type">Command type: text or procedure.</param>
		/// <returns>Returns database command.</returns>
		protected virtual SqlCommand CreateCommand(string sqlText, CommandType type)
        {
            return GetCommand(sqlText, type, SqlConnectionString);
        }

		/// <summary>
		/// Gets sql command.
		/// </summary>
		/// <param name="sqlText">Sql command.</param>
		/// <param name="type">Command type: text or procedure.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>Returns database command.</returns>
		protected virtual SqlCommand CreateCommand(string sqlText, CommandType type, string connectionString)
        {
            return GetCommand(sqlText, type, connectionString);
        }

		#endregion

	}
}
