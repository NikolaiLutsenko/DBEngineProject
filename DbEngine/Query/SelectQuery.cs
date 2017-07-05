using DBEngineProject.Query.SqlBuilders;
using DBEngineProject.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Query
{

    #region Class: SelectQuery

    /// <summary>
    /// Class performs the logic of create select sql query.
    /// </summary>
    public class SelectQuery : QueryWithFilter
    {

        #region Properties: Protected

        /// <summary>
        /// table column names.
        /// </summary>
        private List<String> _columnNames { get; set; }
        public List<String> ColumnNames
        {
            get
            {
                return _columnNames ?? (_columnNames = new List<string>());
            }
            protected set
            {
                value.CheckNull(nameof(ColumnNames));
                _columnNames = value;
            }
        }

        private string _alias;
        public string Alias
        {
            get {
                return _alias;
            }
            set {
                value.CheckNull(nameof(Alias));
                _alias = value.Trim().ToUpper();
            }
        }

        private SelectTextSqlBuilder _sqlTextBuilder;
        protected SelectTextSqlBuilder SqlTextBuilder {
            get { return _sqlTextBuilder; }
            set {
                value.CheckNull(nameof(SqlTextBuilder));
                _sqlTextBuilder = value;
            }
        }

        private JoinList _joinList;
        public JoinList JoinList
        {
            get { return _joinList ?? (_joinList = new JoinList()); }
            protected set
            {
                value.CheckNull(nameof(JoinList));
                _joinList = value;
            }
        }

        #endregion

        #region Constructors: Public

        public SelectQuery(string tableName)
            :base(tableName)
        {
            QueryType = QueryType.Select;
            SqlTextBuilder = new SelectTextSqlBuilder();
        }

        #endregion

        #region Methods: Private

        private void AddColumnName(string columnName)
        {
            var name = columnName.Trim().ToUpper();
            if (!ColumnNames.Contains(name))
            {
                ColumnNames.Add(name);
            }
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual SelectQuery As(string alias)
        {
            Alias = alias;
            return this;
        }

        /// <summary>
        /// Method adds column into query.
        /// </summary>
        /// <param name="columnName">Column name.</param>
        /// <returns>Returns this selectQuery instance.</returns>
        public SelectQuery AddColumn(string columnName)
        {
            columnName.CheckNull(nameof(columnName));
            columnName = String.Format("{0}.[{1}]", TableName, columnName);
            AddColumnName(columnName);
            return this;
        }

        /// <summary>
        /// Method adds column into query.
        /// </summary>
        /// <param name="columnName">Column name.</param>
        /// <returns>Returns this selectQuery instance.</returns>
        public SelectQuery AddColumn(string columnName, string alias)
        {
            columnName = String.Format("{0}.[{1}] AS [{2}]", TableName, columnName, alias);
            AddColumnName(columnName);
            return this;
        }

        /// <summary>
        /// Methods adds all columns into query.
        /// </summary>
        /// <returns>Returns this selectQuery instance.</returns>
        public SelectQuery AddAllColumns()
        {
            ColumnNames.Clear();
            ColumnNames.Add("*");
            return this;
        }

        public SelectQuery AddAllColumns(string tableName)
        {
            tableName.CheckNull(nameof(tableName));
            tableName = String.Format("{0}.*", tableName);
            if (!ColumnNames.Contains(tableName))
            {
                ColumnNames.Add(tableName);
            }
            return this;
        }

        /// <summary>
        /// Methods sets filter into query.
        /// </summary>
        /// <param name="filter">Filter or his child types.</param>
        /// <returns>Returns this selectQuery instance.</returns>
        public new SelectQuery SetFilter(Filter filter)
        {
            base.SetFilter(filter);
            return this;
        }

        public override IDataReader Execute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method returns sql query as text.
        /// </summary>
        /// <returns>Sql text.</returns>
        public override string GetSqlText()
        {
            return new SelectTextSqlBuilder(this)
                .GetSqlText();
        }

        public virtual SelectQuery AddJoin(Join join)
        {
            join.SetSelectQuery(this);
            JoinList.Add(join);
            return this;
        }

        public virtual SelectQuery AddJoins(IEnumerable<Join> joins)
        {
            joins.ForEach(item => AddJoin(item));
            return this;
        }

        public virtual bool ClearJoin(Join join)
        {
            return JoinList.Remove(join);
        }

        public virtual void ClearJoins()
        {
            JoinList.Clear();
        }

        #endregion

    }

    #endregion

}
