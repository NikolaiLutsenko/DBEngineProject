using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Utilities.Extensions;
using Utilities.Diagnostics;
using DBEngineProject.Exceptions;
using DBEngineProject.Managers;
using DBEngineProject.Engine;
using DBEngineProject.Sections;
using DBEngineProject.Interfaces;
using System.Diagnostics;

namespace DBEngineProject.Entities
{
    public class EntityDB
    {

        #region Properties: Public

        /// <summary>
        /// DB connection string.
        /// </summary>
        public string ConnectionString { get; protected set; }

        /// <summary>
        /// Engine for work with db.
        /// </summary>
        public DBEngine DBEngine { get; protected set; }

        #endregion

        #region Properties: Static

        protected static EntityDB _instance = default(EntityDB);

        protected static object singl = new object();

        #endregion

        #region Constructors: Protected

        protected EntityDB(string connectionString)
        {
            ConnectionString = connectionString;
            DBEngine = new DBEngine(ConnectionString);
        }

        #endregion

        #region Methods: Static

        public static EntityDB GetInstance(string connectionString)
        {
            lock (singl)
            {
                if (_instance.IsNull())
                {
                    _instance = new EntityDB(connectionString);
                }
                return _instance;
            }
        }

        #endregion

    }

}
