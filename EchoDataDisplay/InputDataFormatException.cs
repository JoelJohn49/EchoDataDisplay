/*
 * author: Joel John for Scout Aerial
 * email: joeljohn49@gmail.com
 * last modified: 15/06/2021
*/

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

        public InputDataFormatException(string lineNum, string file)
            : base("Error: Input Data has Incorrect Formating." + Environment.NewLine + "Expected a line with 4 or more comma separated values at line: " + lineNum + " in file: " + file)
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