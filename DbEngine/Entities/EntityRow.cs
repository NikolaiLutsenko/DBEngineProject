using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Exceptions;
using Utilities.Extensions;
using System.Data;

namespace DBEngineProject.Entities
{

    #region Class: EntityRow

    public class EntityRow: List<EntitnyColumn>
    {

        #region Properties: Public

        public EntityRowSchema RowSchema { get; protected set; }
        public int RecordsAffected { get; protected set; }

        #endregion

        #region Constructors: Public

        public EntityRow() { }

        public EntityRow(SqlDataReader reader, EntityRowSchema rowSchema)
        {
            RowSchema = rowSchema;
            for (int i = 0; i < reader.FieldCount; i++)
                Add(new EntitnyColumn(reader, i, rowSchema[i]));
        }

        #endregion

        #region Methods: Protected

        protected virtual EntitnyColumn TryGetColumnByName(string columnName)
        {
            return this.FirstOrDefault(x => x.Name.ToLower() == columnName.ToLower());
        }

        protected virtual EntitnyColumn GetColumnByName(string columnName)
        {
            columnName = columnName.ToLower().Trim();
            var column = this.FirstOrDefault(x => x.Name.ToLower() == columnName);
            return column ?? throw new ColumnNotFindException(columnName);
        }

        #endregion

        #region Methods: Public

        public virtual T Get<T>(string columnName)
        {
            return GetColumnByName(columnName).GetValue<T>();
        }

        public virtual void Get<T>(ref T prop, string columnName)
        {
            prop = Get<T>(columnName);
        }

        public virtual T Get<T>(string columnName, T defaultValue)
        {
            var column = TryGetColumnByName(columnName);
            return column != null ? column.GetValue<T>(defaultValue) : defaultValue;
        }

        public virtual T Get<T>(string columnName, Func<T> func)
        {
            return Get(columnName, func());
        }

        public virtual bool IsNull(string columnName)
        {
            return GetColumnByName(columnName).IsNull;
        }

        /// <summary>
        /// Method gets column value by his name or his aliases.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="columnNames">Column name and his aliases.</param>
        /// <returns>Typed column value.</returns>
        /// <exception cref="ColumnNotFindException">Throws if try get column by name is fail.</exception>
        public virtual T Get<T>(params string[] columnNames)
        {
            EntitnyColumn column = null;   
            int i = 0;
            do
            {
                column = i+1 > columnNames.Length? 
                    throw new ColumnNotFindException(columnNames):
                    TryGetColumnByName(columnNames[i++]);   
            } while (column.IsNull());
            return column.GetValue<T>();
        }

        #endregion

    }

    #endregion

}
