using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions
{

    #region Class: CollectionUtilities

    /// <summary>
    /// Class contains extension methods for collection.
    /// </summary>
    public static class CollectionUtilities
    {

        #region Methods: Public

        /// <summary>
        /// Method executes action on each item in collection.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="action">Action which execute.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.CheckNull(nameof(source));
            action.CheckNull(nameof(action));
            foreach (T item in source)
                action(item);
        }

        /// <summary>
        /// Method executes action on each item where predicate is true.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="predicate">Predicate for collection.</param>
        /// <param name="action">Action which execute.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<T>(this IEnumerable<T> source, Predicate<T> predicate, Action<T> action)
        {
            source.CheckNull(nameof(source));
            predicate.CheckNull(nameof(predicate));
            action.CheckNull(nameof(action));
            foreach (T item in source.Where(x => predicate(x)))
                action(item);
        }

        /// <summary>
        /// Method gets value from collection or returns default value.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="index">Index for get value.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>default value for Type.</returns>
        public static T GetValue<T>(this IEnumerable<T> source, int index)
        {
            return source.GetValue(index, default(T));
        }

        /// <summary>
        /// Method gets value from collection or returns default value.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="index">Index for get value.</param>
        /// <param name="defaultValue">Default value which returns if item can`t find by index.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static T GetValue<T>(this IEnumerable<T> source, int index, T defaultValue)
        {
            source.CheckNull(nameof(source));
            if (index < 0 || index >= source.Count())
                return defaultValue;
            return source.ToArray()[index];
        }

        /// <summary>
        /// Method adds value in collection if value not exist.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="value">Value for add if not exist in collection.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddIfNotExist<T>(this List<T> source, T value)
        {
            source.CheckNull(nameof(source));
            if (!source.Contains(value))
                source.Add(value);
        }

        /// <summary>
        /// Method adds values in collection if values not exist.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="values">Values collection.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRangeIfNotExist<T>(this List<T> source, IEnumerable<T> values)
        {
            source.CheckNull(nameof(source));
            values.CheckNull(nameof(values));
            values.ForEach(source.AddIfNotExist);
        }

        /// <summary>
        /// Method returns true if source is null or count less once.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <returns>Returns true if source is null or count less once</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            source.CheckNull(nameof(source));
            return source.IsNull() || source.Count() < 1;
        }

        /// <summary>
        /// Method returns string view for collection items when separator equal comma.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <returns>String view.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string JoinToString<T>(this IEnumerable<T> source)
        {
            source.CheckNull(nameof(source));
            return source.JoinToString(null, ", ");
        }

        /// <summary>
        /// Method returns string view for collection items.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="separator">String separator.</param>
        /// <returns>String view.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string JoinToString<T>(this IEnumerable<T> source, string separator)
        {
            source.CheckNull(nameof(source));
            separator.CheckNull(nameof(separator));
            return source.JoinToString(null, separator);
        }

        /// <summary>
        /// Method returns string view for collection items.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="func">Func for convert T to string.</param>
        /// <returns>String view.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string JoinToString<T>(this IEnumerable<T> source, Func<T, string> func)
        {
            return source.JoinToString(func, ", ");
        }

        /// <summary>
        /// Method returns string view for collection items.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="source">Items collection.</param>
        /// <param name="func">Func for convert T to string.</param>
        /// <param name="separator">String separator.</param>
        /// <returns>String view.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string JoinToString<T>(this IEnumerable<T> source, Func<T,string> func, string separator)
        {
            source.CheckNull(nameof(source));
            separator.CheckNull(nameof(separator));
            if (func.IsNull())
                return String.Join(separator, source);
            else
                return String.Join(separator, source.Select(x => func(x)));
        }

        #endregion

    }

    #endregion

}
