using DBEngineProject.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Engine
{

    #region Class: DBCommand

    [DebuggerDisplay("CommandText = {CommandText}, ConnectionState = {ConnectionState}")]
    public class DBCommand: IDisposable
    {

        #region Properties: Public

        private DBConnection _DBConnection;
        public DBConnection DBConnection
        {
            get { return _DBConnection; }
            set { _DBConnection = value ?? throw new ArgumentNullException("DBConnection"); }
        }

        public string CommandText {
            get { return command.CommandText; }
            set { command.CommandText = value ?? throw new ArgumentNullException("CommandText"); }
        }

        public virtual ConnectionState ConnectionState
        {
            get { return command.Connection.State; }
        }

        public SqlTransaction Transaction { get; protected set; }

        #endregion

        #region Fields: Protected

        protected SqlCommand command;

        #endregion

        #region Constructors: Public

        protected DBCommand()
        {
            command = new SqlCommand();
        }

        public DBCommand(DBConnection connection)
            :this()
        {
            DBConnection = connection;
            command.Connection = DBConnection.SqlConnection;
        }

        public DBCommand(DBConnection connection, string commandText)
            :this(connection)
        {
            CommandText = commandText;
        }

        public DBCommand(DBConnection connection, string commandText, CommandType commandType)
            :this(connection, commandText) 
        {
            command.CommandType = commandType;
        }

        #endregion

        #region Methods: Protected

        protected void OpenCloseConnection(Action action)
        {
            if (DBConnection == null)
                throw new ArgumentNullException("command.Connection");
            try
            {
                OpenConnection();
                action();
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (ConnectionState == ConnectionState.Open)
                    CloseConnection();
            }
        }

        #endregion

        #region Methods: Public

        public virtual void OpenConnection()
        {
            DBConnection.OpenConnection();
        }

        public virtual void CloseConnection()
        {
            DBConnection.CloseConnection();
        }

        public virtual DBCommand AddParameter(string name, object value)
        {
            command.Parameters.AddWithValue(name, value);
            return this;
        }

        public virtual DBCommand AddParameters(Dictionary<string, object> values)
        {
            values.ForEach(value => AddParameter(value.Key, value.Value));
            return this;
        }

        public virtual SqlDataReader ExecuteReader()
        {
            return command.ExecuteReader();
        }

        public void BeginTransaction()
        {
            if(Transaction != null)
                throw new Exception("Транзакция уже запущена");
            Transaction = DBConnection.SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public object Execute()
        {
            object result = null;
            OpenCloseConnection(() => {
                result = command.ExecuteScalar();
            });
            return result;
        }

        public int ExecuteNonQuery()
        {
            int result = -1;
            OpenCloseConnection(() => {
                result = command.ExecuteNonQuery();
            });
            return result;
        }

        public void Dispose()
        {
            Transaction?.Dispose();
        }

        ~DBCommand()
        {
            Dispose();
            GC.Collect(0);
        }

        #endregion

    }

    #endregion

}
