using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

//TODO: Разбить на регионы и написать комментарии ко всем методам и свойствам.
namespace DBEngineProject.Query
{
    [DebuggerDisplay("SqlValueText = {GetSqlText()}")]
    public class FilterGroup : Filter
    {

        #region Constructors: Public

        public FilterGroup()
        {
            QueryAgregateType = QueryAgregateType.And;
        }

        public FilterGroup(Filter leftExpression, Filter rightExpression)
            :this()
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        public FilterGroup(Filter leftExpression, Filter rightExpression, QueryAgregateType queryAgregateType)
            : this(leftExpression, rightExpression)
        {
            QueryAgregateType = queryAgregateType;
        }

        #endregion

        #region Methods: Public

        public virtual Filter LeftExpression { get; set; }

        public virtual Filter RightExpression { get; set; }

        public QueryAgregateType QueryAgregateType { get; set; }

        public override string GetSqlText()
        {
            LeftExpression.CheckNull(nameof(LeftExpression));
            RightExpression.CheckNull(nameof(RightExpression));
            string result = String.Format("({0}) {1} ({2})",
                LeftExpression.GetSqlText(),
                QueryAgregateType.GetSqlText(),
                RightExpression.GetSqlText());
            
            return result;
        }

        #endregion

    }
}
