using System;
using System.Collections.Generic;
using System.Diagnostics;
using DBEngineProject.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
namespace DBEngineProject.Query
{

    #region Class: Join

    /// <summary>
    /// Abstract class performs the logic of construct join sql text.
    /// </summary>
    [DebuggerDisplay("{GetSqlText()}")]
    public abstract class Join : ISqlText, ICloneable
    {

        #region Properties: Public

        public SelectQuery SelectQuery { get; private set; }

        public JoinType JoinType { get; protected set; }

        private string _tableName;
        public string TableName
        {
            get {
                return String.Format("[{0}]", _tableName);
            }
            set
            {
                value.CheckNull(nameof(TableName));
                _tableName = value;
            }
        }

        public virtual string Source
        {
            get {
                return InnerSelect.IsNull() ?
                   String.Format("[{0}]", _tableName) :
                   String.Format("({0})", InnerSelect.GetSqlText());
            }
        }

        private SelectQuery _innerSelect;
        public SelectQuery InnerSelect
        {
            get { return _innerSelect; }
            protected set
            {
                value.CheckNull(nameof(InnerSelect));
                _innerSelect = value;
            }
        }

        private string _rightTableName;
        public string RightTableName
        {
            get { return _rightTableName.IsNull() ? SelectQuery.TableName : String.Format("[{0}]", _rightTableName); }
            set
            {
                _rightTableName = value;
            }
        }

        private string _rightColumnName;
        public string RightColumnName
        {
            get { return String.Format("{0}.[{1}]", RightTableName, _rightColumnName); }
            set
            {
                value.CheckNull(nameof(RightColumnName));
                _rightColumnName = value;
            }
        }

        private string _leftColumnName;
        public string LeftColumnName
        {
            get {
                return InnerSelect.IsNull() ? 
                    String.Format("{0}.[{1}]", TableName, _leftColumnName) : 
                    String.Format("[{0}]", _leftColumnName);
            }
            set
            {
                value.CheckNull(nameof(LeftColumnName));
                _leftColumnName = value;
            }
        }

        #endregion

        #region Constructors: Protected

        protected Join(Join join)
            :this(join.TableName, join.LeftColumnName, join.RightColumnName, join.RightColumnName)
        {
            InnerSelect = join.InnerSelect;
            JoinType = join.JoinType;
        }

        #endregion

        #region Constructors: Public

        public Join(string leftColumnName, string rightColumnName)
        {
            RightColumnName = rightColumnName;
            LeftColumnName = leftColumnName;
        }

        public Join(string tableName, string leftColumnName, string rightColumnName)
            :this(leftColumnName, rightColumnName)
        {
            TableName = tableName;
        }


        public Join(string tableName, string leftColumnName, string rightColumnName, string rightTableName)
            :this(tableName, leftColumnName, rightColumnName)
        {
            RightTableName = rightTableName;
        }

        public Join(SelectQuery innerSelect, string leftColumnName, string rightColumnName)
            :this(leftColumnName, rightColumnName)
        {
            InnerSelect = innerSelect;
        }

        public Join(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName)
            : this(innerSelect, leftColumnName, rightColumnName)
        {
            RightTableName = rightTableName;
        }


        #endregion

        #region Methods: Public

        public abstract string GetSqlText();

        public void SetSelectQuery(SelectQuery query)
        {
            SelectQuery = query;
        }

        public abstract object Clone();

        #endregion

        #region Methods: Static

        private static Join Create(JoinType type, string leftColumn, string rightColumn, string rightTableName = null)
        {
            Join result = null;
            switch (type)
            {
                case JoinType.Inner:
                    {
                        result = new InnerJoin(leftColumn, rightColumn);
                        break;
                    }
                case JoinType.Left:
                    {
                        result = new LeftJoin(leftColumn, rightColumn);
                        break;
                    }
                case JoinType.Right:
                    {
                        result = new RightJoin(leftColumn, rightColumn);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException(nameof(type));
                    }
            }
            result.RightTableName = rightTableName;
            return result;
        }

        public static Join Create(JoinType type, string table, string leftColumn, string rightColumn, string rightTableName = null)
        {
            Join result = Create(type, leftColumn, rightColumn, rightTableName);
            result.TableName = table;
            return result;
        }

        public static Join Create(JoinType type, SelectQuery innerSelect, string leftColumn, string rightColumn, string rightTableName = null)
        {
            Join result = Create(type, leftColumn, rightColumn, rightTableName);
            result.InnerSelect = innerSelect;
            return result;
        }

        public static InnerJoin Inner(string tableName, string leftColumnName, string rightColumnName, string rightTableName)
        {
            return new InnerJoin(tableName, leftColumnName, rightColumnName, rightTableName);
        }

        public static InnerJoin Inner(string tableName, string leftColumnName, string rightColumnName)
        {
            return new InnerJoin(tableName, leftColumnName, rightColumnName);
        }

        public static InnerJoin Inner(SelectQuery innerSelect, string leftColumnName, string rightColumnName)
        {
            return new InnerJoin(innerSelect, leftColumnName, rightColumnName);
        }

        public static InnerJoin Inner(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName)
        {
            return new InnerJoin(innerSelect, leftColumnName, rightColumnName, rightTableName);
        }

        public static InnerJoin Inner(string leftColumnName, string rightColumnName)
        {
            return new InnerJoin(leftColumnName, rightColumnName);
        }

        public static LeftJoin Left(string tableName, string leftColumnName, string rightColumnName, string rightTableName)
        {
            return new LeftJoin(tableName, leftColumnName, rightColumnName, rightTableName);
        }

        public static LeftJoin Left(string tableName, string leftColumnName, string rightColumnName)
        {
            return new LeftJoin(tableName, leftColumnName, rightColumnName);
        }

        public static LeftJoin Left(SelectQuery innerSelect, string leftColumnName, string rightColumnName)
        {
            return new LeftJoin(innerSelect, leftColumnName, rightColumnName);
        }

        public static LeftJoin Left(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName)
        {
            return new LeftJoin(innerSelect, leftColumnName, rightColumnName, rightTableName);
        }

        public static LeftJoin Left(string leftColumnName, string rightColumnName)
        {
            return new LeftJoin(leftColumnName, rightColumnName);
        }

        public static RightJoin Right(string tableName, string leftColumnName, string rightColumnName, string rightTableName)
        {
            return new RightJoin(tableName, leftColumnName, rightColumnName, rightTableName);
        }

        public static RightJoin Right(string tableName, string leftColumnName, string rightColumnName)
        {
            return new RightJoin(tableName, leftColumnName, rightColumnName);
        }

        public static RightJoin Right(SelectQuery innerSelect, string leftColumnName, string rightColumnName)
        {
            return new RightJoin(innerSelect, leftColumnName, rightColumnName);
        }

        public static RightJoin Right(SelectQuery innerSelect, string leftColumnName, string rightColumnName, string rightTableName)
        {
            return new RightJoin(innerSelect, leftColumnName, rightColumnName, rightTableName);
        }

        public static RightJoin Right(string leftColumnName, string rightColumnName)
        {
            return new RightJoin(leftColumnName, rightColumnName);
        }

        #endregion

    }

    #endregion

}
