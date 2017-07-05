using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Managers;
using Utilities.Extensions;

namespace DBEngineProject.Query
{

    #region Class: FilterIn

    public class FilterIn : Filter
    {

        #region Properties: Public

        private string _columnName;
        public virtual string ColumnName
        {
            get { return String.Format("[{0}]", _columnName); }
            protected set
            {
                value.CheckNull(nameof(ColumnName));
                _columnName = value;
            }
        }

        private IEnumerable<object> _conditions;
        public virtual IEnumerable<object> Conditions
        {
            get { return _conditions; }
            protected set
            {
                value.CheckNull(nameof(Conditions));
                _conditions = value;
            }
        }

        public bool IsNotIn { get; set; }

        private SelectQuery _selectQuery;
        public SelectQuery SelectQuery
        {
            get { return _selectQuery; }
            protected set
            {
                value.CheckNull(nameof(SelectQuery));
                _selectQuery = value;
            }
        }

        #endregion

        #region Constructors: Protected

        protected FilterIn(bool isNotIn)
        {
            IsNotIn = isNotIn;
        }

        #endregion

        #region Constructors: Public

        public FilterIn(string columnName, bool isNotIn, IEnumerable<object> conditions)
            :this(isNotIn)
        {
            ColumnName = columnName;
            Conditions = conditions;
        }

        public FilterIn(string columnName, IEnumerable<object> conditions)
            :this(columnName, false, conditions) { }

        public FilterIn(string columnName, bool isNotIn, params object[] conditions)
            :this(columnName, isNotIn, conditions.ToList()) { }

        public FilterIn(string columnName, params object[] conditions)
            :this(columnName, false, conditions.ToList()) { }

        public FilterIn(string columnName, SelectQuery selectQuery)
        {
            ColumnName = columnName;
            SelectQuery = selectQuery;
        }

        #endregion

        #region Methods: Protected

        protected virtual string GetIsNotIn()
        {
            return IsNotIn ? "NOT IN" : "IN";
        }

        protected virtual string GetSelectString()
        {
            int count = SelectQuery.ColumnNames.Count;
            if (count != 1)
            {
                throw new ArgumentException("Must contain only one column", "Select query");
            }
            return SelectQuery.GetSqlText();
        }

        protected virtual string GetSource()
        {
            return SelectQuery.IsNull() ? GetConditionsString() : GetSelectString();
        }

        protected virtual string GetConditionsString()
        {
            return Conditions.JoinToString(SqlDataTypeManager.Convert);
        }

        #endregion

        #region Methods: Public

        public virtual FilterIn Not()
        {
            IsNotIn = !IsNotIn;
            return this;
        }

        public override string GetSqlText()
        {
            return new StringBuilder()
                 .Append(ColumnName)
                 .Append(" ")
                 .Append(GetIsNotIn())
                 .Append(" (")
                 .Append(GetSource())
                 .Append(")")
                 .ToString();
        }

        #endregion

    }

    #endregion

}
