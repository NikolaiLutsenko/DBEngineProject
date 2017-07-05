using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Utilities.Extensions;

namespace DBEngineProject.Exceptions
{

    #region Class: ColumnNotFindException

    /// <summary>
    /// The exception that is thrown when there is an attempt to derefence a null column reference.
    /// </summary>
    [DebuggerDisplay("{Message}")]
    public class ColumnNotFindException: NullReferenceException
    {

        #region Properties: Public

        /// <summary>
        /// Column name which not found. 
        /// </summary>
        public string ColumnName { get; protected set; }

        #endregion

        #region Constructors: Public

        /// <summary>
        /// Exception throw when dbEngine can`t be found column in row.
        /// </summary>
        /// <param name="columnName">Entity column name wich can`t be found.</param>
        public ColumnNotFindException(string columnName)
            :this(columnName, null as Exception) { }

        /// <summary>
        /// Exception throw when dbEngine can`t be found column in row.
        /// </summary>
        /// <param name="columnNames">Entity column name and aliases wich can`t be found.</param>
        public ColumnNotFindException(params string[] columnNames)
            :this(columnNames, null as Exception) { }

        /// <summary>
        /// Exception throw when dbEngine can`t be found column in row.
        /// </summary>
        /// <param name="columnNames">Entity column name and aliases wich can`t be found.</param>
        /// <param name="innerException">Inner exception.</param>
        public ColumnNotFindException(IEnumerable<string> columnNames, Exception innerException)
            : this(columnNames.JoinToString(), innerException) { }

        /// <summary>
        /// Exception throw when dbEngine can`t be found column in row.
        /// </summary>
        /// <param name="columnName">Entity column name wich can`t be found.</param>
        /// <param name="innerException">Inner exception.</param>
        public ColumnNotFindException(string columnName, Exception innerException)
            :base(String.Format("Column with name \"{0}\" not find.", columnName), innerException)
        {
            ColumnName = columnName;
        }

        #endregion

    }

    #endregion

}
