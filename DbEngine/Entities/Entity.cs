using System;
using System.Reflection;
using Utilities.Extensions;

namespace DBEngineProject.Entities
{

    #region Interface: IDBModel

	/// <summary>
	/// Class for view db table.
	/// </summary>
    public abstract class Entity
    {

		#region Methods: Public

		/// <summary>
		/// Inits entity by EntityRow.
		/// </summary>
		/// <param name="row">Collection with columns and values.</param>
		public abstract void Init(EntityRow row);

        #endregion

    }

    #endregion

}
