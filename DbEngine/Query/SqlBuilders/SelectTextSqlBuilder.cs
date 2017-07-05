using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Query;
using Utilities.Extensions;

namespace DBEngineProject.Query.SqlBuilders
{

    #region Class: SelectTextSqlBuilder

    /// <summary>
    /// Class performs the logic of build select text sql query.
    /// </summary>
    public class SelectTextSqlBuilder : SqlTextBuilder
    {

        #region Properties: Protected

        /// <summary>
        /// table column names.
        /// </summary>
        protected List<String> ColumnNames { get; set; }

        /// <summary>
        /// Sql filter.
        /// </summary>
        protected Filter Filter { get; set; }

        protected JoinList ListJoin { get; set; }

        #endregion

        #region Constructors: Public

        /// <summary>
        /// Calls base constructor.
        /// </summary>
        public SelectTextSqlBuilder()
            :base()
        {
            ColumnNames = new List<string>();
        }

        public SelectTextSqlBuilder(SelectQuery select)
            :this()
        {
            SetQueryType(select.QueryType);
            SetTableName(select.TableName);
            SetFilter(select.GetFilter());
            SetJoins(select.JoinList);
            ColumnNames = select.ColumnNames;
        }

        #endregion

        #region Methods: Protected

        /// <summary>
        /// Returns filters text.
        /// </summary>
        /// <returns>Filter.</returns>
        protected virtual string GetFilterText()
        {
            return Filter.IsNull() ? String.Empty : String.Format("WHERE {0}", Filter.GetSqlText());
        }

        /// <summary>
        /// Returns column names text.
        /// </summary>
        /// <returns>Column names.</returns>
        /// <exception cref="ArgumentException">When count column names equal 0.</exception>
        protected virtual string GetColumnsText()
        {
            if (ColumnNames.IsNullOrEmpty())
                throw new ArgumentException("Column names must be define", nameof(ColumnNames));
            return ColumnNames.JoinToString();
        }

        #endregion

        #region Methods: Public

        public virtual SelectTextSqlBuilder SetFilter(Filter filter)
        {
            Filter = filter;
            return this;
        }

        public virtual SelectTextSqlBuilder SetJoins(JoinList joins)
        {
            ListJoin = joins;
            return this;
        }

        /// <summary>
        /// Returns select sql string.
        /// </summary>
        /// <returns></returns>
        public override string GetSqlText()
        {
            var result = strBuilder
                .Append(QueryType.GetSqlText())
                .Append(" ")
                .Append(GetColumnsText())
                .Append(" ")
                .Append(Environment.NewLine)
                .Append(GetTableText())
                .Append(Environment.NewLine)
                .Append(" ")
                .Append(ListJoin.GetSqlText())
                .Append(GetFilterText())
                .ToString();
            strBuilder.Clear();
            return result;
        }

        #endregion

    }

    #endregion

}
