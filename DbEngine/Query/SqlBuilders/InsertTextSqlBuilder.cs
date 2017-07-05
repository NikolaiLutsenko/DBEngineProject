using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Query;
using Utilities.Extensions;

namespace DBEngineProject.Query.SqlBuilders
{

    #region Class: InsertTextSqlBuilder

    public class InsertTextSqlBuilder : SqlTextBuilder
    {

        #region Fields: Public

        /// <summary>
        /// Columns whith value.
        /// </summary>
        public Dictionary<string, Object> ColumnValues { get; protected set; }

        #endregion

        #region Constructors: Public

        public InsertTextSqlBuilder()
            : base()
        {
            ColumnValues = new Dictionary<string, object>();
        }

        #endregion

        #region Methods: Protected

        protected virtual string GetSqlColumnValues()
        {
            if (ColumnValues.IsNull() || ColumnValues.Count < 1)
            {
                throw new ArgumentException("ColumnValues must has more then 0 elements.", nameof(ColumnValues));
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("({0}) VALUES ({1})",
                    ColumnValues.JoinToString(x => x.Key),
                    ColumnValues.JoinToString(x => GetStringValue(x.Value)));
            return sb.ToString();
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Method sets columnValues. 
        /// </summary>
        /// <param name="columnValues">Dictionary with columnName and columnValue.</param>
        /// <returns>Returns this instance.</returns>
        public virtual InsertTextSqlBuilder SetColumnValues(Dictionary<string, Object> columnValues)
        {
            ColumnValues = columnValues;
            return this;
        }

        /// <summary>
        /// Returns sql query as string.
        /// </summary>
        /// <returns>Sql query.</returns>
        public override string GetSqlText()
        {
            var result = strBuilder
                .Append(QueryType.GetSqlText())
                .Append(" ")
                .Append(GetTableText())
                .Append(" ")
                .Append(GetSqlColumnValues())
                .Append(";")
                .ToString();
            strBuilder.Clear();
            return result;
        }

        #endregion

    }

    #endregion

}
