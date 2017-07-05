using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Utilities.Extensions;
using Utilities.Diagnostics;
using DBEngineProject.Exceptions;
using DBEngineProject.Attributes;
using DBEngineProject.Managers;
using DBEngineProject.Engine;
using DBEngineProject.Sections;
using DBEngineProject.Interfaces;
using System.Diagnostics;

namespace DBEngineProject.Entities
{
    public class EntityDB
    {

        #region Properties: Public

        /// <summary>
        /// DB connection string.
        /// </summary>
        public string ConnectionString { get; protected set; }

        /// <summary>
        /// Engine for work with db.
        /// </summary>
        public DBEngine DBEngine { get; protected set; }

        #endregion

        #region Properties: Static

        protected static EntityDB _instance = default(EntityDB);

        protected static object singl = new object();

        #endregion

        #region Events: Public

        /// <summary>
        /// Event called when table success created.
        /// </summary>
        public event EventHandler<EntityRowSchemaEventArgs> OnTableCreated;

        #endregion

        #region Constructors: Protected

        protected EntityDB(string connectionString)
        {
            ConnectionString = connectionString;
            DBEngine = new DBEngine(ConnectionString);
            OnTableCreated += (s, e) => { ProfilerUtilities.DebugWrite("Table: '{0}' created success.", e.RowSchema.TableName); };
        }

        #endregion

        #region Methods: Static

        public static EntityDB GetInstance(string connectionString)
        {
            lock (singl)
            {
                if (_instance.IsNull())
                {
                    _instance = new EntityDB(connectionString);
                }
                return _instance;
            }
        }

        #endregion

        #region Methods: Private

        private int CreateTable(bool isThrowException, Type type, int result)
        {
            try
            {
                EntityRowSchema rowSchema = GetEntityRowFromType(type);
                string ddl = GetDDLText(rowSchema);
                DBCommand command = DBEngine.CreateCommand(ddl);
                command.Execute();
                result++;
                OnTableCreated?.Invoke(this, new EntityRowSchemaEventArgs(rowSchema));
            }
            catch (AttributeNotFoundException ex)
            {
                if (isThrowException)
                    throw ex;
                ProfilerUtilities.DebugWrite("Table for type: '{0}' created fail because type not markered attribute '{1}'.",
                    type.FullName,
                    typeof(DBTableAttribute).FullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Methods: Protected

        /// <summary>
        /// Returns EntityRow from Type.
        /// </summary>
        /// <param name="type">Type markered attribute 'DBTable'.</param>
        /// <returns cref="EntityRowSchema">Entity row with EntityColumns for init data table.</returns>
        protected virtual EntityRowSchema GetEntityRowFromType(Type type)
        {
            DBTableAttribute tableAttribute = GetAttributeFromType<DBTableAttribute>(type);
            PropertyInfo[] propInfos = GetClassProperties(type);
            IEnumerable<EntityColumnSchema> columnSchemas = GetEntityColumnSchemas(propInfos);
            return new EntityRowSchema(tableAttribute, columnSchemas);
        }

        /// <summary>
        /// Method converts array propertyInfo to EntityColumnSchema list.
        /// </summary>
        /// <param name="propInfo">Array property info.</param>
        /// <returns cref="EntityColumnSchema">EntityColumnSchema list.</returns>
        protected virtual IEnumerable<EntityColumnSchema> GetEntityColumnSchemas(PropertyInfo[] propInfos)
        {
            List<EntityColumnSchema> list = new List<EntityColumnSchema>();
            propInfos.ForEach(prop => {
                var columnSchema = new EntityColumnSchema(prop);
                var columnAttributes = GetAttributesFromType(prop, false);
                columnAttributes.ForEach(attr => attr.UpdateColumnSchema(columnSchema));
                list.Add(columnSchema);
            });
            return list;
        }

        /// <summary>
        /// Gets attribute from Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns cref="DBTableAttribute">Instance of DbTableAttribute.</returns>
        /// <exception cref="AttributeNotFoundException">Throws if can`t find atttribute in Type.</exception>
        protected virtual T GetAttributeFromType<T>(MemberInfo type, bool isThrowException)
            where T: Attribute
        {
            T аttribute = (T)Attribute.GetCustomAttribute(type, typeof(T));
            if (аttribute.IsNull() && isThrowException)
                throw new AttributeNotFoundException(typeof(T), type.GetType());
            return аttribute;
        }

        protected virtual List<DBColumnAttribute> GetAttributesFromType(MemberInfo type, bool isThrowException)
        {
            List<DBColumnAttribute> attributes = Attribute.GetCustomAttributes(type, typeof(DBColumnAttribute), true)
                .Select(x => x as DBColumnAttribute).ToList();
            return attributes;
        }

        /// <summary>
        /// Gets attribute from Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns cref="DBTableAttribute">Instance of DbTableAttribute.</returns>
        /// <exception cref="AttributeNotFoundException">Throws if can`t find atttribute in Type.</exception>
        protected virtual T GetAttributeFromType<T>(MemberInfo type)
            where T : Attribute
        {
            return GetAttributeFromType<T>(type, true);
        }

        /// <summary>
        /// Method returns all public properties from class.
        /// </summary>
        /// <param name="type">Type of Class.</param>
        /// <returns cref="PropertyInfo">Public PropertyInfo array.</returns>
        protected virtual PropertyInfo[] GetClassProperties(Type type)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            properties.ForEach(prop => prop.GetCustomAttribute(typeof(IgnoreAttribute)).IsNull(), result.Add);
            return result.ToArray();
        }

        /// <summary>
        /// Method returns DDL string by EntityRowSchema.
        /// </summary>
        /// <param name="rowSchema" cref="EntityRowSchema">EntityRowSchema.</param>
        /// <returns>DDL string.</returns>
        protected virtual string GetDDLText(EntityRowSchema rowSchema)
        {
            DBManagers managers = DBManagerTypesSection.GetConfig().DBManagers;
            IDDLSqlTextBuilder ddlBuilder = managers.DDlBuilderManager;
            return ddlBuilder.GetSqlTextBySchema(rowSchema);
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Inits data base.
        /// </summary>
        /// <param name="isThrowException">Flag which throw exception if true.</param>
        /// <param name="tableTypes">List Types for create data base tables.</param>
        /// <returns>Count success create tables.</returns>
        /// <exception cref="AttributeNotFoundException">Throws if engine can`t find atribute in Type.</exception>
        public virtual int InitDB(bool isThrowException, IEnumerable<Type> tableTypes)
        {
            int result = 0;
            tableTypes.ForEach(type =>
            {
                result = CreateTable(isThrowException, type, result);
            });
            return result;
        }

        /// <summary>
        /// Inits data base.
        /// </summary>
        /// <param name="isThrowException">Flag which throw exception if true.</param>
        /// <param name="tableTypes">List Types for create data base tables.</param>
        /// <returns>Count success create tables.</returns>
        /// <exception cref="AttributeNotFoundException">Throws if engine can`t find atribute in Type.</exception>
        public virtual int InitDB(bool isThrowException, params Type[] tableTypes)
        {
            return InitDB(isThrowException, tableTypes.ToList());
        }

        #endregion   

    }

}
