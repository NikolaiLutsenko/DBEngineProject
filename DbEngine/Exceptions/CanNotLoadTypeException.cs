using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Exceptions
{
    public class CanNotLoadTypeException: ArgumentException
    {
        public string FullNameType { get; protected set; }

        public string AssemblyName { get; protected set; }

        public CanNotLoadTypeException(string fullNameType, string assemblyName)
            :this(fullNameType, assemblyName, null) { }

        public CanNotLoadTypeException(string fullNameType, string assemblyName, Exception innerException)
            : base(String.Format("Can`t load type '{0}' from assembly '{1}'.", fullNameType, assemblyName), innerException)
        {
            FullNameType = fullNameType;
            AssemblyName = assemblyName;
        }
        


    }
}
