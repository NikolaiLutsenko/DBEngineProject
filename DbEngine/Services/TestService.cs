using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Services
{
    public class TestService: BaseService
    {
		
        public TestService()
			:base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=TestDBEngine;Integrated Security=True") { }

		public Object GetClientList1(int clientId) 
        {
            string key = GetCacheKey(clientId, nameof(GetClientList1));
            var result = GetFromCache<Object>(key);
            if(result.IsNull())
            {
                result = new object();
            }
            InsertIntoCahce(key, result, TimeSpan.FromSeconds(10));
            return result;
        }
    }
     
    
}
