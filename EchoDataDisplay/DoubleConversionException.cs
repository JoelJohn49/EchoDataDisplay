using System;
using System.Runtime.Serialization;

namespace EchoDataDisplay
{
    [Serializable]
    internal class DoubleConversionException : ApplicationException
    {
        public DoubleConversionException()
        {
        }

        public DoubleConversionException(string message) : base(message)
        {
        }

        public DoubleConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DoubleConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}