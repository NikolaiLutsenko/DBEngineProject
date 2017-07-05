using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DBEngineProject.Entities;
using Utilities.Extensions;
using DBEngineProject.Managers;
using DBEngineProject.Interfaces;

namespace DBEngineProject.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class DBColumnAttribute : Attribute
    {

        #region Properties: Public

        public string ErrorMessage { get; set; }

        #endregion


        #region Methods: Public

        /// <summary>
        /// Method updates EntityColumnSchema for prepare column initialize.
        /// </summary>
        /// <param name="columnSchema" cref="EntityColumnSchema">Instance for update.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void UpdateColumnSchema(EntityColumnSchema columnSchema)
        {
            columnSchema.CheckNull(nameof(columnSchema));
        }

        public abstract bool IsValid();

        #endregion

    }
}
