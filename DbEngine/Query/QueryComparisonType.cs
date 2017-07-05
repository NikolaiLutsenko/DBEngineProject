using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{
    //TODO: Добавить регионы и комментарии
    public enum QueryComparisonType: byte
    {
        Equal,
        NotEqual,
        Lower,
        Bigger,
        LowerOrEqual,
        BiggerOrEqual
    }

    public static class QueryComparisonTypeExtension
    {
        public static string GetSqlText(this QueryComparisonType target)
        {
            string result = String.Empty;
            switch (target)
            {
                case QueryComparisonType.Equal:
                    result = "="; break;
                case QueryComparisonType.NotEqual:
                    result = "!="; break;
                case QueryComparisonType.Bigger:
                    result = ">"; break;
                case QueryComparisonType.Lower:
                    result = "<"; break;
                case QueryComparisonType.LowerOrEqual:
                    result = "<="; break;
                case QueryComparisonType.BiggerOrEqual:
                    result = ">="; break;
                default:
                    throw new ArgumentException("Unknown " + nameof(QueryComparisonType));
            }
            return result;
        }
    }
}
