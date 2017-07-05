using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
using DBEngineProject.Managers;
using System.Collections.Specialized;
using System.Reflection;
using System.Diagnostics;
using DBEngineProject.Interfaces;
using DBEngineProject.Exceptions;

namespace DBEngineProject.Sections
{

    #region Class: DBManagerTypesSection

    /// <summary>
    /// Class performs the logic of view custom section wich contains DBManager collection.
    /// </summary>
    public class DBManagerTypesSection : ConfigurationSection
    {

        #region Properties: Public

        [ConfigurationProperty("DBManagers")]
        [ConfigurationCollection(typeof(DBManagers), AddItemName = "DBManager")]
        public DBManagers DBManagers => this["DBManagers"] as DBManagers;

        #endregion

        #region Methods: Static

        /// <summary>
        /// Method returns instance of ManagerTypesSection type.
        /// </summary>
        /// <returns cref="ManagerTypesSection">Instance of ManagerTypesSection type.</returns>
        public static DBManagerTypesSection GetConfig()
        {
            return (DBManagerTypesSection)ConfigurationManager.GetSection("DBManagerTypesSection") ?? new DBManagerTypesSection();
        }

        #endregion

    }

    #endregion

    #region Class: DBManagers

    /// <summary>
    /// Class performs the view of collection dbManagers.
    /// </summary>
    [ConfigurationCollection(typeof(DBManagers))]
    public class DBManagers: ConfigurationElementCollection
    {

        #region Properties: Public

        public DBManager this[int index]
        {
            get => (BaseGet(index) as DBManager);
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new DBManager this[string responseString]
        {
            get { return (DBManager)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        public DBManager DDlBuilderManagerType => this["DDlBuilderManagerType"] ?? new DBManager("DDlBuilderManagerType", "DBEngineProject.Query.SqlBuilders.DDLSqlTextBuilder");

        public IDDLSqlTextBuilder DDlBuilderManager => GetTypedInstance<IDDLSqlTextBuilder>(DDlBuilderManagerType);

        #endregion

        #region Methods: Protected

        protected override ConfigurationElement CreateNewElement()
        {
            return new DBManager();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DBManager)element).Name;
        }

        protected virtual object GetInstance(DBManager manager)
        {
            Type type = Assembly.Load(manager.AssemblyName)
                .GetTypes()
                .FirstOrDefault(x => x.FullName.ToLower() == manager.Type.ToLower());
            if (type.IsNull())
                throw new CanNotLoadTypeException(manager.Type, manager.AssemblyName);
            return Activator.CreateInstance(type);
        }

        protected virtual T GetTypedInstance<T>(DBManager manager)
        {
            Object result = GetInstance(manager);
            if (!(result is T))
                throw new NotImplementException(DDlBuilderManagerType.Type, DDlBuilderManagerType.AssemblyName, typeof(T));
            return (T)result;
        }

        #endregion

    }

    #endregion

    #region Class: DBManager

    [DebuggerDisplay("Name = {Name},Type = {Type}, Assembly = {AssemblyName}")]
    public class DBManager: ConfigurationElement
    {

        #region Properties: Public

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return ((string)(base["name"])); }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("assembly", DefaultValue = "DBEngineProject", IsKey = false, IsRequired = false)]
        public string AssemblyName {
            get { return ((string)(base["assembly"])); }
            set { base["assembly"] = value; }
        }

        [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
        public string Type
        {
            get { return ((string)(base["type"])); }
            set { base["type"] = value; }
        }

        #endregion

        #region Constructors

        public DBManager()
        {
            AssemblyName = "DBEngineProject";
        }

        public DBManager(string name, string type, string assembly)
            :this(name, type)
        {
            AssemblyName = assembly; 
        }

        public DBManager(string name, string type)
            :this()
        {
            Name = name;
            Type = type;
        }

        #endregion

    }

    #endregion

}
