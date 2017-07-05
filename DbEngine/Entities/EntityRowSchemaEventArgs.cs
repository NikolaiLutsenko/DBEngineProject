using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Entities
{

    #region Class: EntityRowSchemaEventArgs

    public class EntityRowSchemaEventArgs: EventArgs
    {

        #region Properties: Public

        public EntityRowSchema RowSchema { get; protected set; }

        #endregion

        #region Constructors: Public

        public EntityRowSchemaEventArgs(EntityRowSchema rowSchema)
        {
            RowSchema = rowSchema;
        }

        #endregion

    }

    #endregion

}
