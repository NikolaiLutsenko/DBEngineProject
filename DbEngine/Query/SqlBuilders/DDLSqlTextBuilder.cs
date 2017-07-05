using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Entities;
using DBEngineProject.Interfaces;
using Utilities.Extensions;

namespace DBEngineProject.Query.SqlBuilders
{

    #region Class: DDLSqlTextBuilder

    /// <summary>
    /// Class which build DDL text for execute on db.
    /// </summary>
    public class DDLSqlTextBuilder : IDDLSqlTextBuilder
    {

        protected string TableName { get; set; }

        public IEnumerable<EntityColumnSchema> ColumnSchemas { get; set; }

        #region Methods: Private

        private string GetIdentity(EntityColumnSchema column)
        {
            return column.IsIdentity ?
                String.Format("IDENTITY ({0}, {1})", column.IdentityStart, column.IdentityStep) :
                String.Empty;
        }

        private static string GetPrimaryConstraint(EntityColumnSchema column, string columnText)
        {
            if (column.IsPrimary)
                columnText = String.Format("{0}, PRIMARY KEY CLUSTERED ([{1}] ASC)", columnText, column.Name);
            return columnText;
        }

        #endregion

        #region Methods: Protected

        protected virtual void Init(EntityRowSchema schema)
        {
            TableName = schema.TableName;
            ColumnSchemas = schema;
        }

        protected virtual string CreateHeadTable()
        {
            return String.Format("CREATE TABLE [dbo].[{0}](", TableName);
        }

        protected virtual string CreateColumns()
        {
            List<string> columns = new List<string>();
            ColumnSchemas.ForEach(column => columns.Add(CreateColumn(column)));
            return columns.JoinToString();
        }

        protected virtual string CreateColumn(EntityColumnSchema column)
        {
            string columnText = String.Format("[{0}] {1} {2} {3}",
                column.Name,
                column.DataTypeName,
                GetIdentity(column),
                column.IsAllowNull ? "NULL" : "NOT NULL");
            columnText = GetPrimaryConstraint(column, columnText);
            return columnText;
        }

        protected virtual string CreateFoodTable()
        {
            return String.Format(");");
        }

        #endregion

        #region Methods: Public

        public virtual string GetSqlTextBySchema(EntityRowSchema schema)
        {
            Init(schema);
            return new StringBuilder()
                .Append(CreateHeadTable())
                .Append(CreateColumns())
                .Append(CreateFoodTable())
                .ToString();
        }

        #endregion

    }

    #endregion

}
