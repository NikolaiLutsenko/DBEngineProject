using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Exceptions;

namespace DBEngineProject.Entities
{

    #region Class: EntitnyColumn

    /// <summary>
    /// Class uses OOP for display data base column
    /// </summary>
    [DebuggerDisplay("Name = {Name}, Value = {Value}, IsNull = {IsNull}, DataTypeName = {DataTypeName}")]
    public class EntitnyColumn
    {

        #region Constructors: Public

        public EntitnyColumn(IDataReader reader, int index)
        {
            ColumnSchema = new EntityColumnSchema(reader.GetName(index), reader.GetDataTypeName(index));
            Value = reader[Name];
            IsNull = reader.IsDBNull(index);
        }

        public EntitnyColumn(IDataReader reader, int index, EntityColumnSchema schema)
        {
            ColumnSchema = schema;
            Value = reader[Name];
            IsNull = reader.IsDBNull(index);
        }

        #endregion

        #region Properties: Public

        public virtual string Name { get { return ColumnSchema.Name; } }

        public virtual bool IsNull { get; protected set; }

        public virtual string DataTypeName { get { return ColumnSchema.DataTypeName; } }

        public virtual EntityColumnSchema ColumnSchema { get; protected set; }

        public Object Value { get; protected set; }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Method gets value of column.
        /// </summary>
        /// <typeparam name="T">Column value type.</typeparam>
        /// <exception cref="ColumnInvalidCastException"></exception>
        /// <returns></returns>
        public virtual T GetValue<T>()
        {
            try
            {
                return (T)Convert.ChangeType(Value, typeof(T));
            }catch(InvalidCastException ex)
            {
                throw new ColumnInvalidCastException(Name, DataTypeName, typeof(T), ex);
            }
        }

        /// <summary>
        /// Method gets value of column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public virtual T GetValue<T>(T defaultValue)
        {
            return !IsNull ? GetValue<T>() : defaultValue;
        }

        #endregion

    }

    #endregion

}
