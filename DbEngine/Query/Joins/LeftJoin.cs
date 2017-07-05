using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{

    #region Class: LeftJoin

    public class LeftJoin :Join
    {

        #region Constructors: Protected

        protected LeftJoin(Join join)
            : base(join)
        {
        }

        #endregion

        #region Constructors: Public

        public LeftJoin(string tableName, string leftColumnName, string rightColumnName) 
            :base(tableName, leftColumnName, rightColumnName)
        {
            JoinType = JoinType.Left;
        }

        public LeftJoin(SelectQuery innerSelect, string leftColumnName, string rightColumnName) : base(innerSelect, leftColumnName, rightColumnName)
        {
        }

        public LeftJoin(string tableName, string leftColumnName, string rightColumnName, string rightTableName) 
            :base(tableName, leftColumnName, rightColumnName, rightTableName)
        {
            JoinType = JoinType.Left;
        }

        public LeftJoin(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName) : base(innerSelect, leftColumnName, rightColumnName, rightTableName)
        {
        }

        public LeftJoin(string leftColumnName, string rightColumnName) 
            :base(leftColumnName, rightColumnName)
        {
        }

        #endregion

        #region Methods: Public

        public override string GetSqlText()
        {
            return String.Format("LEFT JOIN {0} ON {1} = {2}", 
                Source, 
                LeftColumnName, 
                RightColumnName);
        }

        public override object Clone()
        {
            return new LeftJoin(this);
        }

        #endregion

    }

    #endregion

}
