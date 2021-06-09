using System;
using System.Runtime.Serialization;

namespace EchoDataDisplay
{
    [Serializable]
    internal class InputDataFormatException : Exception
    {
        public InputDataFormatException()
        {
        }

        public InputDataFormatException(string message) : base(message)
        {
        }

        public InputDataFormatException(string header, string lineNum, string file) 
            : base("Error: Input Data has Incorrect Formating." + Environment.NewLine + "Expected a sentence of the form: " + header + " at line: " + lineNum + " in file: " + file)
        {
        }

        public InputDataFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InputDataFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}