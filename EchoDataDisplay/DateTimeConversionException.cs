using System;
using System.Runtime.Serialization;

namespace EchoDataDisplay
{
    [Serializable]
    internal class DateTimeConversionException : Exception
    {
        public DateTimeConversionException()
        {
        }

        public DateTimeConversionException(string message) : base(message)
        {
        }

        public DateTimeConversionException(string failedToConvertValue, string fileName, string format)
            : base("Error: Cannot be converted into DateTimeOffset." + Environment.NewLine + "Value: \"" + failedToConvertValue + "\" does not match format: " + format + "\" in " + fileName)
        {
        }

        public DateTimeConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DateTimeConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}