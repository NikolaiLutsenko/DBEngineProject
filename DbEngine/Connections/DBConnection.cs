using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DBEngineProject.Engine;
using Utilities.Extensions;

namespace DBEngineProject.Connections
{
    #region Class: DBConnection
    
    /// <summary>
    /// Класс для подключения к базе
    /// </summary>
    public class DBConnection: IDisposable, ICloneable
    {

        #region Constructors: Public

        public DBConnection(string connectionString):this()
        {
            ConnectionString = connectionString;
            SqlConnection = new SqlConnection(ConnectionString);
        }

        public DBConnection(SqlConnection sqlConnection): this(sqlConnection.ConnectionString)
        {
            SqlConnection = sqlConnection;
        }

        #endregion

        #region Constructors: Protected

        protected DBConnection()
        {
            SqlTransactions = new Dictionary<string, SqlTransaction>();
        }

        protected DBConnection(DBConnection dbConnection): this()
        {
            ConnectionString = dbConnection.ConnectionString;
            SqlConnection = dbConnection.SqlConnection;
            OnConnectionClose = dbConnection.OnConnectionClose.Clone() as EventHandler<Object>;
            OnConnectionOpen = dbConnection.OnConnectionOpen.Clone() as EventHandler<Object>;
        }

        #endregion

        #region Properties: Public

        private string _connectionString;

        public virtual string ConnectionString {
            get {
                return _connectionString;
            }
            protected set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("connectionString");
                _connectionString = value.Trim();
            }
        }

        private SqlConnection _sqlConnection;

        public virtual SqlConnection SqlConnection
        {
            get
            {
                return _sqlConnection;
            }
            protected set
            {
                _sqlConnection = value ?? throw new ArgumentException("sqlConnection");
            }
        }

        private Dictionary<string, SqlTransaction> _sqlTransactions;

        public virtual Dictionary<string, SqlTransaction> SqlTransactions
        {
            get
            {
                return _sqlTransactions;
            }
            protected set
            {
                _sqlTransactions = value ?? throw new ArgumentException("sqlTransaction");
            }
        }

        #endregion

        #region Events: Public

        public event EventHandler<Object> OnConnectionOpen;

        public event EventHandler<Object> OnConnectionClose;

        public event EventHandler<Object> OnConnectionDisposed;

        public event EventHandler<Object> OnTransactionOpen;

        public event EventHandler<Object> OnTransactionDisposed;

        public event EventHandler<Object> OnTransactionClose;

        #endregion

        #region Methods: Public

        public virtual void OpenConnection()
        {
            SqlConnection.Open();
            OnConnectionOpen?.Invoke(this, null);
        }

        public virtual void CloseConnection()
        {
            if(SqlConnection.State != ConnectionState.Closed)
                SqlConnection.Close();
            OnConnectionClose?.Invoke(this, null);
        }

        public virtual string GetConnectionInfo()
        {
            StringBuilder infoBuilder = new StringBuilder();
            string br = Environment.NewLine;
            if(SqlConnection.State == ConnectionState.Open)
            {
                infoBuilder.AppendFormat("\tСтрока подключения: {0}{1}", SqlConnection.ConnectionString, br);
                infoBuilder.AppendFormat("\tБаза данных: {0}{1}", SqlConnection.Database, br);
                infoBuilder.AppendFormat("\tСервер: {0}{1}", SqlConnection.DataSource, br);
                infoBuilder.AppendFormat("\tВерсия сервера: {0}{1}", SqlConnection.ServerVersion, br);
                infoBuilder.AppendFormat("\tСостояние: {0}{1}", SqlConnection.State, br);
                infoBuilder.AppendFormat("\tWorkstationld: {0}", SqlConnection.WorkstationId);
            }
            return infoBuilder.ToString();
        }

        public virtual bool PingServer(string connectionString)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public virtual DBCommand CreateCommand(string command)
        {
            return CreateCommandInner(command);
        }

        public virtual DBCommand CreateCommand(string command, CommandType commandType)
        {
            return CreateCommandInner(command, commandType);
        }

        protected DBCommand CreateCommandInner(string command, CommandType commandType = CommandType.Text)
        {
            return new DBCommand(this, command, commandType);
        }

        public void Dispose()
        {
            CloseTransaction();
            OnTransactionDisposed?.Invoke(this, null);
            SqlConnection.Dispose();
            OnConnectionDisposed?.Invoke(this, null);
            
        }

        public virtual object Clone()
        {
            return new DBConnection(this);
        }

        public virtual void BeginTransaction(string transactionName)
        {
            BeginTransaction(transactionName, IsolationLevel.ReadCommitted);
        }

        public virtual void BeginTransaction(string transactionName, IsolationLevel isolationLevel)
        {
            BeginTransaction(transactionName, isolationLevel);
        }

        protected virtual void BeginTransaction(IsolationLevel isolationLevel, string transactionName)
        {
            SqlTransactions.Add(transactionName.ToUpper(), SqlConnection.BeginTransaction(isolationLevel, transactionName));
            OnTransactionOpen?.Invoke(this, transactionName);
        }

        public virtual void CloseTransaction(string transactionName)
        {
            string transactionNameUpper = transactionName.ToUpper();
            if (SqlTransactions.TryGetValue(transactionNameUpper, out SqlTransaction transactToDelete))
            {
                SqlTransactions.Remove(transactionNameUpper);
                transactToDelete.Dispose();
                OnTransactionClose?.Invoke(this, transactionName);
            }
        }

        public virtual void CloseTransaction()
        {
            SqlTransactions.Keys.ForEach(CloseTransaction);
        }

        #endregion

    }

    #endregion
}
