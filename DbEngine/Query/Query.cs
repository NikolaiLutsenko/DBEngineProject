using DBEngineProject.Query.SqlBuilders;
using DBEngineProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Query
{

    #region Class: Query

    /// <summary>
    /// Abstract class which permorms logic of sql query.
    /// </summary>
    [DebuggerDisplay("SqlValueText = {GetSqlText()}")]
    public abstract class Query: ISqlText
    {

        #region Fields: Protected

        private QueryType _queryType;

        #endregion

        #region Constructors: Public

        /// <summary>
        /// Sets data base table name.
        /// </summary>
        /// <param name="tableName">Data base table name</param>
        public Query(string tableName)
        {
            TableName = tableName;
        }

        #endregion

        #region Properties: Public

        public QueryType QueryType
        {
            get { return _queryType; }
            protected set { _queryType = value; }
        }

        private string _tableName;
        /// <summary>
        /// Data base table name.
        /// </summary>
        /// <exception cref="ArgumentNullException">When value is null.</exception>
        public virtual string TableName
        {
            get { return String.Format("[{0}]", _tableName); }
            set {
                value.CheckNull(nameof(TableName));
                _tableName = value.Trim();
            }
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Executs sql query.
        /// </summary>
        /// <returns>DB dataReader.</returns>
        public abstract IDataReader Execute();

        /// <summary>
        /// Returns sql query text.
        /// </summary>
        /// <returns>Sql query text.</returns>
        public abstract string GetSqlText();

        #endregion

    }

    #endregion

}
