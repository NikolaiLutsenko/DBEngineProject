using DBEngineProject.Attributes;
using System;
using System.Reflection;
using Utilities.Extensions;

namespace DBEngineProject.Entities
{

    #region Interface: IDBModel

    public abstract class Entity
    {

        #region Properties: Protected

        [Ignore]
        public EntityRow EntityRow { get; protected set; }

        /// <summary>
        /// Returns primary column name of String.Empty.
        /// </summary>
        public string PrimaryColumnName
        {
            get => GetPrimaryColumnName();
        }

        #endregion

        #region Methods: Protected

        private string GetPrimaryColumnName()
        {
            string name = String.Empty;
            Type type = GetType();
            PropertyInfo[] props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            props.ForEach(prop => Attribute.IsDefined(prop, typeof(PrimaryAttribute)), prop => name = prop.Name);
            return name;
        }

        #endregion

        #region Methods: Public

        public virtual void Init(EntityRow row)
        {
            EntityRow = row;
        }

        public virtual object Get(string columnName)
        {
            return EntityRow.Get<object>(columnName);
        }

        public virtual T GetPrimaryColumnValue<T>()
        {
            return Get<T>(PrimaryColumnName);
        }

        public virtual T GetPrimaryColumnValue<T>(T defaultValue)
        {
            return Get<T>(PrimaryColumnName, defaultValue);
        }

        public virtual T Get<T>(string columnName)
        {
            return EntityRow.Get<T>(columnName);
        }

        protected virtual T Get<T>(string columnName, T defaultValue)
        {
            return EntityRow.Get<T>(columnName, defaultValue);
        }

        protected virtual T Get<T>(string columnName, Func<T> func)
        {
            return EntityRow.Get<T>(columnName, func);
        }

        #endregion

    }

    #endregion

}
