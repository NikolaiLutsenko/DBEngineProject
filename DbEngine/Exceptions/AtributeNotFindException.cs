using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEngineProject.Exceptions
{

    #region Class: AtributeNotFindException

    /// <summary>
    /// The exception that is thrown when can`t find attribute in source.
    /// </summary>
    [DebuggerDisplay("{Message}")]
    public class AttributeNotFoundException: ArgumentException
    {

        #region Properties: Public

        public Type AttributeType { get; protected set; }

        public Type SourceType { get; protected set; }

        #endregion

        #region Constructors: Public

        /// <summary>
        /// Inits new instance of type AttributeNotFindException.
        /// </summary>
        /// <param name="attributeType" cref="Type">Attribute type which not found.</param>
        /// <param name="sourceType" cref="Type">Source type where not found attribute.</param>
        public AttributeNotFoundException(Type attributeType, Type sourceType)
            :this(attributeType, sourceType, null) { }

        /// <summary>
        /// Inits new instance of type AttributeNotFindException.
        /// </summary>
        /// <param name="attributeType" cref="Type">Attribute type which not found.</param>
        /// <param name="sourceType" cref="Type">Source type where not found attribute.</param>
        /// <param name="innerException" cref="Exception">Inner exception.</param>
        public AttributeNotFoundException(Type attributeType, Type sourceType, Exception innerException)
            :base(String.Format("Can`t find attribute '{0}' in '{1}'", attributeType.FullName, sourceType.FullName), innerException)
        {
            AttributeType = attributeType;
            SourceType = sourceType;
        }

        #endregion

    }

    #endregion

}
