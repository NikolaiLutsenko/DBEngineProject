using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Query
{
    //TODO: Добавить регионы и комментарии.
    [DebuggerDisplay("SqlValueText = {GetSqlText()}")]
    public class FilterColumnValue : Filter
    {

        #region Constructors: Public

        public FilterColumnValue()
        {
            QueryComparisonType = QueryComparisonType.Equal;
        }

        public FilterColumnValue(string columnName, Object value)
            :this()
        {
            ColumnName = columnName;
            Value = value;
        }

        public FilterColumnValue(string columnName, Object value, QueryComparisonType comparisonType)
            :this(columnName, value)
        {
            QueryComparisonType = comparisonType;
        }

        #endregion

        #region Properties: Public

        private string _columnName;
        public virtual string ColumnName
        {
            get { return String.Format("[{0}]", _columnName); }
            set { _columnName = (value?.Trim() ?? throw new ArgumentNullException(nameof(ColumnName))); }
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set {
                value.CheckNull(nameof(Value));
                _value = value;
            }
        }

        public QueryComparisonType QueryComparisonType { get; set; }

        #endregion

        #region Methods: Protected

        protected virtual string GetSqlValueText()
        {
            string result = String.Empty;
            if(Value is Guid) {
                Value = Value.ToString();
            }
            Type valueType = Value.GetType();
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Byte:
                case TypeCode.Boolean:
                    result = Value.ToString();
                    break;
                case TypeCode.DateTime:
                    result = String.Format("'{0}'", ((DateTime)Value).ToString());
                    break;
                case TypeCode.String:
                case TypeCode.Char:
                    result = String.Format("'{0}'", Value.ToString());
                    break;
                default:
                    throw new ArgumentException("Value unknown TypeCode");
            }
            return result;
        }

        #endregion

        #region Methods: public

        public override string GetSqlText()
        {
            Value.CheckNull(nameof(Value));
            ColumnName.CheckNull(nameof(ColumnName));
            return String.Format("{0} {1} {2}",
                ColumnName,
                QueryComparisonType.GetSqlText(),
                GetSqlValueText());
        }

        #endregion

    }
}
