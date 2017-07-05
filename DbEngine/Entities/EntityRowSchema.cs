using DBEngineProject.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Entities
{

    #region Class: EntityRowSchema

    public class EntityRowSchema: List<EntityColumnSchema>
    {

        #region Properties: Public

        public string TableName { get; protected set; }

        public List<EntityColumnSchema> ColumnSchemas => this as List<EntityColumnSchema>;

        #endregion

        #region Constructors: Public

        public EntityRowSchema(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Add(new EntityColumnSchema(reader, i));
        }

        public EntityRowSchema(string tableName, IEnumerable<EntityColumnSchema> columns)
        {
            TableName = tableName;
            AddRange(columns);
        }

        public EntityRowSchema(DBTableAttribute tableAttribute, IEnumerable<EntityColumnSchema> columns)
            :this(tableAttribute.Name, columns) { }

        #endregion

    }

    #endregion  

}
