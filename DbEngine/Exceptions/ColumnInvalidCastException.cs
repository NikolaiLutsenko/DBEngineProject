using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DBEngineProject.Exceptions
{

    [DebuggerDisplay("{Message}")]
    public class ColumnInvalidCastException: InvalidCastException
    {

        #region Constructors: Public

        public ColumnInvalidCastException(string columnName, string fromCastType, string toCastType)
            :this(columnName, fromCastType, toCastType, null)
        {
            
        }

        public ColumnInvalidCastException(string columnName, string fromCastType, string toCastType, Exception exception)
            :base(GetErrorMessage(columnName, fromCastType, toCastType), exception)
        {
            ColumnName = columnName;
            FromCastType = fromCastType;
            ToCastType = toCastType;
        }

        public ColumnInvalidCastException(string columnName, string fromCastType, Type toCastType)
            :this(columnName, fromCastType, toCastType.FullName)
        {

        }

        public ColumnInvalidCastException(string columnName, string fromCastType, Type toCastType, Exception exception)
            : this(columnName, fromCastType, toCastType.FullName, exception)
        {

        }

        #endregion

        #region Properties: Public

        public string ColumnName { get; protected set; }

        public string FromCastType { get; protected set; }

        public string ToCastType { get; set; }

        #endregion

        #region Methods: Protected

        protected static string GetErrorMessage(string columnName, string fromCastType, string toCastType)
        {
            return String.Format("Column '{0}' can`t convert from '{1}' type to '{2}' type.", columnName, fromCastType, toCastType);
        }

        #endregion

    }
}
