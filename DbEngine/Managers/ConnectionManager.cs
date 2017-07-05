using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Connections;
using System.Data.SqlClient;

namespace DBEngineProject.Managers
{

    #region Class: ConnectionManager

    /// <summary>
    /// Class uses for create DBConnection and his child
    /// </summary>
    public class ConnectionManager
    {

        #region Constructor: Protected

        protected ConnectionManager()
        {
            _dictionary = new Dictionary<string, Object>();
            singl = new object();
        }

        #endregion

        #region Fields: Protected

        protected Dictionary<string, Object> _dictionary;

        /// <summary>
        /// Fild for sinchronize
        /// </summary>
        protected Object singl;

        #endregion

        #region Fields: Protected (Static)

        private static ConnectionManager _instance;

        #endregion

        #region Methods: Public (Static)

        /// <summary>
        /// Methods creates or gets manager instance.
        /// </summary>
        /// <returns>ConnectionManager instance.</returns>
        public static ConnectionManager GetInstance()
        {
            return _instance ?? (_instance = new ConnectionManager());
        }

        /// <summary>
        /// Method uses for create DBConnection and his children.
        /// </summary>
        /// <typeparam name="T">DBConnection or his children.</typeparam>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>DBConnection instance.</returns>
        public T GetConnection<T>(string connectionString) where T : DBConnection
        {
            lock (singl)
            {
                Type type = typeof(T);
                if (!_dictionary.TryGetValue(type.FullName, out object result))
                {
                    result = Activator.CreateInstance(type, connectionString);
                    _dictionary.Add(type.FullName, result);
                }
                return result as T;
            }
        }

        /// <summary>
        /// Method uses for create DBConnection and his children.
        /// </summary>
        /// <typeparam name="T">DBConnection or his children.</typeparam>
        /// <param name="sqlConnection">Sql instance.</param>
        /// <returns>DBConnection instance.</returns>
        public T GetConnection<T>(SqlConnection sqlConnection) where T : DBConnection
        {
            return GetConnection<T>(sqlConnection.ConnectionString);
        }

        #endregion

    }

    #endregion

}
