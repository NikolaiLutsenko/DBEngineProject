using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Exceptions
{
    public class NotImplementException: ArgumentException
    {

        public string FullNameType { get; protected set; }

        public string AssemblyName { get; protected set; }

        public Type MustImplenetType { get; protected set; }


        public NotImplementException(string fullNameType, string assemblyName, Type mustImplenetType)
            :this(fullNameType, assemblyName, mustImplenetType, null) { }

        public NotImplementException(string fullNameType, string assemblyName, Type mustImplenetType, Exception innerException)
            :base(String.Format("Type '{0}' from assembly '{1}' must implement type '{2}'.", fullNameType, assemblyName, mustImplenetType.FullName))
        {
            FullNameType = fullNameType;
            AssemblyName = assemblyName;
            MustImplenetType = mustImplenetType;
        }
    }
}
