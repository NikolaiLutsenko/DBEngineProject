using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{

    #region Class: RightJoin

    public class RightJoin : Join
    {

        #region Constructors: Protected

        protected RightJoin(Join join) 
            :base(join)
        {
        }

        #endregion

        #region Constructors: Public

        public RightJoin(string leftColumnName, string rightColumnName)
            : base(leftColumnName, rightColumnName)
        {
        }

        public RightJoin(string tableName, string leftColumnName, string rightColumnName) 
            : base(tableName, leftColumnName, rightColumnName)
        {
            JoinType = JoinType.Right;
        }

        public RightJoin(SelectQuery innerSelect, string leftColumnName, string rightColumnName) : base(innerSelect, leftColumnName, rightColumnName)
        {
        }

        public RightJoin(string tableName, string leftColumnName, string rightColumnName, string rightTableName) 
            :base(tableName, leftColumnName, rightColumnName, rightTableName)
        {
            JoinType = JoinType.Right;
        }

        public RightJoin(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName) : base(innerSelect, leftColumnName, rightColumnName, rightTableName)
        {
        }

        #endregion

        #region Methods: Public

        public override string GetSqlText()
        {
            return String.Format("RIGHT JOIN {0} ON {1} = {2}",
                Source,
                LeftColumnName,
                RightColumnName);
        }

        public override object Clone()
        {
            return new RightJoin(this);
        }

        #endregion

    }

    #endregion

}
