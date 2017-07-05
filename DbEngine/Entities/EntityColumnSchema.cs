using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Attributes;
using DBEngineProject.Interfaces;
using System.Reflection;
using DBEngineProject.Managers;
using System.Data.SqlClient;
using System.Data;

namespace DBEngineProject.Entities
{
    public class EntityColumnSchema
    {

        #region Properties: Public

        public virtual string Name { get; set; }

        public virtual string DataTypeName { get; protected set; }

        public bool IsAllowNull { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsIdentity { get; set; }

        public int IdentityStart { get; set; }

        public int IdentityStep { get; set; }

        #endregion

        #region Constructors: Public

        protected EntityColumnSchema()
        {
            IsPrimary = false;
            IsIdentity = false;
        }

        public EntityColumnSchema(PropertyInfo propInfo)
            :this(propInfo.Name, new SqlDataTypeManager().ConvertToSqlType(propInfo.PropertyType), true) { }

        public EntityColumnSchema(SqlDataReader reader, int index)
            :this()
        {
            Name = reader.GetName(index);
            DataTypeName = reader.GetDataTypeName(index);
        }

        public EntityColumnSchema(string name, string dataTypeName)
            :this(name, dataTypeName, true) { }

        public EntityColumnSchema(string name, string dataTypeName, bool isAllowNull)
            :this()
        {
            Name = name;
            DataTypeName = dataTypeName;
            IsAllowNull = isAllowNull;
        }

        #endregion

    }
}
