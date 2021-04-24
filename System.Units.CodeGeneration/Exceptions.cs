using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Units.CodeGeneration
{
    internal class UnitsNetCodeGenException : Exception
    {
        public UnitsNetCodeGenException()
        {
        }

        protected UnitsNetCodeGenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnitsNetCodeGenException(string message) : base(message)
        {
        }

        public UnitsNetCodeGenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
