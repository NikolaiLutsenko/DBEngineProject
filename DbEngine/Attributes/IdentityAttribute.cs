using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Entities;

namespace DBEngineProject.Attributes
{

    #region Class: IdentityAttribute

    /// <summary>
    /// Attribute which markered property as Identity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IdentityAttribute: DBColumnAttribute
    {

        #region Properties: Public

        /// <summary>
        /// Identity number start.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Identity number step.
        /// </summary>
        public int Step { get; set; }

        #endregion

        #region Constructors: Public

        public IdentityAttribute(int start, int step)
        {
            Start = start;
            Step = step;
            ErrorMessage = "Identity start must be great or equal zero and step must be great or equal once.";
        }

        #endregion

        #region Methods: Public

        public override void UpdateColumnSchema(EntityColumnSchema columnSchema)
        {
            base.UpdateColumnSchema(columnSchema);
            if (columnSchema.DataTypeName != "int")
                throw new ArgumentException(String.Format("ColumnDataType '{0}' must be 'int'."));
            columnSchema.IsIdentity = true;
            columnSchema.IdentityStart = Start;
            columnSchema.IdentityStep = Step;
        }

        public override bool IsValid()
        {
            return Start >= 0 && Step >= 1;
        }

        #endregion

    }

    #endregion

}
