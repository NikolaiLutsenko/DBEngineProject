using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Interfaces
{

    #region Interface: IDBCacheble<T>

    public interface IDBCacheble<T>
    {

        #region Methods: Public

        void InsertIntoCache(string cacheKey, T result);

        bool GetFromCache(string cacheKey, out T result);

        bool ClearCache();

        bool ClearCache(string cacheKey);

        #endregion

    }

    #endregion

    #region Interface: IDBCacheble

    public interface IDBCacheble: IDBCacheble<Object> { }

    #endregion

}
