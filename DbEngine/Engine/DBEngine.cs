using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Connections;
using DBEngineProject.Managers;
using System.Data.SqlClient;
using DBEngineProject.Interfaces;
using System.Data;
using DBEngineProject.Entities;
using DBEngineProject.Query;

namespace DBEngineProject.Engine
{

    #region Class: DBEngine

    /// <summary>
    /// Class uses for operation with data base
    /// </summary>
    public class DBEngine: IDisposable
    { 

        #region Properties: Public

        private DBConnection _dbConnection;

        public virtual DBConnection DbConnection
        {
            get { return _dbConnection; }
            protected set
            {
                _dbConnection = value ?? throw new ArgumentNullException("DbConnection");
            }
        }

        private ConnectionManager _connectionManager;

        public virtual ConnectionManager ConnectionManager
        {
            get { return _connectionManager; }
            protected set
            {
                _connectionManager = value ?? throw new ArgumentNullException("ConnectionManager");
            }
        }

        #endregion

        #region Constructors: Public

        protected DBEngine()
        {
            ConnectionManager = ConnectionManager.GetInstance();
        }

        public DBEngine(DBConnection dbConnection):this()
        {
            DbConnection = dbConnection;
        }

        public DBEngine(string connectionString): this()
        {
            DbConnection = ConnectionManager.GetConnection<DBConnection>(connectionString);
        }

        #endregion

        #region Methods: Public

        public virtual EntityTable GetTable(DBCommand command) 
        {
            EntityTable result = null;
            if (command.DBConnection == null)
                throw new ArgumentNullException("command.Connection");

            try
            {
                command.OpenConnection();
                SqlDataReader reader = command.ExecuteReader();
                result = new EntityTable(reader, command.CommandText);
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (command.ConnectionState == ConnectionState.Open)
                    command.CloseConnection();
            }

            return result;
        }

        public virtual EntityTable GetTable(SelectQuery selectQuery)
        {
            return GetTable(CreateCommand(selectQuery));
        }

        public virtual object ExecuteScalar(DBCommand command)
        {
            return command.Execute();
        }

        public virtual object ExecuteScalar(SelectQuery selectQuery)
        {
            return ExecuteScalar(CreateCommand(selectQuery));
        }

        public virtual int ExecuteNonQuery(NonQuery nonQuery)
        {
            return ExecuteNonQuery(CreateCommand(nonQuery));
        }

        public virtual int ExecuteNonQuery(DBCommand command)
        {
            return command.ExecuteNonQuery();
        }

        public virtual DBCommand CreateCommand(string command)
        {
            return DbConnection.CreateCommand(command);
        }

        public virtual DBCommand CreateCommand(DBEngineProject.Query.Query query)
        {
            return CreateCommand(query.GetSqlText());
        }

        public virtual DBCommand CreateCommand(string command, CommandType commandType)
        {
            return DbConnection.CreateCommand(command, commandType);
        }

        #endregion

        #region Methods: Implement

        public void Dispose()
        {
            DbConnection.Dispose();
        }

        #endregion

    }

    #endregion

}
