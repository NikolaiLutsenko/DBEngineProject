using System;

namespace DBEngineProject.Interfaces
{
    public interface IDataTypeManager
    {
        string ConvertToSqlType(Type type);
    }
}