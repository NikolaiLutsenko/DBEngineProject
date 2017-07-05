using System;
using System.Collections.Generic;

namespace Utilities.Extensions
{

    #region Class: ObjectExtensions

    /// <summary>
    /// Utilities class for all Object type children
    /// </summary>
    public static class ObjectUtilities
    {

        #region Methods: Public

        /// <summary>
        /// Method returns true if target object is null.
        /// </summary>
        /// <param name="target">Object for check.</param>
        /// <returns>Flag if object equal null.</returns>
        public static bool IsNull<T>(this T target)
            where T: class
        {
            return target == null;
        }

        /// <summary>
        /// Method throws exception if target object is null.
        /// </summary>
        /// <param name="target">Object for check.</param>
        /// <param name="paramName">Object name for exception.</param>
        /// <exception cref="ArgumentNullException">If object equal null.</exception>
        public static void CheckNull<T>(this T target, string paramName)
            where T: class
        {
            if (target.IsNull()) {
                throw new ArgumentNullException(paramName ?? String.Empty);
            }
        }

        public static bool IsDefault<T>(this T target)
            where T: struct
        {
            return EqualityComparer<T>.Default.Equals(target, default(T));
        }

        public static void CheckDefault<T>(this T target, string paramName)
            where T : struct
        {
            if (target.IsDefault())
            {
                throw new ArgumentNullException(paramName ?? String.Empty);
            }
        }

        #endregion

    }

    #endregion

}
