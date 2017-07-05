using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Entities;

namespace DBEngineProject.Attributes
{

    #region Class: PrimaryAttribute

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryAttribute: DBColumnAttribute
    {

        #region Methods: Public

        public override bool IsValid()
        {
            return true;
        }

        public override void UpdateColumnSchema(EntityColumnSchema columnSchema)
        {
            base.UpdateColumnSchema(columnSchema);
            columnSchema.IsPrimary = true;
            columnSchema.IsAllowNull = false;
        }

        #endregion

    }

    #endregion

}
