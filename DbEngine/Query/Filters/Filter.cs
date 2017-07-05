using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Interfaces;
using System.Diagnostics;

namespace DBEngineProject.Query
{

    #region Class: Filter

    /// <summary>
    /// Class performs logic of sql filters.
    /// </summary>
    [DebuggerDisplay("{GetSqlText()}")]
    public abstract class Filter: ISqlText
    {

        #region Methods: Public

        /// <summary>
        /// Returns sql text filter.
        /// </summary>
        /// <returns>Sql text.</returns>
        public abstract string GetSqlText();

        #endregion

        #region Methods: Static

        public static FilterColumnValue ColumnValue(string columnName, Object value, QueryComparisonType comparisonType)
        {
            return new FilterColumnValue(columnName, value, comparisonType);
        }

        public static FilterColumnValue ColumnValue(string columnName, Object value)
        {
            return new FilterColumnValue(columnName, value);
        }

        public static FilterColumnValue ColumnValue()
        {
            return new FilterColumnValue();
        }

        public static FilterGroup Group(Filter leftExpression, Filter rightExpression, QueryAgregateType queryAgregateType)
        {
            return new FilterGroup(leftExpression, rightExpression, queryAgregateType);
        }

        public static FilterGroup Group(Filter leftExpression, Filter rightExpression)
        {
            return new FilterGroup(leftExpression, rightExpression);
        }

        public static FilterGroup Group()
        {
            return new FilterGroup();
        }

        public static FilterIn In(string columnName, SelectQuery selectQuery)
        {
            return new FilterIn(columnName, selectQuery);
        }

        public static FilterIn In(string columnName, params object[] conditions)
        {
            return new FilterIn(columnName, conditions);
        }

        public static FilterIn In(string columnName, IEnumerable<object> conditions)
        {
            return new FilterIn(columnName, conditions);
        }

        #endregion

    }

    #endregion

}
