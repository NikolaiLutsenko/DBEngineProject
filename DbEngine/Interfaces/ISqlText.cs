using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Interfaces
{

    #region Interface: ISqlText

    /// <summary>
    /// Interface who returns sql text.
    /// </summary>
    public interface ISqlText
    {

        #region Methods: Public

        /// <summary>
        /// Method returns sql text.
        /// </summary>
        /// <returns>Sql text.</returns>
        string GetSqlText();

        #endregion

    }

    #endregion

}
