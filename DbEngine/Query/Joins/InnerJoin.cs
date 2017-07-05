using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{

    #region Class: InnerJoin

    public class InnerJoin: Join
    {

        #region Constructors: Protected

        protected InnerJoin(Join join) 
            :base(join)
        {
        }

        #endregion

        #region Constructors: Public

        public InnerJoin(string tableName, string leftColumnName, string rightColumnName) 
            :base(tableName, leftColumnName, rightColumnName)
        {
            JoinType = JoinType.Inner;
        }

        public InnerJoin(SelectQuery innerSelect, string leftColumnName, string rightColumnName) 
            :base(innerSelect, leftColumnName, rightColumnName)
        {
        }

        public InnerJoin(string tableName, string leftColumnName, string rightColumnName, string rightTableName) :
            base(tableName, leftColumnName, rightColumnName, rightTableName)
        {
            JoinType = JoinType.Inner;
        }

        public InnerJoin(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName) : base(innerSelect, leftColumnName, rightColumnName, rightTableName)
        {
        }

        public InnerJoin(string leftColumnName, string rightColumnName) 
            :base(leftColumnName, rightColumnName)
        {
        }

        #endregion

        #region Methods: Public

        public override string GetSqlText()
        {
            return String.Format("INNER JOIN {0} ON {1} = {2}", 
                Source, 
                LeftColumnName,
                RightColumnName);
        }

        public override object Clone()
        {
            return new InnerJoin(this);
        }

        #endregion

    }

    #endregion

}
