using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
using DBEngineProject.Query.SqlBuilders;

namespace DBEngineProject.Query
{
    public class InsertQuery : NonQuery
    {

        #region Properties: Protected

        private InsertTextSqlBuilder _sqlTextBuilder;
        protected InsertTextSqlBuilder SqlTextBuilder {
            get {
                return _sqlTextBuilder ?? (_sqlTextBuilder = new InsertTextSqlBuilder());
            }
            set {
                value.CheckNull(nameof(SqlTextBuilder));
                _sqlTextBuilder = value;
            } }

        private Dictionary<string, object> _columnValues;
        public Dictionary<string, object> ColumnValues {
            get {
                return _columnValues ?? (_columnValues = new Dictionary<string, object>());
            }
            protected set {
                value.CheckNull(nameof(ColumnValues));
                _columnValues = ColumnValues;
            } }

        #endregion

        #region Constructors: Public

        public InsertQuery(string tableName) 
            :base(tableName)
        {
            QueryType = QueryType.Insert;
            SqlTextBuilder.SetQueryType(QueryType.Insert);
            SqlTextBuilder.SetTableName(tableName);
        }

        #endregion

        #region Methods: Public

        public virtual InsertQuery AddColumnValue(string column, object value)
        {
            ColumnValues.Add(column, value);
            return this;
        }

        public virtual InsertQuery AddColumnValues(Dictionary<string,object> columnValues)
        {
            columnValues.ForEach(item => AddColumnValue(item.Key, item.Value));
            return this;
        }

        public virtual bool IsColumnExist(string columnName)
        {
            return !GetValueByColumn(columnName).IsNull();
        }

        public virtual Object GetValueByColumn(string columnName)
        {
            return this[columnName];
        }

        public virtual bool RemoveColumnValue(string columnName)
        {
            return ColumnValues.Remove(columnName);
        }

        public virtual object this[string index] {
            get { return ColumnValues[index]; }
            set { ColumnValues[index] = value; }
        }

        public override IDataReader Execute()
        {
            throw new NotImplementedException();
        }

        public override string GetSqlText()
        {
            SqlTextBuilder.SetColumnValues(ColumnValues);
            return SqlTextBuilder.GetSqlText();
        }

        #endregion

    }
}
