using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Query;
using Utilities.Extensions;
using DBEngineProject.Interfaces;
using DBEngineProject.Managers;

namespace DBEngineProject.Query.SqlBuilders
{

    #region Class: SqlTextBuilder
    //TODO: Написать комментарии
    public abstract class SqlTextBuilder : ISqlText
    {

        #region Fields: Protected

        /// <summary>
        /// String builder wich contains sql text;
        /// </summary>
        protected StringBuilder strBuilder;

        #endregion

        #region Properties: Protected

        /// <summary>
        /// Sql query type (SQLECT, UPDATE, DELETE, INSERT).
        /// </summary>
        protected QueryType QueryType { get; set; }

        /// <summary>
        /// Data base table name.
        /// </summary>
        protected string TableName { get; set; }

        #endregion

        #region Constructors: Public

        public SqlTextBuilder()
        {
            strBuilder = new StringBuilder();
        }

        #endregion

        #region Methods: Protected

        /// <summary>
        /// Returns table name and his sufix.
        /// </summary>
        /// <returns>Table name.</returns>
        /// <exception cref="ArgumentNullException">When unknown QueryType.</exception>
        protected virtual string GetTableText()
        {
            string result = TableName;
            switch (QueryType)
            {
                case QueryType.Select:
                case QueryType.Delete:
                    result = "FROM " + TableName; break;
                case QueryType.Update:
                    result += " SET"; break;
                case QueryType.Insert:
                    result = "INTO " + TableName; break;
                default:
                    throw new ArgumentException("Unknown " + nameof(QueryType));
            }
            return result;
        }

        protected virtual string GetStringValue(object value)
        {
            return SqlDataTypeManager.Convert(value);
        }

        #endregion

        #region Methods: Public

        public virtual SqlTextBuilder SetQueryType(QueryType queryType)
        {
            QueryType = queryType;
            return this;
        }

        public virtual SqlTextBuilder SetTableName(string tableName)
        {
            tableName.CheckNull(nameof(tableName));
            TableName = tableName;
            return this;
        }

        public abstract string GetSqlText();

        #endregion

    }

    #endregion

}
