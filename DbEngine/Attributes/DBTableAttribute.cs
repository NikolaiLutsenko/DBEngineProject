using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Attributes
{
    /// <summary>
    /// Attribute for marking class as db table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DBTableAttribute : Attribute
    {

        #region Properties: Public

        /// <summary>
        /// Data base table name.
        /// </summary>
        public string Name { get; protected set; }

        #endregion

        #region Constructors: Public

        /// <summary>
        /// Initialize a new instance of DBTableAttribute.
        /// </summary>
        /// <param name="tableName">DB table name.</param>
        public DBTableAttribute(string tableName)
        {
            Name = tableName;
        }

        #endregion

    }
}
