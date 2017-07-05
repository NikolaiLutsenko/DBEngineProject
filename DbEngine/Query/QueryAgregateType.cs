using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{

    #region

    /// <summary>
    /// Enum which contain agregate types.
    /// </summary>
    public enum QueryAgregateType: byte
    {
        And,
        Or
    }

    #endregion

    //TODO: Добавить регионы и написать комментарии
    public static class QueryAgregateTypeExtension
    {
        public static string GetSqlText(this QueryAgregateType target)
        {
            string result = String.Empty;
            switch (target)
            {
                case QueryAgregateType.And:
                    result = "AND"; break;
                case QueryAgregateType.Or:
                    result = "OR"; break;
                default:
                    throw new ArgumentException("Unknown " + nameof(QueryAgregateType));
            }
            return result;
        }
    }

}
