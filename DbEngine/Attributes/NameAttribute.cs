using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Entities;

namespace DBEngineProject.Attributes
{

    #region Class: NameAttribute

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NameAttribute: DBColumnAttribute
    {

        #region Properties: Public

        public string Name { get; protected set; }

        #endregion

        #region Constructors: Public

        public NameAttribute(string name)
        {
            Name = name;
            ErrorMessage = "Name must be define and not empty";
        }

        #endregion

        #region Methods: Public

        public override void UpdateColumnSchema(EntityColumnSchema columnSchema)
        {
            base.UpdateColumnSchema(columnSchema);
            columnSchema.Name = Name;
        }

        public override bool IsValid()
        {
            return !String.IsNullOrEmpty(Name);
        }

        #endregion

    }

    #endregion

}
