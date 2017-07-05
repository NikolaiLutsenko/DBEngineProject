using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Services
{

    #region Class: Cacheble

    /// <summary>
    /// Class performs the logic of cacheble value.
    /// </summary>
    public abstract class Cacheble
    {

        #region Properties: Protected

        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        #endregion

        #region Methods: Public

        public virtual string GetCacheKey(params object[] parametrs)
        {
            return parametrs.JoinToString("_");
        }

        public virtual void InsertIntoCahce(string cacheKey, object value)
        {
            InsertIntoCahce(cacheKey, value, 60 * 1);
        }

        public virtual void InsertIntoCahce(string cacheKey, object value, double expiration)
        {
            CacheItemPolicy policy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expiration)
            };
            Cache.Set(cacheKey, value, policy);
        }

        public virtual T GetFromCache<T>(string cacheKey)
        {
            var result = Cache[cacheKey];
            return result.IsNull() ? default(T) : (T)result;
        }

        #endregion

    }

    #endregion

}
