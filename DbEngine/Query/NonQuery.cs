using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Query
{
    public abstract class NonQuery : Query
    {
        public NonQuery(string tableName) 
            :base(tableName)
        {
        }
    }
}
