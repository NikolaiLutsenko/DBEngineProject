using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Query;
using Utilities.Extensions;

namespace DBEngineProject.Query
{

    #region Class: QueryWithFilter

    /// <summary>
    /// Query with filter (SELECT, UPDATE, DELETE).
    /// </summary>
    public abstract class QueryWithFilter: Query
    {

        #region Constructors: Public

        public QueryWithFilter(string tableName)
            :base(tableName){ }

        #endregion

        #region Fields: Private

        /// <summary>
        /// Filter for data base.
        /// </summary>
        private Filter _filter;

        #endregion

        #region Methods: Public

        /// <summary>
        /// Method sets filter into query.
        /// </summary>
        /// <param name="filter">Filter child.</param>
        /// <exception cref="ArgumentNullException">If filter is null.</exception>
        public virtual QueryWithFilter SetFilter(Filter filter)
        {
            filter.CheckNull(nameof(filter));
            _filter = filter;
            return this;
        }

        /// <summary>
        /// Method gets filter.
        /// </summary>
        /// <returns>Filter child.</returns>
        public Filter GetFilter()
        {
            return _filter;
        }

        #endregion

    }

    #endregion

}
