using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Entities;

namespace DBEngineProject.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsNullAttribute : DBColumnAttribute
    {

        #region Properties: Public

        public bool IsAllow { get; protected set; }

        #endregion

        #region Constructors: Public

        public IsNullAttribute()
        {
            IsAllow = true;
        }

        public IsNullAttribute(bool isAllow)
        {
            IsAllow = isAllow;
        }

        #endregion

        #region Methods: Public

        public override void UpdateColumnSchema(EntityColumnSchema columnSchema)
        {
            base.UpdateColumnSchema(columnSchema);
            columnSchema.IsAllowNull = IsAllow;
        }

        public override bool IsValid()
        {
            return true;
        }

        #endregion

    }
}
