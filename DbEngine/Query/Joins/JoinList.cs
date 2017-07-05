using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEngineProject.Interfaces;

namespace DBEngineProject.Query
{
    public class JoinList: List<Join>, ISqlText
    {
        public string GetSqlText()
        {
            StringBuilder result = new StringBuilder();
            ForEach(item => {
                result.Append(item.GetSqlText());
                result.Append(Environment.NewLine);
            });
            return result.ToString();
        }


    }
}
