using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Interfaces
{

    #region Interface: ITryGet

    /// <summary>
    /// Interface which checks can get value and returns value.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public interface ITryGet<T>
    {

        #region Properties: Public

        /// <summary>
        /// Checks can returns value.
        /// </summary>
        bool IsCanGetValue { get; }

        /// <summary>
        /// Returns value.
        /// </summary>
        T GetValue { get; }

        #endregion

    }

    #endregion

}
