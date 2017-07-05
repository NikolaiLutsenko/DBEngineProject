using DBEngineProject.Query.SqlBuilders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Query
{
    public class DeleteQuery : NonQuery
    {

        #region Properties

        /// <summary>
        /// Sql filter.
        /// </summary>
        protected Filter Filter { get; set; }

        private DeleteTextSqlBuilder _sqlTextBuilder;
        protected DeleteTextSqlBuilder SqlTextBuilder
        {
            get { return _sqlTextBuilder ?? (_sqlTextBuilder = new DeleteTextSqlBuilder()); }
            set
            {
                value.CheckNull(nameof(SqlTextBuilder));
                _sqlTextBuilder = value;
            }
        }

        #endregion

        #region Constructors: Public

        public DeleteQuery(string tableName) 
            :base(tableName)
        {
            SqlTextBuilder.SetTableName(tableName);
            QueryType = QueryType.Delete;
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Methods sets filter into query.
        /// </summary>
        /// <param name="filter">Filter or his child types.</param>
        /// <returns>Returns this DeleteQuery instance.</returns>
        public virtual DeleteQuery SetFilter(Filter filter)
        {
            Filter = filter;
            return this;
        }

        public override IDataReader Execute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method returns sql query as text.
        /// </summary>
        /// <returns>Sql text.</returns>
        public override string GetSqlText()
        {
            return (SqlTextBuilder
                .SetQueryType(QueryType)
                .SetTableName(TableName) as DeleteTextSqlBuilder)
                .SetFilter(Filter)
                .GetSqlText();
        }

        #endregion

    }
}
