using System;
using System.Runtime.Serialization;

namespace EchoDataDisplay
{
    [Serializable]
    internal class DoubleConversionException : ApplicationException
    {
        //private readonly string failedToConvertValue;

        public DoubleConversionException()
        {
        }

        public DoubleConversionException(string message)
            : base(message)
        {
        }

        public DoubleConversionException(string failedToConvertValue, string fileName)
            : base("Error: Cannot be converted into double." + Environment.NewLine + "Value: \"" + failedToConvertValue + "\" in " + fileName)
        {
        }

        public DoubleConversionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DoubleConversionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}