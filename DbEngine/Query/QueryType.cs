using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{

    #region Enum: QueryType

    /// <summary>
    /// Enum with query types.
    /// </summary>
    public enum QueryType: byte
    {
        Select,
        Update,
        Delete,
        Insert
    }

    #endregion

    #region Class: QueryTypeExtension

    //TODO: добавить регионы и написать комментарии
    public static class QueryTypeExtension
    {
        public static string GetSqlText(this QueryType target)
        {
            string result = String.Empty;
            switch (target)
            {
                case QueryType.Select:
                    result = "SELECT"; break;
                case QueryType.Update:
                    result = "UPDATE"; break;
                case QueryType.Delete:
                    result = "DELETE"; break;
                case QueryType.Insert:
                    result = "INSERT"; break;
                default:
                    throw new ArgumentException("Unknown " + nameof(QueryType));
            }
            return result;
        }
    }

    #endregion

}
