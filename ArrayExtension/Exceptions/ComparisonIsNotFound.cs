using System;
using System.Runtime.Serialization;

namespace ArrayExtension.Exceptions
{
    public class ComparisonIsNotFound: Exception
    {
        public ComparisonIsNotFound()
        {
        }

        public ComparisonIsNotFound(string message): base(message)
        {
        }

        public ComparisonIsNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComparisonIsNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
