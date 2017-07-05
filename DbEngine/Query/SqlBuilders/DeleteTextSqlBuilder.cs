using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Query.SqlBuilders
{
    public class DeleteTextSqlBuilder : SqlTextBuilder
    {

        #region Properties: Protected

        /// <summary>
        /// Sql filter.
        /// </summary>
        protected Filter Filter { get; set; }

        #endregion

        #region Constructors: Public

        public DeleteTextSqlBuilder()
            :base() { }

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

        #endregion

        #region Methods: Public

        public virtual DeleteTextSqlBuilder SetFilter(Filter filter)
        {
            Filter = filter;
            return this;
        }

        public override string GetSqlText()
        {
            string sql = strBuilder
                .Append(QueryType.GetSqlText())
                .Append(" ")
                .Append(GetTableText())
                .Append(" ")
                .Append(GetFilterText())
                .Append(";")
                .ToString();
            strBuilder.Clear();
            return sql;
        }

        #endregion

    }
}
