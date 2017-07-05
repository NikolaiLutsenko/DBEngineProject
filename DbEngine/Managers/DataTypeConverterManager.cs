using DBEngineProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DBEngineProject.Managers
{

    #region Class: DataTypeConverterManager

    public class SqlDataTypeManager : IDataTypeManager
    {

        #region Methods: Static

        public virtual string ConvertToSqlType(Type type)
        {
            string result = string.Empty;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    result = "int";
                    break;
                case TypeCode.Boolean:
                    result = "bit";
                    break;
                case TypeCode.DateTime:
                    result = "datetime";
                    break;
                case TypeCode.String:
                    result = "varchar(50)";
                    break;
                case TypeCode.Char:
                    result = "nchar(1)";
                    break;
                default:
                    throw new ArgumentException(string.Format("Type ({0}) can't convert to sql type", type.FullName));
            }
            return result;
        }

        public static string Convert(object value)
        {
            string result = value.IsNull() ? "NULL" : String.Empty;
            if (result != String.Empty)
                return result;
            Type valueType = value.GetType();
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Byte:
                    result = value.ToString();
                    break;
                case TypeCode.Boolean:
                    result = (bool)value ? "1" : "0";
                    break;
                case TypeCode.DateTime:
                    result = String.Format("'{0}'", ((DateTime)value).ToString());
                    break;
                case TypeCode.String:
                case TypeCode.Char:
                    result = String.Format("'{0}'", value.ToString());
                    break;
                default:
                    throw new ArgumentException("Value unknown TypeCode");
            }
            return result;
        }

        #endregion

    }

    #endregion

}
